using ZoomableApp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Linq;

[assembly: ExportRenderer(typeof(ScrollView), typeof(ZoomScrollViewRenderer))]
namespace ZoomableApp.iOS.Renderers
{
	public class ZoomScrollViewRenderer : ScrollViewRenderer
	{
		// bool zoomEnabled = false;
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
			MaximumZoomScale = 1f;
			MinimumZoomScale = 0.3f;


		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			if (Subviews.Length > 0)
			{
				ViewForZoomingInScrollView += GetViewForZooming;
			}
			else
			{
				ViewForZoomingInScrollView -= GetViewForZooming;
			}

		}
		public UIView GetViewForZooming(UIScrollView sv)
		{
			return this.Subviews.FirstOrDefault();
		}

	}
}