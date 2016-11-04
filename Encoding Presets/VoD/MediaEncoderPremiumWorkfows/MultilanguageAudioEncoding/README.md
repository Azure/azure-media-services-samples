# Premium Encoder Workflow Sample - Multiple language audio encoding

This folder contains a sample workflow which can be used to encode a MXF file to a multi MP4 files asset with multiple audio tracks.
This sample uses the concepts of component properties and multiple files as described in [Using multiple input files and component properties with Premium Encoder](https://azure.microsoft.com//en-us/documentation/articles/media-services-media-encoder-premium-workflow-multiplefilesinput/).

This workflow assumes that the MXF file contains one audio track ; the additional audio tracks should be passed as seperate audio files (WAV or MP4...).

To encode, please follow these steps:
- Create a Media Services asset with the MXF file and the Audio files (0 to 18 audio files)
- Make sure that the MXF file is set as a primary file
- Create a job and a task using the Premium Workflow Encoder processor. Use the workflow provided (MultiMP4-1080p-19audio-v1.workflow)
- Pass the setruntime.xml data to the task (if you use Azure Media Services Explorer, use the “pass xml data to the workflow” button).
  - Please update the XML data to specify the correct file names and languages tags
  - The workflow has audio components named Audio 1 to Audio 18
  - RFC5646 is supported for the language tag
- The encoded asset will contain multi language audio tracks and these tracks should be selectable in Azure Media Player
