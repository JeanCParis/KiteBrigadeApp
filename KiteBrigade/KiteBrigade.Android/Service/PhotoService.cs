using Android.Content;
using Android.Provider;
using KiteBrigade.Droid.Utils;
using KiteBrigade.Service.Interface;
using Plugin.CurrentActivity;

namespace KiteBrigade.Android.Service
{
    public class PhotoService : IPhotoService
    {
        public void ShowPhotoSelector(string selectorTitle)
        {
            Intent intent = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
            intent.SetType("image/*");
            CrossCurrentActivity.Current.Activity.StartActivityForResult(Intent.CreateChooser(intent, selectorTitle), RequestCode.SelectPicture);
        }
    }
}
