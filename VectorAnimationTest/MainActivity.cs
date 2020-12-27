using System;
using Android.Animation;
using Android.App;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Animation;
using Android.Support.Design.Widget;
using Android.Support.Graphics.Drawable;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Org.XmlPull.V1;

namespace VectorAnimationTest
{

    

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {        
        ImageView originalSolution;
        ImageView secondSolution;
        ImageView idealSolution;    // This one does not work

        SpringAnimation rotateAnim;
        AnimatorSet originalSet;
        AnimatorSet notWorking;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            

            originalSolution = FindViewById<ImageView>(Resource.Id.TestVectOne);
            secondSolution = FindViewById<ImageView>(Resource.Id.TestVectTwo);
            idealSolution = FindViewById<ImageView>(Resource.Id.TestVectThree);

            FirstSolution();
            SecondSolution();
            NotWorking();


            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {

            originalSet.Start();
            rotateAnim.Start();
            notWorking.Start();


            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        private void FirstSolution()
        {
            //using an AnimatorSet and Object Animator to rotate the view

            originalSet = new AnimatorSet();
            originalSet.SetInterpolator(new OvershootInterpolator(0.8f));
            originalSet.SetDuration(3000);

            ObjectAnimator dialrotate = ObjectAnimator.OfFloat(originalSolution, "rotation", 0, 826);

            originalSet.Play(dialrotate);

        }

        private void SecondSolution()
        {

            //Similar to first solution but uses a Spring Animator.

            VectorDrawable avd = (VectorDrawable)this.GetDrawable(Resource.Drawable.Needle);

            rotateAnim = new SpringAnimation(secondSolution, DynamicAnimation.Rotation, 270);
            rotateAnim.Spring.SetStiffness(SpringForce.StiffnessLow);
            rotateAnim.SetStartVelocity(50);
            secondSolution.SetImageDrawable(avd);
        }

        private void NotWorking()
        {

            // This does not create an animation. Is it not possible to animate a vector drawable in code?
            // Am I missing a step here?

            VectorDrawable avd = (VectorDrawable)this.GetDrawable(Resource.Drawable.Needle);

            idealSolution.SetImageDrawable(avd);

            var rotate = ObjectAnimator.OfFloat(avd, "rotation", 0f, 756f).SetDuration(3000);

            notWorking = new AnimatorSet();
            notWorking.SetInterpolator(new AccelerateDecelerateInterpolator());
            notWorking.Play(rotate).With(ObjectAnimator.OfFloat(avd, "pivotX", 13.22f)).With(ObjectAnimator.OfFloat(rotate, "pivotY", 13.22f));




        }
	}
}
