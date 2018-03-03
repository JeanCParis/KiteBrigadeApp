using Android.Views;
using Android.Support.V7.Widget;
using Android.Graphics;

namespace KiteBrigade.Droid.UI.Recycler
{
    public class VerticalSpaceItemDecoration : RecyclerView.ItemDecoration
    {
        private int verticalSpaceHeight;

        public VerticalSpaceItemDecoration(int verticalSpaceHeight)
        {
            this.verticalSpaceHeight = verticalSpaceHeight;
        }

        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            outRect.Bottom = verticalSpaceHeight;
        }
    }
}