using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidX.Core.App;
using Plugin.Firebase.Crashlytics;
using SkiaSharp;
using Svg.Skia;

namespace Matri.Helper;

public static class NotificationHelper
{
    public static void ShowCustomNotification(string title, string language)
    {
        try
        {
            var context = global::Android.App.Application.Context;
            if (context == null)
                throw new InvalidOperationException("Application context is null.");

            var channelId = $"{AppInfo.PackageName}.general";

            // FIX: Use the correct Resource namespace for Android resources.
            // Typically, the Resource class is in the root namespace of your Android project (e.g., Mooknayak.APP).
            // If your Android project's namespace is Mooknayak.APP, use global::Mooknayak.APP.Resource.

            // First, load the SVG without forcing height
            var tempBitmap = RenderSvgToBitmap("appicon.svg", 48, 0); // 0 for height means auto-calculate aspect ratio

            // Calculate height to maintain aspect ratio
            int width = 48;
            int height = (int)(tempBitmap.Height * (width / (float)tempBitmap.Width));

            // Now render the final bitmap with correct width and height
            var bitmap = RenderSvgToBitmap("appicon.svg", width, height);

            var remoteView = new RemoteViews(AppInfo.PackageName,
                Resource.Layout.custom_notification);
            remoteView.SetTextViewText(Resource.Id.notify_title, title);
            remoteView.SetTextViewText(Resource.Id.notify_body, "sdfjsdfjsfjsfj"); // Add this
            remoteView.SetImageViewBitmap(Resource.Id.notify_icon, bitmap);

            var intent = new Intent(context, typeof(MainActivity));
            intent.SetAction("notification_tap");
            intent.PutExtra("language", language);
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);

            var pendingIntent = PendingIntent.GetActivity(
           context,
           0,
           intent,
           PendingIntentFlags.UpdateCurrent |
           (Build.VERSION.SdkInt >= BuildVersionCodes.S ? PendingIntentFlags.Immutable : 0)
       );

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channel = new NotificationChannel(channelId, "General Notifications", NotificationImportance.High)
                {
                    Description = "App alerts and updates",
                    //EnableLights = true,
                    //EnableVibration = true,
                    LockscreenVisibility = NotificationVisibility.Public
                };
                var nm = (NotificationManager)context.GetSystemService(Context.NotificationService);
                nm.CreateNotificationChannel(channel);
            }

            var builder = new NotificationCompat.Builder(context, channelId)
    .SetSmallIcon(Resource.Drawable.ic_m) // must be plain white icon in drawable
    .SetContentTitle("ChristianJodi")
    .SetContentText(title)
    .SetStyle(new NotificationCompat.BigTextStyle().BigText(title))
    .SetLargeIcon(bitmap) // ✅ put your SVG bitmap here, not in small icon
    .SetAutoCancel(true)
    .SetPriority((int)NotificationPriority.High)
    .SetDefaults((int)NotificationDefaults.All) // sound + vibration
    .SetContentIntent(pendingIntent);

            builder.SetCustomContentView(remoteView);
            builder.SetCustomBigContentView(remoteView);

            var notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Notify(new Random().Next(), builder.Build());
        }
        catch (Exception ex)
        {
            CrossFirebaseCrashlytics.Current.RecordException(ex);
        }
    }

    public static Bitmap RenderSvgToBitmap(string filename, int width, int height = 0)
    {
        // Load SVG from MAUI app package
        var context = global::Android.App.Application.Context;
        using var stream = context.Assets.Open(filename);

        var svg = new SKSvg();
        svg.Load(stream);

        // Get SVG dimensions
        var svgRect = svg.Picture?.CullRect ?? SKRect.Empty;

        // Auto-calculate height if not provided (0)
        if (height == 0 && svgRect.Width > 0)
            height = (int)(width * (svgRect.Height / svgRect.Width));

        float scaleX = width / svgRect.Width;
        float scaleY = height / svgRect.Height;

        using var skBitmap = new SKBitmap(width, height);
        using var skCanvas = new SKCanvas(skBitmap);
        skCanvas.Clear(SKColors.Transparent);

        skCanvas.Scale(scaleX, scaleY);

        if (svg.Picture != null)
            skCanvas.DrawPicture(svg.Picture);

        skCanvas.Flush();

        // Convert SKBitmap to Android Bitmap
        return skBitmap.ToBitmap(); // Extension method
    }


    public static Bitmap ToBitmap(this SKBitmap skBitmap)
    {
        using var image = SKImage.FromBitmap(skBitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        var bytes = data.ToArray();
        return BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
    }
}
