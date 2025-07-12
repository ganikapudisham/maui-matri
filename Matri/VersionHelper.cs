using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matri;

public static class VersionHelper
{
    public static bool IsNewVersionAvailable(string currentVersion, string latestVersion)
    {
        var currentParts = currentVersion.Split('.').Select(int.Parse).ToList();
        var latestParts = latestVersion.Split('.').Select(int.Parse).ToList();

        int maxLength = Math.Max(currentParts.Count, latestParts.Count);

        for (int i = 0; i < maxLength; i++)
        {
            int currentPart = i < currentParts.Count ? currentParts[i] : 0;
            int latestPart = i < latestParts.Count ? latestParts[i] : 0;

            if (latestPart > currentPart) return true;
            if (latestPart < currentPart) return false;
        }

        return false;
    }

    public static DateTime GetEasterDate(int year)
    {
        int a = year % 19;
        int b = year / 100;
        int c = year % 100;
        int d = b / 4;
        int e = b % 4;
        int f = (b + 8) / 25;
        int g = (b - f + 1) / 3;
        int h = (19 * a + b - d - g + 15) % 30;
        int i = c / 4;
        int k = c % 4;
        int l = (32 + 2 * e + 2 * i - h - k) % 7;
        int m = (a + 11 * h + 22 * l) / 451;
        int month = (h + l - 7 * m + 114) / 31;
        int day = ((h + l - 7 * m + 114) % 31) + 1;

        return new DateTime(year, month, day, 9, 0, 0); // 9:00 AM
    }
}
