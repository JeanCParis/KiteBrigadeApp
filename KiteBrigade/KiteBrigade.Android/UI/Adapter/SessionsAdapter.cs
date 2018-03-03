using System.Collections.Generic;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using KiteBrigade.Model;
using KiteBrigade.Business;
using System;
using Uri = Android.Net.Uri;
using Com.Bumptech.Glide;

namespace KiteBrigade.Droid.UI.Adapter
{
    public class SessionAdapter : RecyclerView.Adapter
    {
        public IList<KiteSession> Sessions { get; set; }
        public override int ItemCount => Sessions.Count;
        public event EventHandler<int> SessionClick;

        public SessionAdapter()
        {
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            KiteSession kiteSession = Sessions[position];
            SessionViewHolder sessionView = holder as SessionViewHolder;

            Glide.With(holder.ItemView.Context).Load(kiteSession.PictureUri).Apply(new Com.Bumptech.Glide.Request.RequestOptions().Override(1000, 1000)).Into(sessionView.Picture);
            sessionView.Date.Text = kiteSession.CreationDate.ToString("dd-MM-yyyy");
            sessionView.Score.Text = Math.Round(Score.Calculate(kiteSession.WindValue, kiteSession.WaterValue, kiteSession.FunValue), 1).ToString();
            sessionView.Location.Text = "-";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return new SessionViewHolder(LayoutInflater.From(parent.Context).Inflate(Resource.Layout.card_session, parent, false), OnClick);
        }

        private void OnClick(int position)
        {
            SessionClick?.Invoke(this, position);
        }
    }

    public class SessionViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Picture { get; set; }

        public TextView Date { get; set; }
        public TextView Score { get; set; }
        public TextView Location { get; set; }

        public SessionViewHolder(View itemView, Action<int> clickListener) : base(itemView)
        {
            Picture = itemView.FindViewById<ImageView>(Resource.Id.session_picture);
            Date = itemView.FindViewById<TextView>(Resource.Id.session_date);
            Score = itemView.FindViewById<TextView>(Resource.Id.session_score);
            Location = itemView.FindViewById<TextView>(Resource.Id.session_location);

            itemView.Click += (sender, e) => clickListener(AdapterPosition);
        }
    }
}