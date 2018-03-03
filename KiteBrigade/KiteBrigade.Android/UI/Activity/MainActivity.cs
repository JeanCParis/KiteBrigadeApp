using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using KiteBrigade.Droid.UI.Adapter;
using KiteBrigade.Droid.UI.Recycler;
using KiteBrigade.ViewModel;
using System;

namespace KiteBrigade.Droid.UI.Activity
{
    [Activity(Label = "MainActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : LoggedActivity<MainViewModel>
    {
        private View _addButton;
        private RecyclerView _sessionsRecycler;
        private SessionAdapter _adapter = new SessionAdapter();

        protected override int LayoutId => Resource.Layout.activity_main;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            _addButton = FindViewById(Resource.Id.button_add);
            _sessionsRecycler = FindViewById<RecyclerView>(Resource.Id.recycler_sessions);
            _sessionsRecycler.SetLayoutManager(new LinearLayoutManager(this));
            _sessionsRecycler.AddItemDecoration(new VerticalSpaceItemDecoration((int) Resources.GetDimension(Resource.Dimension.session_card_margin)));

            _sessionsRecycler.SetAdapter(_adapter);
        }

        protected override void OnResume()
        {
            base.OnResume();

            //TODO implement fresh pattern
            _adapter.Sessions = ViewModel.KiteSessions;
            _adapter.NotifyDataSetChanged();
        }

        protected override void SubscribeToViewEvents()
        {
            base.SubscribeToViewEvents();

            _addButton.Click += CreateSession;
            _adapter.SessionClick += EditSession;
        }

        protected override void UnsubscribeFromViewEvents()
        {
            base.UnsubscribeFromViewEvents();

            _addButton.Click -= CreateSession;
            _adapter.SessionClick -= EditSession;
        }

        private void CreateSession(object sender, EventArgs args)
        {
            ViewModel.CreateSession();
            StartActivity(new Intent(this, typeof(SessionCreationActivity)));
        }

        private void EditSession(object sender, int sessionIndex)
        {
            ViewModel.SelectSession(sessionIndex);
            StartActivity(new Intent(this, typeof(SessionEditActivity)));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            ViewModel.LogOut();
        }
    }
}