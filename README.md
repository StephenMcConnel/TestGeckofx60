# Test Geckofx60

This program provides a simple C# program for testing Geckofx60.

## Building

This has been built and tested with mono 4.6 as provided by the SIL
mono4 packages.  The commands needed are very simple:

    nuget restore && /opt/mono4-sil/bin/xbuild

## Testing

The RunTest.sh script provides all the environment needed to run the
program.  Testing the program is again a simple command line:

    ./RunTest.sh

