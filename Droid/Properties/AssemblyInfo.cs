using System.Reflection;
using System.Runtime.CompilerServices;
using Android.App;
using UXDivers.Artina.Shared;
using Xamarin.Forms;

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

[assembly: AssemblyTitle("IcatuzinhoApp.Droid")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("ramaral")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: UsesFeature("android.hardware.wifi", Required = false)]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion("1.0.0")]

// The following attributes are used to specify the signing key for the assembly, 
// if desired. See the Mono documentation for more information about signing.

//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

[assembly: ExportRenderer(typeof(Entry), typeof(UXDivers.Artina.Shared.ArtinaEntryRenderer))]
[assembly: ExportRenderer(typeof(Editor), typeof(UXDivers.Artina.Shared.ArtinaEditorRenderer))]
[assembly: ExportRenderer(typeof(Label), typeof(IcatuzinhoApp.Droid.CustomFontLabelRenderer))]
[assembly: ExportRenderer(typeof(Switch), typeof(UXDivers.Artina.Shared.ArtinaSwitchRenderer))]
[assembly: ExportRenderer(typeof(ActivityIndicator), typeof(UXDivers.Artina.Shared.ArtinaActivityIndicatorRenderer))]
[assembly: ExportRenderer(typeof(ProgressBar), typeof(UXDivers.Artina.Shared.ArtinaProgressBarRenderer))]
[assembly: ExportRenderer(typeof(Slider), typeof(UXDivers.Artina.Shared.ArtinaSliderRenderer))]
[assembly: ExportRenderer(typeof(SwitchCell), typeof(UXDivers.Artina.Shared.ArtinaSwitchCellRenderer))]
[assembly: ExportRenderer(typeof(TextCell), typeof(UXDivers.Artina.Shared.ArtinaTextCellRenderer))]
[assembly: ExportRenderer(typeof(ImageCell), typeof(UXDivers.Artina.Shared.ArtinaImageCellRenderer))]
[assembly: ExportRenderer(typeof(ViewCell), typeof(UXDivers.Artina.Shared.ArtinaViewCellRenderer))]
[assembly: ExportRenderer(typeof(EntryCell), typeof(UXDivers.Artina.Shared.ArtinaEntryCellRenderer))]
[assembly: ExportRenderer(typeof(SearchBar), typeof(UXDivers.Artina.Shared.ArtinaSearchBarRenderer))]

[assembly: UsesFeature("android.hardware.wifi", Required = false)]