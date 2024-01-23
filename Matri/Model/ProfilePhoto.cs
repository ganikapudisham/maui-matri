namespace Matri.Model
{
    public class ProfilePhoto
    {
        public ProfilePhoto(string imageUrl)
        {
            _imageUrl = imageUrl;
        }
        private String _imageUrl;
        public String ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }
    }
}
