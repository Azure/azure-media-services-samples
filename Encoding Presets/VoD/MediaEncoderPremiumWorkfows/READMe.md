#Media-Encoder-Premium-Workflows

The following sections describe the default workflows.

##H264 Progressive Download MP4

The intent here is to produce an audio-video interleaved MP4 that can be delivered via progressive download to playback devices. This workflow is intended to:

1. Support all allowed input container formats 
1. Perform the following Input video processing: 
	1. Detect and apply de-interlacing automatically
		1. If the source indicates that it is interlaced, the workflow will automatically deinterlace the source
		1. Else if the source indicates that the source is progressive, it will pass through the video without applying any deinterlacing
		1. Else if the source contains no metadata indicating scan type (e.g. an AVI), we will assume the following: 
			- If 23.976 or 24 fps, we will assume progressive 
			- If 59.94 or 60 fps AND image height is 720, we will assume progressive 
			- If 30fps or 29.97, we will assume interlaced and bottom field dominance 
			- If none of the above, we will assume interlaced and top field dominance
1. Video encoding for the output: 
	1. Codec is H.264 
	1. 1-pass CBR, fixed 2 second GOPs
	1. Keep source resolution, and aspect ratio
		- If source image height is greater than or equal to 720, we will assume Rec709 colorspace and a 16:9 aspect ratio; if the source image height is less than 720, we will assume Rec601 colorspace and a 4:3 aspect ratio
	1. Bitrate will be chosen based on source resolution:
i.	4k/UHD content: 12,000 kbps
ii.	1080p:  6500 kbps
iii.	720p:  3500 kbps 
iv.	SD :  2200 kbps
1. Audio encoding: 
	1. Codec is AAC-LC 
	1. 1-pass CBR
	1. Convert sampling rate to 44.1 kHz, sample depth to 16 
	1. Output is stereo – apply downmix to 5.1 sources, pick L/R pair when possible for other oddball sources. 
		- Error out if no logical selection can be made. The first six channels (including multi-track audio) will be considered the active channels; any channels outside of this mapping will be ignored. 
	1. Bitrate: 
		- 4k/UHD content: 256 kbps
		- 1080p content: 192 kbps
		- 720p and lower: 128 kbps
1. Captions:
	1. If source has 608/708, then 608 captions will be extracted from the first valid byte pair identified, and will be embedded into AVC elementary stream per SCTE128.  The 608 caption stream will also be encoded into a side-car SMPTE Timed Text file, .ttml extension, per SMPTE RP2052. 


##H264 Progressive Download MP4 SD

This workflow is intended to be the same as the “H264 Progressive Download MP4.workflow”, except that the output is kept to standard definition irrespective of input resolution. This workflow can be used to generate a standard definition proxy video.

1. Video Encoding (H.264, 1-pass CBR, 2200 kbps, 2sec GOPs)
	1. Case 1: If source is SD (image width is less than 640)
		- The frame size of the output is left unchanged. 
	1. Case 2: If source is HD (image width is 640 or greater)
		- Output framesize is set to 640x360 (16:9 sources) or 640x480 (4:3 sources)
2.	Audio encoding:
	1. Codec is AAC-LC
	1. 1-pass CBR
	1. Convert sampling rate to 44.1 kHz, sample depth to 16
	1. Output is stereo – apply downmix to 5.1 sources, pick L/R pair when possible for other oddball sources. 
		- Error out if no logical selection can be made. The first six channels (including multi-track audio) will be considered the active channels; any channels outside of this mapping will be ignored. 
	1. Bitrate: 128kbps
1. Captions:
	1. If source has 608/708, then 608 captions will be extracted from the first valid byte pair identified, and will be embedded into AVC elementary stream per SCTE128.  The 608 caption stream will also be encoded into a side-car SMPTE Timed Text file, .ttml extension, per SMPTE RP2052.
