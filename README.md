
# Welcome to the GMP Native Interface for .NET Library
The GMP Native Interface for .NET Library exposes to .NET (through P-Invoke and .NET types) all of
the functionality of the [GNU MP](https://gmplib.org/) Library (version 6.1.2).
It automatically loads at runtime the 32-bit or 64-bit GNU MP library that matches the current CPU
architecture, thus allowing building Visual Studio Projects for AnyCPU, x86, or x64.
It is based on the GNU MP "fat" build which automatically detects the current CPU type, and selects
any available assembly language code optimization for that CPU, thus providing optimal performance. 

### Documentation

- On-line help is available [here](https://machinecognitis.github.io/Math.Gmp.Native/).

### NuGet

The GMP Native Interface for .NET library can be installed in your Visual Studio solution or project
with this [NuGet package](https://www.nuget.org/packages/Math.Gmp.Native.NET/).

### Latest Build

The build file includes the compiled library and the help file (.chm) ready to be included in your project.
The x86 and x64 folders contain the compiled GNU MP Library and must be copied to the same folder as the
Math.Gmp.Native.dll library. The library targets the .NET Framework 4.0, so it can be used with all newer
versions of the .NET Framework.
For other builds, see the [Releases](https://github.com/MachineCognitis/Math.Gmp.Native/releases) page.

- [Math.Gmp.Native.v2.0.build.zip](https://github.com/MachineCognitis/Math.Gmp.Native/releases/download/v2.0/Math.Gmp.Native.v2.0.build.zip).

**NOTE**: On some systems, the content of the ZIP file may be blocked. To unblock it, right click on the
ZIP file, select Properties, and click on the Unblock button, if it is present.

### Making a Donation

You can make a donation to support this project by clicking on the PayPal Donate button below.
PayPal guarantees your privacy and security. I will not receive any details about your payment
other than the amount, and your name.

<a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=WUQ6Q2QC8EVDA"><img src="https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif" border="0" alt="PayPal - The safer, easier way to pay online!"></a>

Thanks to those who have made a donation. This is much appreciated!

### Sharing

<div>
     <!-- Email --> 
    <a href="mailto:?Subject=C.math.NET%20Library&amp;Body=I%20saw%20this%20and%20thought%20of%20you!%20https://github.com/MachineCognitis/Math.Gmp.Native/" target="_blank"> 
        <img width="35" src="./docs/icons/mail.png" alt="Email" /> 
    </a> 
     <!-- Facebook --> 
    <a href="http://www.facebook.com/sharer.php?u=https://github.com/MachineCognitis/Math.Gmp.Native/" target="_blank"> 
        <img width="35"src="./docs/icons/facebook.png" alt="Facebook" /> 
    </a> 
     <!-- Google+ --> 
    <a href="https://plus.google.com/share?url=https://github.com/MachineCognitis/Math.Gmp.Native/" target="_blank"> 
        <img width="35"src="./docs/icons/google.png" alt="Google" /> 
    </a> 
     <!-- LinkedIn --> 
    <a href="http://www.linkedin.com/shareArticle?mini=true&amp;url=https://github.com/MachineCognitis/Math.Gmp.Native/" target="_blank"> 
        <img width="35"src="./docs/icons/linkedin.png" alt="LinkedIn" /> 
    </a> 
    <!-- Twitter --> 
    <a href="https://twitter.com/share?url=https://github.com/MachineCognitis/Math.Gmp.Native/" target="_blank"> 
        <img width="35"src="./docs/icons/twitter.png" alt="Twitter" /> 
    </a> 
</div>


