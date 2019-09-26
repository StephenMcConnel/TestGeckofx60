# Test Geckofx60

This program provides a simple C# program for testing Geckofx60.  It logs
memory use to the console as the user cycles through ten different html files.

## Building

This has been built and tested with mono 5 or 6 as the system mono, and with
mono 4.6 as provided by the SIL mono4 packages.  The commands needed are very
simple:

    nuget restore && msbuild  (using system mono 5 or 6)
or

    nuget restore && /opt/mono4-sil/bin/xbuild  (using mono4-sil)

## Testing

The RunTest.sh script provides all the environment needed to run the
program.  Testing the program is again a simple command line:

    ./RunTest.sh  (using system mono)
or

    ./RunTestMono4Sil.sh  (using mono4-sil)
