Microsoft.SilverlightMediaFramework.Diagnostics is responsible for reporting diagnostic data coming from the SSME for smooth streaming videos. 
It is built for performance as to not interfere with video playback while still collecting, aggregating and reporting on the most important in a consumable format.
It is also highly configurable in order to collect only the data you need.

Once attached to an instance of SSME it performs the following functions:
- monitors the trace logs the SSME produces by polling from it at regular intervals on a background thread
- monitors the events that the SSME raises
- picks out the most useful data to report upstream
- aggregates quality data over a sliding sampling window
- aggregates download error data over a sliding sampling window
- reports the raw trace logs upstream
- generates quality snapshots from the most recent quality data