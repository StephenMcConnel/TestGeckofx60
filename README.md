# Test Geckofx60

This program provides a simple C# program for testing Geckofx60.  It logs
memory use to the console as the user cycles through eleven different html
files.  It tests the ability to play audio or video through the Geckofx60
browser window on two pages and the ability to run some simple javascript on
one page.

## Building

This has been built and tested with mono 5 or 6 as the system mono, and with
mono 5.16 as provided by the SIL mono5-sil packages.  The commands needed to
build are fairly simple:

    nuget restore Linux/packages.config -SolutionDirectory $(pwd) && msbuild  # using system mono 5 or 6
or

    nuget restore Linux/packages.config -SolutionDirectory $(pwd) && /opt/mono5-sil/bin/xbuild  (using mono5-sil)

## Testing

The RunTest.sh script provides all the environment needed to run the
program.  Testing the program is again a simple command line:

    ./RunTest.sh  (using system mono)
or

    ./RunTestMono5Sil.sh  (using mono5-sil)
