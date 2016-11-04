<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<kayakDocument version="1.2" xml:space="preserve">
    <components>
        <component>
            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
            <propertyDefinition displayName="RFC5646 Language Code" name="language_code" group="Published" dynamic="true">
                <propertyRedirect propertyPath="Language/language_code"/>
            </propertyDefinition>
            <property name="_graphDisplayContents" isNull="true"/>
            <property name="_graphMinDisplaySize" isNull="true"/>
            <property name="_logDebugInfoOnError" isNull="true"/>
            <property name="_parentIgnoresOurErrors" isNull="true"/>
            <property name="_timeBase_local" isNull="true"/>
            <property name="acquireChildLicenses" isNull="true"/>
            <property name="assetPieceNoMetadata" isNull="true"/>
            <property name="clipListXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;clipList&gt;
    &lt;clip&gt;
        &lt;videoSource&gt;
            &lt;mediaFile&gt;
                &lt;file&gt;MainVideo.mxf&lt;/file&gt;
            &lt;/mediaFile&gt;
        &lt;/videoSource&gt;
        &lt;audioSource&gt;
            &lt;mediaFile&gt;
                &lt;file&gt;MainVideo.mxf&lt;/file&gt;
            &lt;/mediaFile&gt;
        &lt;/audioSource&gt;
    &lt;/clip&gt;
    &lt;primaryClipIndex&gt;0&lt;/primaryClipIndex&gt;
&lt;/clipList&gt;
</property>
            <property name="connectionTimeout">5</property>
            <property name="defaultAssetName">DEFAULT</property>
            <property name="defaultInputPin" isNull="true"/>
            <property name="defaultOutputPin" isNull="true"/>
            <property name="graphThreadAffinityMask" isNull="true"/>
            <property name="graphThreadAffinityMode" isNull="true"/>
            <property name="graphThreadAffinityNuma" isNull="true"/>
            <property name="ignoreChildComponentErrors" isNull="true"/>
            <property name="ignoreParentGraphState" isNull="true"/>
            <property name="inactiveTimeout">60</property>
            <property name="language_code">en</property>
            <property name="logsMaxEntries" isNull="true"/>
            <property name="monitorProgress">true</property>
            <property name="outputWriteDirectory">c:\output</property>
            <property name="primarySourceFile">MainVideo.mxf</property>
            <property name="submitTimeout">10</property>
            <property name="tmGroup" isNull="true"/>
            <property name="tmHost" isNull="true"/>
            <property name="tmMoveToPath" isNull="true"/>
            <property name="tmPassword" isNull="true"/>
            <property name="tmPriority" isNull="true"/>
            <property name="tmServerPort" isNull="true"/>
            <property name="tmUsername" isNull="true"/>
            <property name="tmWriteToPath" isNull="true"/>
            <property name="transcodeRequestXml" isNull="true"/>
            <componentName>Transcode Blueprint</componentName>
            <componentDefinitionName>Transcode Task Graph</componentDefinitionName>
            <componentDefinitionGuid>cc2f8f8a-85a3-4522-85a5-b0b26b12f4cd</componentDefinitionGuid>
            <componentOwningPluginName>Media Manager</componentOwningPluginName>
            <componentOwningPluginId>ca.digitalrapids.MediaManager</componentOwningPluginId>
            <childComponents>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="Decode_audio">true</property>
                    <property name="Decode_captions_AFD">true</property>
                    <property name="EndTime" isNull="true"/>
                    <property name="EndTimecode" isNull="true"/>
                    <property name="StartTime" isNull="true"/>
                    <property name="StartTimecode" isNull="true"/>
                    <property name="TargetFrameRate" isNull="true"/>
                    <property name="TrimmingMode">Timestamp</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">47.0,96.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="always_use_directshow">false</property>
                    <property name="blackThreshold">0.10</property>
                    <property name="black_border_detection">false</property>
                    <property name="captions_conform">true</property>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="enable_directshow">false</property>
                    <property name="filename">MainVideo.mxf</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="inspection_max_megabytes" isNull="true"/>
                    <property name="inspection_max_seconds" isNull="true"/>
                    <property name="inspection_mode" isNull="true"/>
                    <property name="logFile" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="max_drift" isNull="true"/>
                    <property name="max_latency">8008/30000</property>
                    <property name="noiseThreshold">0.10</property>
                    <property name="probeDuration">60.0</property>
                    <property name="probeRate">0.10</property>
                    <property name="probeTimeInterval">1.0</property>
                    <property name="truncation">true</property>
                    <property name="validation">true</property>
                    <componentName>Media File Input Video</componentName>
                    <componentDefinitionName>Media File Input</componentDefinitionName>
                    <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                    <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                    <childComponents/>
                    <pin name="filename" type="PROPERTY">
                        <property name="_pinProperty">filename</property>
                    </pin>
                    <pin name="UncompressedVideo" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedVideo" displayName="Uncompressed Video" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode" type="OUTPUT_IO">
                        <pinDefinition name="Timecode" displayName="Timecode (DNxHD)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="CompressedVideo" type="OUTPUT_IO">
                        <pinDefinition name="CompressedVideo" displayName="Compressed Video (DNxHD)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="CompressedAudio" type="OUTPUT_IO">
                        <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="UncompressedAudio" type="OUTPUT_IO">
                        <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode 3" type="OUTPUT_IO">
                        <pinDefinition name="Timecode 3" displayName="Timecode (LTC)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode 4" type="OUTPUT_IO">
                        <pinDefinition name="Timecode 4" displayName="Timecode (VITC)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Data608Service" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAARdAAJRnJhbWVSYXRldAANRGF0YUlzTWlzc2lu
Z3QAC01lZGlhVGltaW5ndAAORGF0YTYwOFNlcnZpY2V0ABREYXRhNjA4U2VydmljZVN0cmVhbXQA
EkRhdGFJc01hbnVmYWN0dXJlZHQAEVNhbXBsZUluZm9ybWF0aW9udAALRGF0YVNlcnZpY2V0ABRE
YXRhNjA4U2VydmljZVNhbXBsZXQAEURhdGFTZXJ2aWNlU2FtcGxldAAJQ29udGFpbmVydAAIVGVt
cG9yYWx0AAtLYXlha0J1ZmZlcnQACkJ5dGVTdHJlYW10AAtNZWRpYU9yaWdpbnQAEURhdGFTZXJ2
aWNlU3RyZWFtdAAGU3RyZWFteHNxAH4ACHcMAAAAED9AAAAAAAAMc3IAT2NhLmRpZ2l0YWxyYXBp
ZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJFNp
bXBsZVR5cGUAAAAAAAAAAQIAAkwAEWVudW1lcmF0aW9uVmFsdWVzdAAQTGphdmEvdXRpbC9MaXN0
O0wABHR5cGV0AENMY2EvZGlnaXRhbHJhcGlkcy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9t
b2RlbC9TaW1wbGVUeXBlc0VudW07eHIAUmNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVz
LmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJEtleURlZmluaXRpb24AAAAAAAAA
AQIABEwAB2NvbW1lbnRxAH4ABUwAC2Rpc3BsYXlOYW1lcQB+AAVMAAttdWx0aVZhbHVlZHQAE0xq
YXZhL2xhbmcvQm9vbGVhbjtMAARuYW1lcQB+AAV4cHQASVRoZSB0aW1lIHBlcnRhaW5pbmcgdG8g
dGhlIGVuZCBvZiB0aGUgZGF0YSAodGltZSArIGR1cmF0aW9uIG9mIHRoaXMgZGF0YSlwcHQAB3Rp
bWVFbmRwfnIAQWNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9k
ZWwuU2ltcGxlVHlwZXNFbnVtAAAAAAAAAAASAAB4cgAOamF2YS5sYW5nLkVudW0AAAAAAAAAABIA
AHhwdAAEVElNRXNxAH4AHHQAHEluZGljYXRlcyB0aGUgbWVkaWEgZHVyYXRpb25wcHQADm1lZGlh
X2R1cmF0aW9ucHEAfgAmc3EAfgAccHBwdAAPZGF0YV9pc19taXNzaW5ncH5xAH4AJHQAB0JPT0xF
QU5zcQB+ABx0ADNQb3NpdGlvbiBvciBvZmZzZXQgZnJvbSB0aGUgYmVnaW5uaW5nIG9mIHRoZSBz
dHJlYW1wcHQAEHBvc2l0aW9uSW5TdHJlYW1wfnEAfgAkdAAETE9OR3NxAH4AHHBwc3IAEWphdmEu
bGFuZy5Cb29sZWFuzSBygNWc+u4CAAFaAAV2YWx1ZXhwAXQADG1lZGlhX29yaWdpbnNyABNqYXZh
LnV0aWwuQXJyYXlMaXN0eIHSHZnHYZ0DAAFJAARzaXpleHAAAAAXdwQAAAAXc3IAN2NhLmRpZ2l0
YWxyYXBpZHMua2F5YWsucGx1Z2luLnhtbC5LYXlha0VudW1lcmF0aW9uVmFsdWUAAAAAAAAAAQIA
BUwAC2Rlc2NyaXB0aW9ucQB+AAVMAAtkaXNwbGF5TmFtZXEAfgAFTAAGaGlkZGVucQB+ACBMAA52
YWx1ZUF0dHJpYnV0ZXEAfgAFTAANdmFsdWVFbWJlZGRlZHEAfgAFeHBwcHBwdAAMTWFudWZhY3R1
cmVkc3EAfgA6cHQACERSQ1ZpZGVvcHEAfgA+dAAAc3EAfgA6cHQAA0dYRnBxAH4AQXEAfgA/c3EA
fgA6cHQAA0xYRnBxAH4AQ3EAfgA/c3EAfgA6cHQAA01YRnBxAH4ARXEAfgA/c3EAfgA6cHQACVF1
aWNrVGltZXBxAH4AR3EAfgA/c3EAfgA6cHQADVdpbmRvd3MgTWVkaWFwdAAMV2luZG93c01lZGlh
cQB+AD9zcQB+ADpwdAAJVXNlciBEYXRhcHQACVVzZXJfZGF0YXEAfgA/c3EAfgA6cHQADkFuY2ls
bGFyeSBEYXRhcHQADkFuY2lsbGFyeV9kYXRhcQB+AD9zcQB+ADpwdAACRFZwcQB+AFJxAH4AP3Nx
AH4AOnB0AANWQzNwcQB+AFRxAH4AP3NxAH4AOnB0ABZBVkMgUGljdHVyZSBUaW1pbmcgU0VJcHQA
FkFWQ19QaWN0dXJlX1RpbWluZ19TRUlxAH4AP3NxAH4AOnB0ABBNUEVHMiBHT1AgSGVhZGVycHQA
EE1QRUcyX0dPUF9oZWFkZXJxAH4AP3NxAH4AOnB0ABRNWEYgTWF0ZXJpYWwgUGFja2FnZXB0ABRN
WEZfbWF0ZXJpYWxfcGFja2FnZXEAfgA/c3EAfgA6cHQAEE1YRiBTeXN0ZW0gVHJhY2twdAAQTVhG
X3N5c3RlbV90cmFja3EAfgA/c3EAfgA6cHQAE0FuY2lsbGFyeSBEYXRhIFZJVENwdAATQW5jaWxs
YXJ5X2RhdGFfVklUQ3EAfgA/c3EAfgA6cHQAEkFuY2lsbGFyeSBEYXRhIExUQ3B0ABJBbmNpbGxh
cnlfZGF0YV9MVENxAH4AP3NxAH4AOnB0ABFIRVZDIFRpbWVjb2RlIFNFSXB0ABFIRVZDX1RpbWVj
b2RlX1NFSXEAfgA/c3EAfgA6cHQAEFNDVEUyMCBVc2VyIERhdGFwdAAQVXNlcl9kYXRhX1NDVEUy
MHEAfgA/c3EAfgA6cHQADkFUU0MgVXNlciBEYXRhcHQADlVzZXJfZGF0YV9BVFNDcQB+AD9zcQB+
ADpwcQB+AG5wdAADU0NDcQB+AD9zcQB+ADpwdAAZQW5jaWxsYXJ5IERhdGEgTGVnYWN5IDYwOHB0
ABlBbmNpbGxhcnlfZGF0YV9sZWdhY3lfNjA4cQB+AD9zcQB+ADpwdAANQ3VlIFBvaW50IFhNTHB0
AAtDdWVQb2ludFhNTHEAfgA/eH5xAH4AJHQABlNUUklOR3NxAH4AHHQAQVRydWUgb24gdGhlIGxh
c3QgZGF0YSBwYWNrZXQgb2YgdGhlIFN0cmVhbSAod2l0aCBvciB3aXRob3V0IGRhdGEpcHB0AAtl
bmRPZlN0cmVhbXBxAH4ALXNxAH4AHHQALEluZGljYXRlcyBpZiB0aGUgZnJhbWUgcmF0ZSByZW1h
aW5zIGNvbnN0YW50cHB0ABNjb25zdGFudF9mcmFtZV9yYXRlcHEAfgAtc3IAUGNhLmRpZ2l0YWxy
YXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9u
JENvbXBsZXhUeXBlAAAAAAAAAAECAAJMAAhvcHRpb25hbHEAfgAgTAAEdHlwZXEAfgAFeHEAfgAf
dAAhRGV0YWlsIGluZm9ybWF0aW9uIGZvciBlYWNoIHRyYWNrcHEAfgA2dAAIc2VydmljZXNwdAAO
RGF0YTYwOENvbnRlbnRzcQB+ABx0AC1UaGUgbW9zdCByZWxldmFudCB0aW1lIHBlcnRhaW5pbmcg
dG8gdGhlIGRhdGFwcHQABHRpbWVwcQB+ACZzcQB+ABx0ABhJbmRpY2F0ZXMgdGhlIGZyYW1lIHJh
dGVwcHQACmZyYW1lX3JhdGVwfnEAfgAkdAAIUkFUSU9OQUxzcQB+ABxwcHB0ABRkYXRhX2lzX21h
bnVmYWN0dXJlZHBxAH4ALXNxAH4AHHQAI1RvdGFsIGxlbmd0aCBvZiB0aGUgc3RyZWFtIGlmIGtu
b3ducHB0AA5sZW5ndGhPZlN0cmVhbXBxAH4AMnhwc3IAEWphdmEudXRpbC5IYXNoTWFwBQfawcMW
YNEDAAJGAApsb2FkRmFjdG9ySQAJdGhyZXNob2xkeHA/AAAAAAAAIHcIAAAAQAAAAAJxAH4AN3Ny
ACZqYXZhLnV0aWwuQ29sbGVjdGlvbnMkVW5tb2RpZmlhYmxlTGlzdPwPJTG17I4QAgABTAAEbGlz
dHEAfgAdeHIALGphdmEudXRpbC5Db2xsZWN0aW9ucyRVbm1vZGlmaWFibGVDb2xsZWN0aW9uGUIA
gMte9x4CAAFMAAFjdAAWTGphdmEvdXRpbC9Db2xsZWN0aW9uO3hwc3EAfgA4AAAAAXcEAAAAAXEA
fgB0eHEAfgCYcQB+AINzcQB+AJRzcQB+ADgAAAAAdwQAAAAAeHEAfgCaeA==</property>
                        <pinDefinition name="Data608Service" displayName="EIA-608 Captions (Ancillary Data)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Data608Service 2" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAARdAAJRnJhbWVSYXRldAANRGF0YUlzTWlzc2lu
Z3QAC01lZGlhVGltaW5ndAAORGF0YTYwOFNlcnZpY2V0ABREYXRhNjA4U2VydmljZVN0cmVhbXQA
EkRhdGFJc01hbnVmYWN0dXJlZHQAEVNhbXBsZUluZm9ybWF0aW9udAALRGF0YVNlcnZpY2V0ABRE
YXRhNjA4U2VydmljZVNhbXBsZXQAEURhdGFTZXJ2aWNlU2FtcGxldAAJQ29udGFpbmVydAAIVGVt
cG9yYWx0AAtLYXlha0J1ZmZlcnQACkJ5dGVTdHJlYW10AAtNZWRpYU9yaWdpbnQAEURhdGFTZXJ2
aWNlU3RyZWFtdAAGU3RyZWFteHNxAH4ACHcMAAAAED9AAAAAAAAMc3IAT2NhLmRpZ2l0YWxyYXBp
ZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJFNp
bXBsZVR5cGUAAAAAAAAAAQIAAkwAEWVudW1lcmF0aW9uVmFsdWVzdAAQTGphdmEvdXRpbC9MaXN0
O0wABHR5cGV0AENMY2EvZGlnaXRhbHJhcGlkcy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9t
b2RlbC9TaW1wbGVUeXBlc0VudW07eHIAUmNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVz
LmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJEtleURlZmluaXRpb24AAAAAAAAA
AQIABEwAB2NvbW1lbnRxAH4ABUwAC2Rpc3BsYXlOYW1lcQB+AAVMAAttdWx0aVZhbHVlZHQAE0xq
YXZhL2xhbmcvQm9vbGVhbjtMAARuYW1lcQB+AAV4cHQASVRoZSB0aW1lIHBlcnRhaW5pbmcgdG8g
dGhlIGVuZCBvZiB0aGUgZGF0YSAodGltZSArIGR1cmF0aW9uIG9mIHRoaXMgZGF0YSlwcHQAB3Rp
bWVFbmRwfnIAQWNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9k
ZWwuU2ltcGxlVHlwZXNFbnVtAAAAAAAAAAASAAB4cgAOamF2YS5sYW5nLkVudW0AAAAAAAAAABIA
AHhwdAAEVElNRXNxAH4AHHQAHEluZGljYXRlcyB0aGUgbWVkaWEgZHVyYXRpb25wcHQADm1lZGlh
X2R1cmF0aW9ucHEAfgAmc3EAfgAccHBwdAAPZGF0YV9pc19taXNzaW5ncH5xAH4AJHQAB0JPT0xF
QU5zcQB+ABxwcHNyABFqYXZhLmxhbmcuQm9vbGVhbs0gcoDVnPruAgABWgAFdmFsdWV4cAF0AAxt
ZWRpYV9vcmlnaW5zcgATamF2YS51dGlsLkFycmF5TGlzdHiB0h2Zx2GdAwABSQAEc2l6ZXhwAAAA
F3cEAAAAF3NyADdjYS5kaWdpdGFscmFwaWRzLmtheWFrLnBsdWdpbi54bWwuS2F5YWtFbnVtZXJh
dGlvblZhbHVlAAAAAAAAAAECAAVMAAtkZXNjcmlwdGlvbnEAfgAFTAALZGlzcGxheU5hbWVxAH4A
BUwABmhpZGRlbnEAfgAgTAAOdmFsdWVBdHRyaWJ1dGVxAH4ABUwADXZhbHVlRW1iZWRkZWRxAH4A
BXhwcHBwcHQADE1hbnVmYWN0dXJlZHNxAH4ANXB0AAhEUkNWaWRlb3BxAH4AOXQAAHNxAH4ANXB0
AANHWEZwcQB+ADxxAH4AOnNxAH4ANXB0AANMWEZwcQB+AD5xAH4AOnNxAH4ANXB0AANNWEZwcQB+
AEBxAH4AOnNxAH4ANXB0AAlRdWlja1RpbWVwcQB+AEJxAH4AOnNxAH4ANXB0AA1XaW5kb3dzIE1l
ZGlhcHQADFdpbmRvd3NNZWRpYXEAfgA6c3EAfgA1cHQACVVzZXIgRGF0YXB0AAlVc2VyX2RhdGFx
AH4AOnNxAH4ANXB0AA5BbmNpbGxhcnkgRGF0YXB0AA5BbmNpbGxhcnlfZGF0YXEAfgA6c3EAfgA1
cHQAAkRWcHEAfgBNcQB+ADpzcQB+ADVwdAADVkMzcHEAfgBPcQB+ADpzcQB+ADVwdAAWQVZDIFBp
Y3R1cmUgVGltaW5nIFNFSXB0ABZBVkNfUGljdHVyZV9UaW1pbmdfU0VJcQB+ADpzcQB+ADVwdAAQ
TVBFRzIgR09QIEhlYWRlcnB0ABBNUEVHMl9HT1BfaGVhZGVycQB+ADpzcQB+ADVwdAAUTVhGIE1h
dGVyaWFsIFBhY2thZ2VwdAAUTVhGX21hdGVyaWFsX3BhY2thZ2VxAH4AOnNxAH4ANXB0ABBNWEYg
U3lzdGVtIFRyYWNrcHQAEE1YRl9zeXN0ZW1fdHJhY2txAH4AOnNxAH4ANXB0ABNBbmNpbGxhcnkg
RGF0YSBWSVRDcHQAE0FuY2lsbGFyeV9kYXRhX1ZJVENxAH4AOnNxAH4ANXB0ABJBbmNpbGxhcnkg
RGF0YSBMVENwdAASQW5jaWxsYXJ5X2RhdGFfTFRDcQB+ADpzcQB+ADVwdAARSEVWQyBUaW1lY29k
ZSBTRUlwdAARSEVWQ19UaW1lY29kZV9TRUlxAH4AOnNxAH4ANXB0ABBTQ1RFMjAgVXNlciBEYXRh
cHQAEFVzZXJfZGF0YV9TQ1RFMjBxAH4AOnNxAH4ANXB0AA5BVFNDIFVzZXIgRGF0YXB0AA5Vc2Vy
X2RhdGFfQVRTQ3EAfgA6c3EAfgA1cHEAfgBpcHQAA1NDQ3EAfgA6c3EAfgA1cHQAGUFuY2lsbGFy
eSBEYXRhIExlZ2FjeSA2MDhwdAAZQW5jaWxsYXJ5X2RhdGFfbGVnYWN5XzYwOHEAfgA6c3EAfgA1
cHQADUN1ZSBQb2ludCBYTUxwdAALQ3VlUG9pbnRYTUxxAH4AOnh+cQB+ACR0AAZTVFJJTkdzcQB+
ABx0ADNQb3NpdGlvbiBvciBvZmZzZXQgZnJvbSB0aGUgYmVnaW5uaW5nIG9mIHRoZSBzdHJlYW1w
cHQAEHBvc2l0aW9uSW5TdHJlYW1wfnEAfgAkdAAETE9OR3NxAH4AHHQAQVRydWUgb24gdGhlIGxh
c3QgZGF0YSBwYWNrZXQgb2YgdGhlIFN0cmVhbSAod2l0aCBvciB3aXRob3V0IGRhdGEpcHB0AAtl
bmRPZlN0cmVhbXBxAH4ALXNxAH4AHHQALEluZGljYXRlcyBpZiB0aGUgZnJhbWUgcmF0ZSByZW1h
aW5zIGNvbnN0YW50cHB0ABNjb25zdGFudF9mcmFtZV9yYXRlcHEAfgAtc3IAUGNhLmRpZ2l0YWxy
YXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9u
JENvbXBsZXhUeXBlAAAAAAAAAAECAAJMAAhvcHRpb25hbHEAfgAgTAAEdHlwZXEAfgAFeHEAfgAf
dAAhRGV0YWlsIGluZm9ybWF0aW9uIGZvciBlYWNoIHRyYWNrcHEAfgAxdAAIc2VydmljZXNwdAAO
RGF0YTYwOENvbnRlbnRzcQB+ABx0AC1UaGUgbW9zdCByZWxldmFudCB0aW1lIHBlcnRhaW5pbmcg
dG8gdGhlIGRhdGFwcHQABHRpbWVwcQB+ACZzcQB+ABx0ABhJbmRpY2F0ZXMgdGhlIGZyYW1lIHJh
dGVwcHQACmZyYW1lX3JhdGVwfnEAfgAkdAAIUkFUSU9OQUxzcQB+ABxwcHB0ABRkYXRhX2lzX21h
bnVmYWN0dXJlZHBxAH4ALXNxAH4AHHQAI1RvdGFsIGxlbmd0aCBvZiB0aGUgc3RyZWFtIGlmIGtu
b3ducHB0AA5sZW5ndGhPZlN0cmVhbXBxAH4AeHhwc3IAEWphdmEudXRpbC5IYXNoTWFwBQfawcMW
YNEDAAJGAApsb2FkRmFjdG9ySQAJdGhyZXNob2xkeHA/AAAAAAAAIHcIAAAAQAAAAAJxAH4AMnNy
ACZqYXZhLnV0aWwuQ29sbGVjdGlvbnMkVW5tb2RpZmlhYmxlTGlzdPwPJTG17I4QAgABTAAEbGlz
dHEAfgAdeHIALGphdmEudXRpbC5Db2xsZWN0aW9ucyRVbm1vZGlmaWFibGVDb2xsZWN0aW9uGUIA
gMte9x4CAAFMAAFjdAAWTGphdmEvdXRpbC9Db2xsZWN0aW9uO3hwc3EAfgAzAAAAAXcEAAAAAXEA
fgBLeHEAfgCYcQB+AINzcQB+AJRzcQB+ADMAAAAAdwQAAAAAeHEAfgCaeA==</property>
                        <pinDefinition name="Data608Service 2" displayName="EIA-608 Captions (708 compatibility bytes)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Data708Service" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAARdAAJRnJhbWVSYXRldAANRGF0YUlzTWlzc2lu
Z3QAC01lZGlhVGltaW5ndAAURGF0YTcwOFNlcnZpY2VTYW1wbGV0ABJEYXRhSXNNYW51ZmFjdHVy
ZWR0ABFTYW1wbGVJbmZvcm1hdGlvbnQADkRhdGE3MDhTZXJ2aWNldAALRGF0YVNlcnZpY2V0ABFE
YXRhU2VydmljZVNhbXBsZXQACUNvbnRhaW5lcnQACFRlbXBvcmFsdAALS2F5YWtCdWZmZXJ0AApC
eXRlU3RyZWFtdAALTWVkaWFPcmlnaW50ABFEYXRhU2VydmljZVN0cmVhbXQABlN0cmVhbXQAFERh
dGE3MDhTZXJ2aWNlU3RyZWFteHNxAH4ACHcMAAAAID9AAAAAAAANc3IAT2NhLmRpZ2l0YWxyYXBp
ZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJFNp
bXBsZVR5cGUAAAAAAAAAAQIAAkwAEWVudW1lcmF0aW9uVmFsdWVzdAAQTGphdmEvdXRpbC9MaXN0
O0wABHR5cGV0AENMY2EvZGlnaXRhbHJhcGlkcy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9t
b2RlbC9TaW1wbGVUeXBlc0VudW07eHIAUmNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVz
LmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJEtleURlZmluaXRpb24AAAAAAAAA
AQIABEwAB2NvbW1lbnRxAH4ABUwAC2Rpc3BsYXlOYW1lcQB+AAVMAAttdWx0aVZhbHVlZHQAE0xq
YXZhL2xhbmcvQm9vbGVhbjtMAARuYW1lcQB+AAV4cHQASVRoZSB0aW1lIHBlcnRhaW5pbmcgdG8g
dGhlIGVuZCBvZiB0aGUgZGF0YSAodGltZSArIGR1cmF0aW9uIG9mIHRoaXMgZGF0YSlwcHQAB3Rp
bWVFbmRwfnIAQWNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9k
ZWwuU2ltcGxlVHlwZXNFbnVtAAAAAAAAAAASAAB4cgAOamF2YS5sYW5nLkVudW0AAAAAAAAAABIA
AHhwdAAEVElNRXNxAH4AHHQAHEluZGljYXRlcyB0aGUgbWVkaWEgZHVyYXRpb25wcHQADm1lZGlh
X2R1cmF0aW9ucHEAfgAmc3EAfgAccHBzcgARamF2YS5sYW5nLkJvb2xlYW7NIHKA1Zz67gIAAVoA
BXZhbHVleHABdAAMbWVkaWFfb3JpZ2luc3IAE2phdmEudXRpbC5BcnJheUxpc3R4gdIdmcdhnQMA
AUkABHNpemV4cAAAABd3BAAAABdzcgA3Y2EuZGlnaXRhbHJhcGlkcy5rYXlhay5wbHVnaW4ueG1s
LktheWFrRW51bWVyYXRpb25WYWx1ZQAAAAAAAAABAgAFTAALZGVzY3JpcHRpb25xAH4ABUwAC2Rp
c3BsYXlOYW1lcQB+AAVMAAZoaWRkZW5xAH4AIEwADnZhbHVlQXR0cmlidXRlcQB+AAVMAA12YWx1
ZUVtYmVkZGVkcQB+AAV4cHBwcHB0AAxNYW51ZmFjdHVyZWRzcQB+ADFwdAAIRFJDVmlkZW9wcQB+
ADV0AABzcQB+ADFwdAADR1hGcHEAfgA4cQB+ADZzcQB+ADFwdAADTFhGcHEAfgA6cQB+ADZzcQB+
ADFwdAADTVhGcHEAfgA8cQB+ADZzcQB+ADFwdAAJUXVpY2tUaW1lcHEAfgA+cQB+ADZzcQB+ADFw
dAANV2luZG93cyBNZWRpYXB0AAxXaW5kb3dzTWVkaWFxAH4ANnNxAH4AMXB0AAlVc2VyIERhdGFw
dAAJVXNlcl9kYXRhcQB+ADZzcQB+ADFwdAAOQW5jaWxsYXJ5IERhdGFwdAAOQW5jaWxsYXJ5X2Rh
dGFxAH4ANnNxAH4AMXB0AAJEVnBxAH4ASXEAfgA2c3EAfgAxcHQAA1ZDM3BxAH4AS3EAfgA2c3EA
fgAxcHQAFkFWQyBQaWN0dXJlIFRpbWluZyBTRUlwdAAWQVZDX1BpY3R1cmVfVGltaW5nX1NFSXEA
fgA2c3EAfgAxcHQAEE1QRUcyIEdPUCBIZWFkZXJwdAAQTVBFRzJfR09QX2hlYWRlcnEAfgA2c3EA
fgAxcHQAFE1YRiBNYXRlcmlhbCBQYWNrYWdlcHQAFE1YRl9tYXRlcmlhbF9wYWNrYWdlcQB+ADZz
cQB+ADFwdAAQTVhGIFN5c3RlbSBUcmFja3B0ABBNWEZfc3lzdGVtX3RyYWNrcQB+ADZzcQB+ADFw
dAATQW5jaWxsYXJ5IERhdGEgVklUQ3B0ABNBbmNpbGxhcnlfZGF0YV9WSVRDcQB+ADZzcQB+ADFw
dAASQW5jaWxsYXJ5IERhdGEgTFRDcHQAEkFuY2lsbGFyeV9kYXRhX0xUQ3EAfgA2c3EAfgAxcHQA
EUhFVkMgVGltZWNvZGUgU0VJcHQAEUhFVkNfVGltZWNvZGVfU0VJcQB+ADZzcQB+ADFwdAAQU0NU
RTIwIFVzZXIgRGF0YXB0ABBVc2VyX2RhdGFfU0NURTIwcQB+ADZzcQB+ADFwdAAOQVRTQyBVc2Vy
IERhdGFwdAAOVXNlcl9kYXRhX0FUU0NxAH4ANnNxAH4AMXBxAH4AZXB0AANTQ0NxAH4ANnNxAH4A
MXB0ABlBbmNpbGxhcnkgRGF0YSBMZWdhY3kgNjA4cHQAGUFuY2lsbGFyeV9kYXRhX2xlZ2FjeV82
MDhxAH4ANnNxAH4AMXB0AA1DdWUgUG9pbnQgWE1McHQAC0N1ZVBvaW50WE1McQB+ADZ4fnEAfgAk
dAAGU1RSSU5Hc3EAfgAcdABBVHJ1ZSBvbiB0aGUgbGFzdCBkYXRhIHBhY2tldCBvZiB0aGUgU3Ry
ZWFtICh3aXRoIG9yIHdpdGhvdXQgZGF0YSlwcHQAC2VuZE9mU3RyZWFtcH5xAH4AJHQAB0JPT0xF
QU5zcQB+ABx0ACxJbmRpY2F0ZXMgaWYgdGhlIGZyYW1lIHJhdGUgcmVtYWlucyBjb25zdGFudHBw
dAATY29uc3RhbnRfZnJhbWVfcmF0ZXBxAH4AdHNyAFBjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRh
dGF0eXBlcy5kZWZpbml0aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlvbiRDb21wbGV4VHlwZQAA
AAAAAAABAgACTAAIb3B0aW9uYWxxAH4AIEwABHR5cGVxAH4ABXhxAH4AH3BwcQB+AC10AAxjZHBf
c2VydmljZXNwdAAORGF0YTYwOENvbnRlbnRzcQB+ABx0ABhJbmRpY2F0ZXMgdGhlIGZyYW1lIHJh
dGVwcHQACmZyYW1lX3JhdGVwfnEAfgAkdAAIUkFUSU9OQUxzcQB+AHl0AC1PcHRpb25hbCB0aW1l
Y29kZSBpbmZvcm1hdGlvbiBpbiB0aGUgNzA4IENEUHNwc3EAfgAsAHQADGNkcF90aW1lY29kZXEA
fgAtdAAIVGltZWNvZGVzcQB+ABxwcHB0AA9kYXRhX2lzX21pc3NpbmdwcQB+AHRzcQB+ABx0ADNQ
b3NpdGlvbiBvciBvZmZzZXQgZnJvbSB0aGUgYmVnaW5uaW5nIG9mIHRoZSBzdHJlYW1wcHQAEHBv
c2l0aW9uSW5TdHJlYW1wfnEAfgAkdAAETE9OR3NxAH4AHHQALVRoZSBtb3N0IHJlbGV2YW50IHRp
bWUgcGVydGFpbmluZyB0byB0aGUgZGF0YXBwdAAEdGltZXBxAH4AJnNxAH4AHHBwcHQAFGRhdGFf
aXNfbWFudWZhY3R1cmVkcHEAfgB0c3EAfgAcdAAjVG90YWwgbGVuZ3RoIG9mIHRoZSBzdHJlYW0g
aWYga25vd25wcHQADmxlbmd0aE9mU3RyZWFtcHEAfgCMeHBzcgARamF2YS51dGlsLkhhc2hNYXAF
B9rBwxZg0QMAAkYACmxvYWRGYWN0b3JJAAl0aHJlc2hvbGR4cD8AAAAAAAAgdwgAAABAAAAAAnEA
fgAuc3IAJmphdmEudXRpbC5Db2xsZWN0aW9ucyRVbm1vZGlmaWFibGVMaXN0/A8lMbXsjhACAAFM
AARsaXN0cQB+AB14cgAsamF2YS51dGlsLkNvbGxlY3Rpb25zJFVubW9kaWZpYWJsZUNvbGxlY3Rp
b24ZQgCAy173HgIAAUwAAWN0ABZMamF2YS91dGlsL0NvbGxlY3Rpb247eHBzcQB+AC8AAAABdwQA
AAABcQB+AEd4cQB+AJxxAH4Ae3NxAH4AmHNxAH4ALwAAAAB3BAAAAAB4cQB+AJ54</property>
                        <pinDefinition name="Data708Service" displayName="EIA-708 Captions (Ancillary Data)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="TeletextData" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAASdAAJRnJhbWVSYXRldAASVGVsZXRleHREYXRh
U3RyZWFtdAANRGF0YUlzTWlzc2luZ3QAC01lZGlhVGltaW5ndAASRGF0YUlzTWFudWZhY3R1cmVk
dAARU2FtcGxlSW5mb3JtYXRpb250AAtEYXRhU2VydmljZXQAEURhdGFTZXJ2aWNlU2FtcGxldAAJ
Q29udGFpbmVydAATVGVsZXRleHREYXRhU2VydmljZXQACFRlbXBvcmFsdAAITGFuZ3VhZ2V0AAtL
YXlha0J1ZmZlcnQACkJ5dGVTdHJlYW10AAtNZWRpYU9yaWdpbnQAEURhdGFTZXJ2aWNlU3RyZWFt
dAAGU3RyZWFtdAASVGVsZXRleHREYXRhU2FtcGxleHNxAH4ACHcMAAAAID9AAAAAAAAPc3IAT2Nh
LmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVE
ZWZpbml0aW9uJFNpbXBsZVR5cGUAAAAAAAAAAQIAAkwAEWVudW1lcmF0aW9uVmFsdWVzdAAQTGph
dmEvdXRpbC9MaXN0O0wABHR5cGV0AENMY2EvZGlnaXRhbHJhcGlkcy9rYXlhay9kYXRhdHlwZXMv
ZGVmaW5pdGlvbi9tb2RlbC9TaW1wbGVUeXBlc0VudW07eHIAUmNhLmRpZ2l0YWxyYXBpZHMua2F5
YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJEtleURlZmlu
aXRpb24AAAAAAAAAAQIABEwAB2NvbW1lbnRxAH4ABUwAC2Rpc3BsYXlOYW1lcQB+AAVMAAttdWx0
aVZhbHVlZHQAE0xqYXZhL2xhbmcvQm9vbGVhbjtMAARuYW1lcQB+AAV4cHQASVRoZSB0aW1lIHBl
cnRhaW5pbmcgdG8gdGhlIGVuZCBvZiB0aGUgZGF0YSAodGltZSArIGR1cmF0aW9uIG9mIHRoaXMg
ZGF0YSlwcHQAB3RpbWVFbmRwfnIAQWNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRl
ZmluaXRpb24ubW9kZWwuU2ltcGxlVHlwZXNFbnVtAAAAAAAAAAASAAB4cgAOamF2YS5sYW5nLkVu
dW0AAAAAAAAAABIAAHhwdAAEVElNRXNxAH4AHXQAHEluZGljYXRlcyB0aGUgbWVkaWEgZHVyYXRp
b25wcHQADm1lZGlhX2R1cmF0aW9ucHEAfgAnc3EAfgAddABASW5kaWNhdGVzIHRoZSBwcmVzZW50
YXRpb24gbGV2ZWwgaW4gdXNlIGluIHRoaXMgVGVsZXRleHQgc2VydmljZXBwdAAbdGVsZXRleHRf
cHJlc2VudGF0aW9uX2xldmVsc3IAE2phdmEudXRpbC5BcnJheUxpc3R4gdIdmcdhnQMAAUkABHNp
emV4cAAAAAR3BAAAAARzcgA3Y2EuZGlnaXRhbHJhcGlkcy5rYXlhay5wbHVnaW4ueG1sLktheWFr
RW51bWVyYXRpb25WYWx1ZQAAAAAAAAABAgAFTAALZGVzY3JpcHRpb25xAH4ABUwAC2Rpc3BsYXlO
YW1lcQB+AAVMAAZoaWRkZW5xAH4AIUwADnZhbHVlQXR0cmlidXRlcQB+AAVMAA12YWx1ZUVtYmVk
ZGVkcQB+AAV4cHB0AAExcHEAfgAzdAAAc3EAfgAxcHQAAzEuNXB0AAMxXzVxAH4ANHNxAH4AMXB0
AAMyLjVwdAADMl81cQB+ADRzcQB+ADFwdAADMy41cHQAAzNfNXEAfgA0eH5xAH4AJXQABlNUUklO
R3NxAH4AHXBwc3IAEWphdmEubGFuZy5Cb29sZWFuzSBygNWc+u4CAAFaAAV2YWx1ZXhwAXQADG1l
ZGlhX29yaWdpbnNxAH4ALwAAABd3BAAAABdzcQB+ADFwcHBwdAAMTWFudWZhY3R1cmVkc3EAfgAx
cHQACERSQ1ZpZGVvcHEAfgBIcQB+ADRzcQB+ADFwdAADR1hGcHEAfgBKcQB+ADRzcQB+ADFwdAAD
TFhGcHEAfgBMcQB+ADRzcQB+ADFwdAADTVhGcHEAfgBOcQB+ADRzcQB+ADFwdAAJUXVpY2tUaW1l
cHEAfgBQcQB+ADRzcQB+ADFwdAANV2luZG93cyBNZWRpYXB0AAxXaW5kb3dzTWVkaWFxAH4ANHNx
AH4AMXB0AAlVc2VyIERhdGFwdAAJVXNlcl9kYXRhcQB+ADRzcQB+ADFwdAAOQW5jaWxsYXJ5IERh
dGFwdAAOQW5jaWxsYXJ5X2RhdGFxAH4ANHNxAH4AMXB0AAJEVnBxAH4AW3EAfgA0c3EAfgAxcHQA
A1ZDM3BxAH4AXXEAfgA0c3EAfgAxcHQAFkFWQyBQaWN0dXJlIFRpbWluZyBTRUlwdAAWQVZDX1Bp
Y3R1cmVfVGltaW5nX1NFSXEAfgA0c3EAfgAxcHQAEE1QRUcyIEdPUCBIZWFkZXJwdAAQTVBFRzJf
R09QX2hlYWRlcnEAfgA0c3EAfgAxcHQAFE1YRiBNYXRlcmlhbCBQYWNrYWdlcHQAFE1YRl9tYXRl
cmlhbF9wYWNrYWdlcQB+ADRzcQB+ADFwdAAQTVhGIFN5c3RlbSBUcmFja3B0ABBNWEZfc3lzdGVt
X3RyYWNrcQB+ADRzcQB+ADFwdAATQW5jaWxsYXJ5IERhdGEgVklUQ3B0ABNBbmNpbGxhcnlfZGF0
YV9WSVRDcQB+ADRzcQB+ADFwdAASQW5jaWxsYXJ5IERhdGEgTFRDcHQAEkFuY2lsbGFyeV9kYXRh
X0xUQ3EAfgA0c3EAfgAxcHQAEUhFVkMgVGltZWNvZGUgU0VJcHQAEUhFVkNfVGltZWNvZGVfU0VJ
cQB+ADRzcQB+ADFwdAAQU0NURTIwIFVzZXIgRGF0YXB0ABBVc2VyX2RhdGFfU0NURTIwcQB+ADRz
cQB+ADFwdAAOQVRTQyBVc2VyIERhdGFwdAAOVXNlcl9kYXRhX0FUU0NxAH4ANHNxAH4AMXBxAH4A
d3B0AANTQ0NxAH4ANHNxAH4AMXB0ABlBbmNpbGxhcnkgRGF0YSBMZWdhY3kgNjA4cHQAGUFuY2ls
bGFyeV9kYXRhX2xlZ2FjeV82MDhxAH4ANHNxAH4AMXB0AA1DdWUgUG9pbnQgWE1McHQAC0N1ZVBv
aW50WE1McQB+ADR4cQB+AD5zcQB+AB10AEdJbmRpY2F0ZXMgdGhlIG51bWJlciBvZiA0NS1ieXRl
IFRlbGV0ZXh0IHBhY2tldHMgaW4gdGhpcyBkYXRhIGNvbnRhaW5lcnBwdAAVdGVsZXRleHRfcGFj
a2V0X2NvdW50cH5xAH4AJXQAB0lOVEVHRVJzcQB+AB10AEFUcnVlIG9uIHRoZSBsYXN0IGRhdGEg
cGFja2V0IG9mIHRoZSBTdHJlYW0gKHdpdGggb3Igd2l0aG91dCBkYXRhKXBwdAALZW5kT2ZTdHJl
YW1wfnEAfgAldAAHQk9PTEVBTnNxAH4AHXQAJkluZGljYXRlcyB0aGUgcGFja2V0IHRyYW5zbWlz
c2lvbiBtb2RlcHB0ABp0ZWxldGV4dF90cmFuc21pc3Npb25fbW9kZXNxAH4ALwAAAAJ3BAAAAAJz
cQB+ADFwdAAGU2VyaWFscHQABnNlcmlhbHEAfgA0c3EAfgAxcHQACFBhcmFsbGVscHQACHBhcmFs
bGVscQB+ADR4cQB+AD5zcQB+AB10ACxJbmRpY2F0ZXMgaWYgdGhlIGZyYW1lIHJhdGUgcmVtYWlu
cyBjb25zdGFudHBwdAATY29uc3RhbnRfZnJhbWVfcmF0ZXBxAH4AiXNxAH4AHXQAGEluZGljYXRl
cyB0aGUgZnJhbWUgcmF0ZXBwdAAKZnJhbWVfcmF0ZXB+cQB+ACV0AAhSQVRJT05BTHNxAH4AHXBw
cHQAD2RhdGFfaXNfbWlzc2luZ3BxAH4AiXNxAH4AHXQAF0Nhbm9uaWNhbCBMYW5ndWFnZSBDb2Rl
cHB0AA1sYW5ndWFnZV9jb2Rlc3EAfgAvAAAAuncEAAAAunNxAH4AMXB0AAlVbmRlZmluZWRwdAAD
dW5kcQB+ADRzcQB+ADFwdAAOTm90IEFwcGxpY2FibGVwdAADenh4cQB+ADRzcQB+ADFwdAARQWJr
aGF6aWFuLCBBYmtoYXpwdAACYWJxAH4ANHNxAH4AMXB0AARBZmFycHQAAmFhcQB+ADRzcQB+ADFw
dAAJQWZyaWthYW5zcHQAAmFmcQB+ADRzcQB+ADFwdAAEQWthbnB0AAJha3EAfgA0c3EAfgAxcHQA
CEFsYmFuaWFucHQAAnNxcQB+ADRzcQB+ADFwdAAHQW1oYXJpY3B0AAJhbXEAfgA0c3EAfgAxcHQA
BkFyYWJpY3B0AAJhcnEAfgA0c3EAfgAxcHQACUFyYWdvbmVzZXB0AAJhbnEAfgA0c3EAfgAxcHQA
CEFybWVuaWFucHQAAmh5cQB+ADRzcQB+ADFwdAAIQXNzYW1lc2VwdAACYXNxAH4ANHNxAH4AMXB0
AAZBdmFyaWNwdAACYXZxAH4ANHNxAH4AMXB0AAdBdmVzdGFucHQAAmFlcQB+ADRzcQB+ADFwdAAG
QXltYXJhcHQAAmF5cQB+ADRzcQB+ADFwdAALQXplcmJhaWphbmlwdAACYXpxAH4ANHNxAH4AMXB0
AAdCYW1iYXJhcHQAAmJtcQB+ADRzcQB+ADFwdAAHQmFzaGtpcnB0AAJiYXEAfgA0c3EAfgAxcHQA
BkJhc3F1ZXB0AAJldXEAfgA0c3EAfgAxcHQACkJlbGFydXNpYW5wdAACYmVxAH4ANHNxAH4AMXB0
AAdCZW5nYWxpcHQAAmJucQB+ADRzcQB+ADFwdAAQQmloYXJpIExhbmd1YWdlc3B0AAJiaHEAfgA0
c3EAfgAxcHQAB0Jpc2xhbWFwdAACYmlxAH4ANHNxAH4AMXB0AAdCb3NuaWFucHQAAmJzcQB+ADRz
cQB+ADFwdAAGQnJldG9ucHQAAmJycQB+ADRzcQB+ADFwdAAJQnVsZ2FyaWFucHQAAmJncQB+ADRz
cQB+ADFwdAAHQnVybWVzZXB0AAJteXEAfgA0c3EAfgAxcHQAEkNhdGFsYW4sIFZhbGVuY2lhbnB0
AAJjYXEAfgA0c3EAfgAxcHQACENoYW1vcnJvcHQAAmNocQB+ADRzcQB+ADFwdAAHQ2hlY2hlbnB0
AAJjZXEAfgA0c3EAfgAxcHQAF0NoaWNoZXdhLCBDaGV3YSwgTnlhbmphcHQAAm55cQB+ADRzcQB+
ADFwdAAHQ2hpbmVzZXB0AAJ6aHEAfgA0c3EAfgAxcHQAHENodXJjaCBTbGF2aWMsIE9sZCBCdWxn
YXJpYW5wdAACY3VxAH4ANHNxAH4AMXB0AAdDaHV2YXNocHQAAmN2cQB+ADRzcQB+ADFwdAAHQ29y
bmlzaHB0AAJrd3EAfgA0c3EAfgAxcHQACENvcnNpY2FucHQAAmNvcQB+ADRzcQB+ADFwdAAEQ3Jl
ZXB0AAJjcnEAfgA0c3EAfgAxcHQACENyb2F0aWFucHQAAmhycQB+ADRzcQB+ADFwdAAFQ3plY2hw
dAACY3NxAH4ANHNxAH4AMXB0AAZEYW5pc2hwdAACZGFxAH4ANHNxAH4AMXB0ABpEaXZlaGksIERo
aXZlaGksIE1hbGRpdmlhbnB0AAJkdnEAfgA0c3EAfgAxcHQADkR1dGNoLCBGbGVtaXNocHQAAm5s
cQB+ADRzcQB+ADFwdAAIRHpvbmdraGFwdAACZHpxAH4ANHNxAH4AMXB0AAdFbmdsaXNocHQAAmVu
cQB+ADRzcQB+ADFwdAAJRXNwZXJhbnRvcHQAAmVvcQB+ADRzcQB+ADFwdAAIRXN0b25pYW5wdAAC
ZXRxAH4ANHNxAH4AMXB0AANFd2VwdAACZWVxAH4ANHNxAH4AMXB0AAdGYXJvZXNlcHQAAmZvcQB+
ADRzcQB+ADFwdAAGRmlqaWFucHQAAmZqcQB+ADRzcQB+ADFwdAAHRmlubmlzaHB0AAJmaXEAfgA0
c3EAfgAxcHQABkZyZW5jaHB0AAJmcnEAfgA0c3EAfgAxcHQABUZ1bGFocHQAAmZmcQB+ADRzcQB+
ADFwdAAIR2FsaWNpYW5wdAACZ2xxAH4ANHNxAH4AMXB0AAVHYW5kYXB0AAJsZ3EAfgA0c3EAfgAx
cHQACEdlb3JnaWFucHQAAmthcQB+ADRzcQB+ADFwdAAGR2VybWFucHQAAmRlcQB+ADRzcQB+ADFw
dAAHR3VhcmFuaXB0AAJnbnEAfgA0c3EAfgAxcHQACEd1amFyYXRpcHQAAmd1cQB+ADRzcQB+ADFw
dAAPSGFpdGlhbiwgQ3Jlb2xlcHQAAmh0cQB+ADRzcQB+ADFwdAAFSGF1c2FwdAACaGFxAH4ANHNx
AH4AMXB0AAZIZWJyZXdwdAACaGVxAH4ANHNxAH4AMXB0AAZIZXJlcm9wdAACaHpxAH4ANHNxAH4A
MXB0AAVIaW5kaXB0AAJoaXEAfgA0c3EAfgAxcHQACUhpcmkgTW90dXB0AAJob3EAfgA0c3EAfgAx
cHQACUh1bmdhcmlhbnB0AAJodXEAfgA0c3EAfgAxcHQACUljZWxhbmRpY3B0AAJpc3EAfgA0c3EA
fgAxcHQAA0lkb3B0AAJpb3EAfgA0c3EAfgAxcHQABElnYm9wdAACaWdxAH4ANHNxAH4AMXB0AApJ
bmRvbmVzaWFucHQAAmlkcQB+ADRzcQB+ADFwdAALSW50ZXJsaW5ndWFwdAACaWFxAH4ANHNxAH4A
MXB0ABdJbnRlcmxpbmd1ZSwgT2NjaWRlbnRhbHB0AAJpZXEAfgA0c3EAfgAxcHQACUludWt0aXR1
dHB0AAJpdXEAfgA0c3EAfgAxcHQAB0ludXBpYXFwdAACaWtxAH4ANHNxAH4AMXB0AAVJcmlzaHB0
AAJnYXEAfgA0c3EAfgAxcHQAB0l0YWxpYW5wdAACaXRxAH4ANHNxAH4AMXB0AAhKYXBhbmVzZXB0
AAJqYXEAfgA0c3EAfgAxcHQACEphdmFuZXNlcHQAAmp2cQB+ADRzcQB+ADFwdAAYS2FsYWFsbGlz
dXQsIEdyZWVubGFuZGljcHQAAmtscQB+ADRzcQB+ADFwdAAHS2FubmFkYXB0AAJrbnEAfgA0c3EA
fgAxcHQABkthbnVyaXB0AAJrcnEAfgA0c3EAfgAxcHQACEthc2htaXJpcHQAAmtzcQB+ADRzcQB+
ADFwdAAGS2F6YWtocHQAAmtrcQB+ADRzcQB+ADFwdAAFS2htZXJwdAACa21xAH4ANHNxAH4AMXB0
AA5LaWt1eXUsIEdpa3V5dXB0AAJraXEAfgA0c3EAfgAxcHQAC0tpbnlhcndhbmRhcHQAAnJ3cQB+
ADRzcQB+ADFwdAAPS2lyZ2hpeiwgS3lyZ3l6cHQAAmt5cQB+ADRzcQB+ADFwdAAHS2lydW5kaXB0
AAJybnEAfgA0c3EAfgAxcHQABEtvbWlwdAACa3ZxAH4ANHNxAH4AMXB0AAVLb25nb3B0AAJrZ3EA
fgA0c3EAfgAxcHQABktvcmVhbnB0AAJrb3EAfgA0c3EAfgAxcHQAEkt1YW55YW1hLCBLd2FueWFt
YXB0AAJranEAfgA0c3EAfgAxcHQAB0t1cmRpc2hwdAACa3VxAH4ANHNxAH4AMXB0AANMYW9wdAAC
bG9xAH4ANHNxAH4AMXB0AAVMYXRpbnB0AAJsYXEAfgA0c3EAfgAxcHQAB0xhdHZpYW5wdAACbHZx
AH4ANHNxAH4AMXB0ACBMaW1idXJnYW4sIExpbWJ1cmdlciwgTGltYnVyZ2lzaHB0AAJsaXEAfgA0
c3EAfgAxcHQAB0xpbmdhbGFwdAACbG5xAH4ANHNxAH4AMXB0AApMaXRodWFuaWFucHQAAmx0cQB+
ADRzcQB+ADFwdAAMTHViYS1LYXRhbmdhcHQAAmx1cQB+ADRzcQB+ADFwdAAcTHV4ZW1ib3VyZ2lz
aCwgTGV0emVidXJnZXNjaHB0AAJsYnEAfgA0c3EAfgAxcHQACk1hY2Vkb25pYW5wdAACbWtxAH4A
NHNxAH4AMXB0AAhNYWxhZ2FzeXB0AAJtZ3EAfgA0c3EAfgAxcHQABU1hbGF5cHQAAm1zcQB+ADRz
cQB+ADFwdAAJTWFsYXlhbGFtcHQAAm1scQB+ADRzcQB+ADFwdAAHTWFsdGVzZXB0AAJtdHEAfgA0
c3EAfgAxcHQABE1hbnhwdAACZ3ZxAH4ANHNxAH4AMXB0AAVNYW9yaXB0AAJtaXEAfgA0c3EAfgAx
cHQAB01hcmF0aGlwdAACbXJxAH4ANHNxAH4AMXB0AAtNYXJzaGFsbGVzZXB0AAJtaHEAfgA0c3EA
fgAxcHQADE1vZGVybiBHcmVla3B0AAJlbHEAfgA0c3EAfgAxcHQACU1vbmdvbGlhbnB0AAJtbnEA
fgA0c3EAfgAxcHQABU5hdXJ1cHQAAm5hcQB+ADRzcQB+ADFwdAAOTmF2YWpvLCBOYXZhaG9wdAAC
bnZxAH4ANHNxAH4AMXB0AAZOZG9uZ2FwdAACbmdxAH4ANHNxAH4AMXB0AAZOZXBhbGlwdAACbmVx
AH4ANHNxAH4AMXB0AA1Ob3J0aCBOZGViZWxlcHQAAm5kcQB+ADRzcQB+ADFwdAANTm9ydGhlcm4g
U2FtaXB0AAJzZXEAfgA0c3EAfgAxcHQACU5vcndlZ2lhbnB0AAJub3EAfgA0c3EAfgAxcHQAEU5v
cndlZ2lhbiBCb2ttw6VscHQAAm5icQB+ADRzcQB+ADFwdAARTm9yd2VnaWFuIE55bm9yc2twdAAC
bm5xAH4ANHNxAH4AMXB0ABNPY2NpdGFuIChwb3N0IDE1MDApcHQAAm9jcQB+ADRzcQB+ADFwdAAG
T2ppYndhcHQAAm9qcQB+ADRzcQB+ADFwdAAFT3JpeWFwdAACb3JxAH4ANHNxAH4AMXB0AAVPcm9t
b3B0AAJvbXEAfgA0c3EAfgAxcHQAEU9zc2V0aWFuLCBPc3NldGljcHQAAm9zcQB+ADRzcQB+ADFw
dAAEUGFsaXB0AAJwaXEAfgA0c3EAfgAxcHQAEFBhbmphYmksIFB1bmphYmlwdAACcGFxAH4ANHNx
AH4AMXB0AAdQZXJzaWFucHQAAmZhcQB+ADRzcQB+ADFwdAAGUG9saXNocHQAAnBscQB+ADRzcQB+
ADFwdAAKUG9ydHVndWVzZXB0AAJwdHEAfgA0c3EAfgAxcHQADlB1c2h0bywgUGFzaHRvcHQAAnBz
cQB+ADRzcQB+ADFwdAAHUXVlY2h1YXB0AAJxdXEAfgA0c3EAfgAxcHQAHVJvbWFuaWFuLCBNb2xk
YXZpYW4sIE1vbGRvdmFucHQAAnJvcQB+ADRzcQB+ADFwdAAHUm9tYW5zaHB0AAJybXEAfgA0c3EA
fgAxcHQAB1J1c3NpYW5wdAACcnVxAH4ANHNxAH4AMXB0AAZTYW1vYW5wdAACc21xAH4ANHNxAH4A
MXB0AAVTYW5nb3B0AAJzZ3EAfgA0c3EAfgAxcHQACFNhbnNrcml0cHQAAnNhcQB+ADRzcQB+ADFw
dAAJU2FyZGluaWFucHQAAnNjcQB+ADRzcQB+ADFwdAAPU2NvdHRpc2ggR2FlbGljcHQAAmdkcQB+
ADRzcQB+ADFwdAAHU2VyYmlhbnB0AAJzcnEAfgA0c3EAfgAxcHQABVNob25hcHQAAnNucQB+ADRz
cQB+ADFwdAARU2ljaHVhbiBZaSwgTnVvc3VwdAACaWlxAH4ANHNxAH4AMXB0AAZTaW5kaGlwdAAC
c2RxAH4ANHNxAH4AMXB0ABJTaW5oYWxhLCBTaW5oYWxlc2VwdAACc2lxAH4ANHNxAH4AMXB0AAZT
bG92YWtwdAACc2txAH4ANHNxAH4AMXB0AAlTbG92ZW5pYW5wdAACc2xxAH4ANHNxAH4AMXB0AAZT
b21hbGlwdAACc29xAH4ANHNxAH4AMXB0AA1Tb3V0aCBOZGViZWxlcHQAAm5ycQB+ADRzcQB+ADFw
dAAOU291dGhlcm4gU290aG9wdAACc3RxAH4ANHNxAH4AMXB0ABJTcGFuaXNoLCBDYXN0aWxpYW5w
dAACZXNxAH4ANHNxAH4AMXB0AAlTdW5kYW5lc2VwdAACc3VxAH4ANHNxAH4AMXB0AAdTd2FoaWxp
cHQAAnN3cQB+ADRzcQB+ADFwdAAFU3dhdGlwdAACc3NxAH4ANHNxAH4AMXB0AAdTd2VkaXNocHQA
AnN2cQB+ADRzcQB+ADFwdAAHVGFnYWxvZ3B0AAJ0bHEAfgA0c3EAfgAxcHQACFRhaGl0aWFucHQA
AnR5cQB+ADRzcQB+ADFwdAAFVGFqaWtwdAACdGdxAH4ANHNxAH4AMXB0AAVUYW1pbHB0AAJ0YXEA
fgA0c3EAfgAxcHQABVRhdGFycHQAAnR0cQB+ADRzcQB+ADFwdAAGVGVsdWd1cHQAAnRlcQB+ADRz
cQB+ADFwdAAEVGhhaXB0AAJ0aHEAfgA0c3EAfgAxcHQAB1RpYmV0YW5wdAACYm9xAH4ANHNxAH4A
MXB0AAhUaWdyaW55YXB0AAJ0aXEAfgA0c3EAfgAxcHQAFVRvbmdhIChUb25nYSBJc2xhbmRzKXB0
AAJ0b3EAfgA0c3EAfgAxcHQABlRzb25nYXB0AAJ0c3EAfgA0c3EAfgAxcHQABlRzd2FuYXB0AAJ0
bnEAfgA0c3EAfgAxcHQAB1R1cmtpc2hwdAACdHJxAH4ANHNxAH4AMXB0AAdUdXJrbWVucHQAAnRr
cQB+ADRzcQB+ADFwdAADVHdpcHQAAnR3cQB+ADRzcQB+ADFwdAAOVWlnaHVyLCBVeWdodXJwdAAC
dWdxAH4ANHNxAH4AMXB0AAlVa3JhaW5pYW5wdAACdWtxAH4ANHNxAH4AMXB0AARVcmR1cHQAAnVy
cQB+ADRzcQB+ADFwdAAFVXpiZWtwdAACdXpxAH4ANHNxAH4AMXB0AAVWZW5kYXB0AAJ2ZXEAfgA0
c3EAfgAxcHQAClZpZXRuYW1lc2VwdAACdmlxAH4ANHNxAH4AMXB0AAhWb2xhcMO8a3B0AAJ2b3EA
fgA0c3EAfgAxcHQAB1dhbGxvb25wdAACd2FxAH4ANHNxAH4AMXB0AAVXZWxzaHB0AAJjeXEAfgA0
c3EAfgAxcHQAD1dlc3Rlcm4gRnJpc2lhbnB0AAJmeXEAfgA0c3EAfgAxcHQABVdvbG9mcHQAAndv
cQB+ADRzcQB+ADFwdAAFWGhvc2FwdAACeGhxAH4ANHNxAH4AMXB0AAdZaWRkaXNocHQAAnlpcQB+
ADRzcQB+ADFwdAAGWW9ydWJhcHQAAnlvcQB+ADRzcQB+ADFwdAAOWmh1YW5nLCBDaHVhbmdwdAAC
emFxAH4ANHNxAH4AMXB0AARadWx1cHQAAnp1cQB+ADR4cQB+AD5zcQB+AB10ADNQb3NpdGlvbiBv
ciBvZmZzZXQgZnJvbSB0aGUgYmVnaW5uaW5nIG9mIHRoZSBzdHJlYW1wcHQAEHBvc2l0aW9uSW5T
dHJlYW1wfnEAfgAldAAETE9OR3NxAH4AHXQALVRoZSBtb3N0IHJlbGV2YW50IHRpbWUgcGVydGFp
bmluZyB0byB0aGUgZGF0YXBwdAAEdGltZXBxAH4AJ3NxAH4AHXBwcHQAFGRhdGFfaXNfbWFudWZh
Y3R1cmVkcHEAfgCJc3EAfgAddAAjVG90YWwgbGVuZ3RoIG9mIHRoZSBzdHJlYW0gaWYga25vd25w
cHQADmxlbmd0aE9mU3RyZWFtcHEAfgLUeHBzcgARamF2YS51dGlsLkhhc2hNYXAFB9rBwxZg0QMA
AkYACmxvYWRGYWN0b3JJAAl0aHJlc2hvbGR4cD8AAAAAAAAgdwgAAABAAAAAAXEAfgBDc3IAJmph
dmEudXRpbC5Db2xsZWN0aW9ucyRVbm1vZGlmaWFibGVMaXN0/A8lMbXsjhACAAFMAARsaXN0cQB+
AB54cgAsamF2YS51dGlsLkNvbGxlY3Rpb25zJFVubW9kaWZpYWJsZUNvbGxlY3Rpb24ZQgCAy173
HgIAAUwAAWN0ABZMamF2YS91dGlsL0NvbGxlY3Rpb247eHBzcQB+AC8AAAABdwQAAAABcQB+AFl4
cQB+AuR4</property>
                        <pinDefinition name="TeletextData" displayName="WST Subtitles (Ancillary Data)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="ActiveFormatDescriptionAndBarData" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAASdAAJRnJhbWVSYXRldAAnQWN0aXZlRm9ybWF0
RGVzY3JpcHRpb25BbmRCYXJEYXRhU3RyZWFtdAAyQWN0aXZlRm9ybWF0RGVzY3JpcHRpb25BbmRC
YXJEYXRhU2FtcGxlSW5mb3JtYXRpb250AA1EYXRhSXNNaXNzaW5ndAALTWVkaWFUaW1pbmd0ABJE
YXRhSXNNYW51ZmFjdHVyZWR0ABFTYW1wbGVJbmZvcm1hdGlvbnQAF0FjdGl2ZUZvcm1hdERlc2Ny
aXB0aW9udAALQXNwZWN0UmF0aW90AAlDb250YWluZXJ0ACFBY3RpdmVGb3JtYXREZXNjcmlwdGlv
bkFuZEJhckRhdGF0AAhUZW1wb3JhbHQAC0theWFrQnVmZmVydAAHQmFyRGF0YXQACkJ5dGVTdHJl
YW10ACdBY3RpdmVGb3JtYXREZXNjcmlwdGlvbkFuZEJhckRhdGFTYW1wbGV0AAtNZWRpYU9yaWdp
bnQABlN0cmVhbXhzcQB+AAh3DAAAACA/QAAAAAAAEXNyAE9jYS5kaWdpdGFscmFwaWRzLmtheWFr
LmRhdGF0eXBlcy5kZWZpbml0aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlvbiRTaW1wbGVUeXBl
AAAAAAAAAAECAAJMABFlbnVtZXJhdGlvblZhbHVlc3QAEExqYXZhL3V0aWwvTGlzdDtMAAR0eXBl
dABDTGNhL2RpZ2l0YWxyYXBpZHMva2F5YWsvZGF0YXR5cGVzL2RlZmluaXRpb24vbW9kZWwvU2lt
cGxlVHlwZXNFbnVtO3hyAFJjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZpbml0
aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlvbiRLZXlEZWZpbml0aW9uAAAAAAAAAAECAARMAAdj
b21tZW50cQB+AAVMAAtkaXNwbGF5TmFtZXEAfgAFTAALbXVsdGlWYWx1ZWR0ABNMamF2YS9sYW5n
L0Jvb2xlYW47TAAEbmFtZXEAfgAFeHB0AElUaGUgdGltZSBwZXJ0YWluaW5nIHRvIHRoZSBlbmQg
b2YgdGhlIGRhdGEgKHRpbWUgKyBkdXJhdGlvbiBvZiB0aGlzIGRhdGEpcHB0AAd0aW1lRW5kcH5y
AEFjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZpbml0aW9uLm1vZGVsLlNpbXBs
ZVR5cGVzRW51bQAAAAAAAAAAEgAAeHIADmphdmEubGFuZy5FbnVtAAAAAAAAAAASAAB4cHQABFRJ
TUVzcQB+AB10ABxJbmRpY2F0ZXMgdGhlIG1lZGlhIGR1cmF0aW9ucHB0AA5tZWRpYV9kdXJhdGlv
bnBxAH4AJ3NxAH4AHXBwc3IAEWphdmEubGFuZy5Cb29sZWFuzSBygNWc+u4CAAFaAAV2YWx1ZXhw
AXQADG1lZGlhX29yaWdpbnNyABNqYXZhLnV0aWwuQXJyYXlMaXN0eIHSHZnHYZ0DAAFJAARzaXpl
eHAAAAAXdwQAAAAXc3IAN2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsucGx1Z2luLnhtbC5LYXlha0Vu
dW1lcmF0aW9uVmFsdWUAAAAAAAAAAQIABUwAC2Rlc2NyaXB0aW9ucQB+AAVMAAtkaXNwbGF5TmFt
ZXEAfgAFTAAGaGlkZGVucQB+ACFMAA52YWx1ZUF0dHJpYnV0ZXEAfgAFTAANdmFsdWVFbWJlZGRl
ZHEAfgAFeHBwcHBwdAAMTWFudWZhY3R1cmVkc3EAfgAycHQACERSQ1ZpZGVvcHEAfgA2dAAAc3EA
fgAycHQAA0dYRnBxAH4AOXEAfgA3c3EAfgAycHQAA0xYRnBxAH4AO3EAfgA3c3EAfgAycHQAA01Y
RnBxAH4APXEAfgA3c3EAfgAycHQACVF1aWNrVGltZXBxAH4AP3EAfgA3c3EAfgAycHQADVdpbmRv
d3MgTWVkaWFwdAAMV2luZG93c01lZGlhcQB+ADdzcQB+ADJwdAAJVXNlciBEYXRhcHQACVVzZXJf
ZGF0YXEAfgA3c3EAfgAycHQADkFuY2lsbGFyeSBEYXRhcHQADkFuY2lsbGFyeV9kYXRhcQB+ADdz
cQB+ADJwdAACRFZwcQB+AEpxAH4AN3NxAH4AMnB0AANWQzNwcQB+AExxAH4AN3NxAH4AMnB0ABZB
VkMgUGljdHVyZSBUaW1pbmcgU0VJcHQAFkFWQ19QaWN0dXJlX1RpbWluZ19TRUlxAH4AN3NxAH4A
MnB0ABBNUEVHMiBHT1AgSGVhZGVycHQAEE1QRUcyX0dPUF9oZWFkZXJxAH4AN3NxAH4AMnB0ABRN
WEYgTWF0ZXJpYWwgUGFja2FnZXB0ABRNWEZfbWF0ZXJpYWxfcGFja2FnZXEAfgA3c3EAfgAycHQA
EE1YRiBTeXN0ZW0gVHJhY2twdAAQTVhGX3N5c3RlbV90cmFja3EAfgA3c3EAfgAycHQAE0FuY2ls
bGFyeSBEYXRhIFZJVENwdAATQW5jaWxsYXJ5X2RhdGFfVklUQ3EAfgA3c3EAfgAycHQAEkFuY2ls
bGFyeSBEYXRhIExUQ3B0ABJBbmNpbGxhcnlfZGF0YV9MVENxAH4AN3NxAH4AMnB0ABFIRVZDIFRp
bWVjb2RlIFNFSXB0ABFIRVZDX1RpbWVjb2RlX1NFSXEAfgA3c3EAfgAycHQAEFNDVEUyMCBVc2Vy
IERhdGFwdAAQVXNlcl9kYXRhX1NDVEUyMHEAfgA3c3EAfgAycHQADkFUU0MgVXNlciBEYXRhcHQA
DlVzZXJfZGF0YV9BVFNDcQB+ADdzcQB+ADJwcQB+AGZwdAADU0NDcQB+ADdzcQB+ADJwdAAZQW5j
aWxsYXJ5IERhdGEgTGVnYWN5IDYwOHB0ABlBbmNpbGxhcnlfZGF0YV9sZWdhY3lfNjA4cQB+ADdz
cQB+ADJwdAANQ3VlIFBvaW50IFhNTHB0AAtDdWVQb2ludFhNTHEAfgA3eH5xAH4AJXQABlNUUklO
R3NxAH4AHXQAQVRydWUgb24gdGhlIGxhc3QgZGF0YSBwYWNrZXQgb2YgdGhlIFN0cmVhbSAod2l0
aCBvciB3aXRob3V0IGRhdGEpcHB0AAtlbmRPZlN0cmVhbXB+cQB+ACV0AAdCT09MRUFOc3EAfgAd
dABZRmlyc3QgbGluZSBvZiBhIGhvcml6b250YWwgbGV0dGVyYm94IGJvdHRvbSBiYXIgYXJlYS4g
RXhwcmVzc2VkIHVzaW5nIFNNUFRFIGxpbmUgbnVtYmVycy5wcHQAHGJvdHRvbV9iYXJfbGluZV9u
dW1iZXJfc3RhcnRwfnEAfgAldAAHSU5URUdFUnNxAH4AHXQALEluZGljYXRlcyBpZiB0aGUgZnJh
bWUgcmF0ZSByZW1haW5zIGNvbnN0YW50cHB0ABNjb25zdGFudF9mcmFtZV9yYXRlcHEAfgB1c3EA
fgAddABARmlyc3QgaG9yaXpvbnRhbCBsdW1pbmFuY2Ugc2FtcGxlIG9mIGEgcGlsbGFyYm94IHJp
Z2h0IGJhciBhcmVhLnBwdAAWcmlnaHRfYmFyX3BpeGVsX251bWJlcnBxAH4AenNxAH4AHXQAGElu
ZGljYXRlcyB0aGUgZnJhbWUgcmF0ZXBwdAAKZnJhbWVfcmF0ZXB+cQB+ACV0AAhSQVRJT05BTHNx
AH4AHXBwcHQAD2RhdGFfaXNfbWlzc2luZ3BxAH4AdXNxAH4AHXQAM1Bvc2l0aW9uIG9yIG9mZnNl
dCBmcm9tIHRoZSBiZWdpbm5pbmcgb2YgdGhlIHN0cmVhbXBwdAAQcG9zaXRpb25JblN0cmVhbXB+
cQB+ACV0AARMT05Hc3EAfgAddAAiSW5kaWNhdGVzIHRoZSBkaXNwbGF5IGFzcGVjdCByYXRpb3Bw
dAAUZGlzcGxheV9hc3BlY3RfcmF0aW9wcQB+AIVzcQB+AB10AD5MYXN0IGhvcml6b250YWwgbHVt
aW5hbmNlIHNhbXBsZSBvZiBhIHBpbGxhcmJveCBsZWZ0IGJhciBhcmVhLnBwdAAVbGVmdF9iYXJf
cGl4ZWxfbnVtYmVycHEAfgB6c3EAfgAddABURGVzY3JpYmVzIHRoZSAnYXJlYSBvZiBpbnRlcmVz
dCcgaW4gdGVybXMgb2YgaXRzIGFzcGVjdCByYXRpbyB3aXRoaW4gdGhlIGNvZGVkIGZyYW1lcHB0
AA1hY3RpdmVfZm9ybWF0c3EAfgAwAAAAEHcEAAAAEHNxAH4AMnB0AAxub24tc3RhbmRhcmRwdAAB
MHEAfgA3c3EAfgAycHQACHJlc2VydmVkcHQAATFxAH4AN3NxAH4AMnB0AA5ib3ggMTY6OSAodG9w
KXB0AAEycQB+ADdzcQB+ADJwdAAOYm94IDE0OjkgKHRvcClwdAABM3EAfgA3c3EAfgAycHQAEGJv
eCA+IDE2OjkgKHRvcClwdAABNHEAfgA3c3EAfgAycHEAfgCccHQAATVxAH4AN3NxAH4AMnBxAH4A
nHB0AAE2cQB+ADdzcQB+ADJwcQB+AJxwdAABN3EAfgA3c3EAfgAycHQALGFjdGl2ZSBmb3JtYXQg
aXMgdGhlIHNhbWUgYXMgdGhlIGNvZGVkIGZyYW1lcHQAAThxAH4AN3NxAH4AMnB0AAw0OjMgKGNl
bnRyZSlwdAABOXEAfgA3c3EAfgAycHQADTE2OjkgKGNlbnRyZSlwdAACMTBxAH4AN3NxAH4AMnB0
AA0xNDo5IChjZW50cmUpcHQAAjExcQB+ADdzcQB+ADJwcQB+AJxwdAACMTJxAH4AN3NxAH4AMnB0
ACo0OjMgKHdpdGggc2hvb3QgYW5kIHByb3RlY3RlZCAxNDo5IGNlbnRyZSlwdAACMTNxAH4AN3Nx
AH4AMnB0ACsxNjo5ICh3aXRoIHNob290IGFuZCBwcm90ZWN0ZWQgMTQ6OSBjZW50cmUpcHQAAjE0
cQB+ADdzcQB+ADJwdAAqMTY6OSAod2l0aCBzaG9vdCBhbmQgcHJvdGVjdGVkIDQ6MyBjZW50cmUp
cHQAAjE1cQB+ADd4cQB+AHpzcQB+AB10AC1UaGUgbW9zdCByZWxldmFudCB0aW1lIHBlcnRhaW5p
bmcgdG8gdGhlIGRhdGFwcHQABHRpbWVwcQB+ACdzcQB+AB1wcHB0ABRkYXRhX2lzX21hbnVmYWN0
dXJlZHBxAH4AdXNxAH4AHXQAVUxhc3QgbGluZSBvZiBhIGhvcml6b250YWwgbGV0dGVyYm94IHRv
cCBiYXIgYXJlYS4gRXhwcmVzc2VkIHVzaW5nIFNNUFRFIGxpbmUgbnVtYmVycy5wcHQAF3RvcF9i
YXJfbGluZV9udW1iZXJfZW5kcHEAfgB6c3EAfgAddAAjVG90YWwgbGVuZ3RoIG9mIHRoZSBzdHJl
YW0gaWYga25vd25wcHQADmxlbmd0aE9mU3RyZWFtcHEAfgCMeHBzcgARamF2YS51dGlsLkhhc2hN
YXAFB9rBwxZg0QMAAkYACmxvYWRGYWN0b3JJAAl0aHJlc2hvbGR4cD8AAAAAAAAgdwgAAABAAAAA
AXEAfgAvc3IAJmphdmEudXRpbC5Db2xsZWN0aW9ucyRVbm1vZGlmaWFibGVMaXN0/A8lMbXsjhAC
AAFMAARsaXN0cQB+AB54cgAsamF2YS51dGlsLkNvbGxlY3Rpb25zJFVubW9kaWZpYWJsZUNvbGxl
Y3Rpb24ZQgCAy173HgIAAUwAAWN0ABZMamF2YS91dGlsL0NvbGxlY3Rpb247eHBzcQB+ADAAAAAB
dwQAAAABcQB+AEh4cQB+ANV4</property>
                        <pinDefinition name="ActiveFormatDescriptionAndBarData" displayName="Active Format Description" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="SCTE104OperationMessage" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAATdAAIQ3VlUG9pbnR0AAlGcmFtZVJhdGV0AA5D
dWVQb2ludFNhbXBsZXQAKFNDVEUxMDRPcGVyYXRpb25NZXNzYWdlU2FtcGxlSW5mb3JtYXRpb250
AA1EYXRhSXNNaXNzaW5ndAALTWVkaWFUaW1pbmd0ABlDdWVQb2ludFNhbXBsZUluZm9ybWF0aW9u
dAASRGF0YUlzTWFudWZhY3R1cmVkdAARU2FtcGxlSW5mb3JtYXRpb250ABdTQ1RFMTA0T3BlcmF0
aW9uTWVzc2FnZXQAHVNDVEUxMDRPcGVyYXRpb25NZXNzYWdlU2FtcGxldAAOQ3VlUG9pbnRTdHJl
YW10AAlDb250YWluZXJ0AAhUZW1wb3JhbHQAC0theWFrQnVmZmVydAAdU0NURTEwNE9wZXJhdGlv
bk1lc3NhZ2VTdHJlYW10AApCeXRlU3RyZWFtdAALTWVkaWFPcmlnaW50AAZTdHJlYW14c3EAfgAI
dwwAAAAgP0AAAAAAAA5zcgBPY2EuZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhdHlwZXMuZGVmaW5p
dGlvbi5tb2RlbC5EYXRhVHlwZURlZmluaXRpb24kU2ltcGxlVHlwZQAAAAAAAAABAgACTAARZW51
bWVyYXRpb25WYWx1ZXN0ABBMamF2YS91dGlsL0xpc3Q7TAAEdHlwZXQAQ0xjYS9kaWdpdGFscmFw
aWRzL2theWFrL2RhdGF0eXBlcy9kZWZpbml0aW9uL21vZGVsL1NpbXBsZVR5cGVzRW51bTt4cgBS
Y2EuZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5EYXRhVHlw
ZURlZmluaXRpb24kS2V5RGVmaW5pdGlvbgAAAAAAAAABAgAETAAHY29tbWVudHEAfgAFTAALZGlz
cGxheU5hbWVxAH4ABUwAC211bHRpVmFsdWVkdAATTGphdmEvbGFuZy9Cb29sZWFuO0wABG5hbWVx
AH4ABXhwdABJVGhlIHRpbWUgcGVydGFpbmluZyB0byB0aGUgZW5kIG9mIHRoZSBkYXRhICh0aW1l
ICsgZHVyYXRpb24gb2YgdGhpcyBkYXRhKXBwdAAHdGltZUVuZHB+cgBBY2EuZGlnaXRhbHJhcGlk
cy5rYXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5TaW1wbGVUeXBlc0VudW0AAAAAAAAA
ABIAAHhyAA5qYXZhLmxhbmcuRW51bQAAAAAAAAAAEgAAeHB0AARUSU1Fc3EAfgAedAAcSW5kaWNh
dGVzIHRoZSBtZWRpYSBkdXJhdGlvbnBwdAAObWVkaWFfZHVyYXRpb25wcQB+AChzcQB+AB50AC1E
dXJhdGlvbiBvZiB0aGUgc3BsaWNlIG9wcG9ydHVuaXR5IGluIHNlY29uZHNwcHQAEWN1ZXBvaW50
X2R1cmF0aW9ucH5xAH4AJnQACFJBVElPTkFMc3EAfgAecHBzcgARamF2YS5sYW5nLkJvb2xlYW7N
IHKA1Zz67gIAAVoABXZhbHVleHABdAAMbWVkaWFfb3JpZ2luc3IAE2phdmEudXRpbC5BcnJheUxp
c3R4gdIdmcdhnQMAAUkABHNpemV4cAAAABd3BAAAABdzcgA3Y2EuZGlnaXRhbHJhcGlkcy5rYXlh
ay5wbHVnaW4ueG1sLktheWFrRW51bWVyYXRpb25WYWx1ZQAAAAAAAAABAgAFTAALZGVzY3JpcHRp
b25xAH4ABUwAC2Rpc3BsYXlOYW1lcQB+AAVMAAZoaWRkZW5xAH4AIkwADnZhbHVlQXR0cmlidXRl
cQB+AAVMAA12YWx1ZUVtYmVkZGVkcQB+AAV4cHBwcHB0AAxNYW51ZmFjdHVyZWRzcQB+ADhwdAAI
RFJDVmlkZW9wcQB+ADx0AABzcQB+ADhwdAADR1hGcHEAfgA/cQB+AD1zcQB+ADhwdAADTFhGcHEA
fgBBcQB+AD1zcQB+ADhwdAADTVhGcHEAfgBDcQB+AD1zcQB+ADhwdAAJUXVpY2tUaW1lcHEAfgBF
cQB+AD1zcQB+ADhwdAANV2luZG93cyBNZWRpYXB0AAxXaW5kb3dzTWVkaWFxAH4APXNxAH4AOHB0
AAlVc2VyIERhdGFwdAAJVXNlcl9kYXRhcQB+AD1zcQB+ADhwdAAOQW5jaWxsYXJ5IERhdGFwdAAO
QW5jaWxsYXJ5X2RhdGFxAH4APXNxAH4AOHB0AAJEVnBxAH4AUHEAfgA9c3EAfgA4cHQAA1ZDM3Bx
AH4AUnEAfgA9c3EAfgA4cHQAFkFWQyBQaWN0dXJlIFRpbWluZyBTRUlwdAAWQVZDX1BpY3R1cmVf
VGltaW5nX1NFSXEAfgA9c3EAfgA4cHQAEE1QRUcyIEdPUCBIZWFkZXJwdAAQTVBFRzJfR09QX2hl
YWRlcnEAfgA9c3EAfgA4cHQAFE1YRiBNYXRlcmlhbCBQYWNrYWdlcHQAFE1YRl9tYXRlcmlhbF9w
YWNrYWdlcQB+AD1zcQB+ADhwdAAQTVhGIFN5c3RlbSBUcmFja3B0ABBNWEZfc3lzdGVtX3RyYWNr
cQB+AD1zcQB+ADhwdAATQW5jaWxsYXJ5IERhdGEgVklUQ3B0ABNBbmNpbGxhcnlfZGF0YV9WSVRD
cQB+AD1zcQB+ADhwdAASQW5jaWxsYXJ5IERhdGEgTFRDcHQAEkFuY2lsbGFyeV9kYXRhX0xUQ3EA
fgA9c3EAfgA4cHQAEUhFVkMgVGltZWNvZGUgU0VJcHQAEUhFVkNfVGltZWNvZGVfU0VJcQB+AD1z
cQB+ADhwdAAQU0NURTIwIFVzZXIgRGF0YXB0ABBVc2VyX2RhdGFfU0NURTIwcQB+AD1zcQB+ADhw
dAAOQVRTQyBVc2VyIERhdGFwdAAOVXNlcl9kYXRhX0FUU0NxAH4APXNxAH4AOHBxAH4AbHB0AANT
Q0NxAH4APXNxAH4AOHB0ABlBbmNpbGxhcnkgRGF0YSBMZWdhY3kgNjA4cHQAGUFuY2lsbGFyeV9k
YXRhX2xlZ2FjeV82MDhxAH4APXNxAH4AOHB0AA1DdWUgUG9pbnQgWE1McHQAC0N1ZVBvaW50WE1M
cQB+AD14fnEAfgAmdAAGU1RSSU5Hc3EAfgAedABBVHJ1ZSBvbiB0aGUgbGFzdCBkYXRhIHBhY2tl
dCBvZiB0aGUgU3RyZWFtICh3aXRoIG9yIHdpdGhvdXQgZGF0YSlwcHQAC2VuZE9mU3RyZWFtcH5x
AH4AJnQAB0JPT0xFQU5zcQB+AB50ACxJbmRpY2F0ZXMgaWYgdGhlIGZyYW1lIHJhdGUgcmVtYWlu
cyBjb25zdGFudHBwdAATY29uc3RhbnRfZnJhbWVfcmF0ZXBxAH4Ae3NxAH4AHnQAGEluZGljYXRl
cyB0aGUgZnJhbWUgcmF0ZXBwdAAKZnJhbWVfcmF0ZXBxAH4AMHNxAH4AHnBwcHQAD2RhdGFfaXNf
bWlzc2luZ3BxAH4Ae3NxAH4AHnQAM1Bvc2l0aW9uIG9yIG9mZnNldCBmcm9tIHRoZSBiZWdpbm5p
bmcgb2YgdGhlIHN0cmVhbXBwdAAQcG9zaXRpb25JblN0cmVhbXB+cQB+ACZ0AARMT05Hc3EAfgAe
cHBwdAALY3VlcG9pbnRfaWRwcQB+AHZzcQB+AB50AC1UaGUgbW9zdCByZWxldmFudCB0aW1lIHBl
cnRhaW5pbmcgdG8gdGhlIGRhdGFwcHQABHRpbWVwcQB+AChzcQB+AB5wcHB0ABRkYXRhX2lzX21h
bnVmYWN0dXJlZHBxAH4Ae3NxAH4AHnQAJFByZS1yb2xsIG9mIHRoZSBjdWUgcG9pbnQgaW4gc2Vj
b25kc3BwdAAcY3VlcG9pbnRfcHJlc2VudGF0aW9uX29mZnNldHBxAH4AMHNxAH4AHnQAI1RvdGFs
IGxlbmd0aCBvZiB0aGUgc3RyZWFtIGlmIGtub3ducHB0AA5sZW5ndGhPZlN0cmVhbXBxAH4AiHhw
c3IAEWphdmEudXRpbC5IYXNoTWFwBQfawcMWYNEDAAJGAApsb2FkRmFjdG9ySQAJdGhyZXNob2xk
eHA/AAAAAAAAIHcIAAAAQAAAAAFxAH4ANXNyACZqYXZhLnV0aWwuQ29sbGVjdGlvbnMkVW5tb2Rp
ZmlhYmxlTGlzdPwPJTG17I4QAgABTAAEbGlzdHEAfgAfeHIALGphdmEudXRpbC5Db2xsZWN0aW9u
cyRVbm1vZGlmaWFibGVDb2xsZWN0aW9uGUIAgMte9x4CAAFMAAFjdAAWTGphdmEvdXRpbC9Db2xs
ZWN0aW9uO3hwc3EAfgA2AAAAAXcEAAAAAXEAfgBOeHEAfgCdeA==</property>
                        <pinDefinition name="SCTE104OperationMessage" displayName="Cue Points (SCTE 104)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="AncillaryData" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAASdAAGRW5kaWFudAAJRnJhbWVSYXRldAAKU3lz
dGVtRGF0YXQADURhdGFJc01pc3Npbmd0AAtNZWRpYVRpbWluZ3QADUFuY2lsbGFyeURhdGF0ABJE
YXRhSXNNYW51ZmFjdHVyZWR0ABFTYW1wbGVJbmZvcm1hdGlvbnQADFNhbXBsZUZvcm1hdHQACUNv
bnRhaW5lcnQACFRlbXBvcmFsdAALS2F5YWtCdWZmZXJ0AApCeXRlU3RyZWFtdAATQW5jaWxsYXJ5
RGF0YVNhbXBsZXQABlN0cmVhbXQAEFN5c3RlbURhdGFTYW1wbGV0ABNBbmNpbGxhcnlEYXRhU3Ry
ZWFtdAAQU3lzdGVtRGF0YVN0cmVhbXhzcQB+AAh3DAAAACA/QAAAAAAAEXNyAE9jYS5kaWdpdGFs
cmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZpbml0aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlv
biRTaW1wbGVUeXBlAAAAAAAAAAECAAJMABFlbnVtZXJhdGlvblZhbHVlc3QAEExqYXZhL3V0aWwv
TGlzdDtMAAR0eXBldABDTGNhL2RpZ2l0YWxyYXBpZHMva2F5YWsvZGF0YXR5cGVzL2RlZmluaXRp
b24vbW9kZWwvU2ltcGxlVHlwZXNFbnVtO3hyAFJjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0
eXBlcy5kZWZpbml0aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlvbiRLZXlEZWZpbml0aW9uAAAA
AAAAAAECAARMAAdjb21tZW50cQB+AAVMAAtkaXNwbGF5TmFtZXEAfgAFTAALbXVsdGlWYWx1ZWR0
ABNMamF2YS9sYW5nL0Jvb2xlYW47TAAEbmFtZXEAfgAFeHB0AElUaGUgdGltZSBwZXJ0YWluaW5n
IHRvIHRoZSBlbmQgb2YgdGhlIGRhdGEgKHRpbWUgKyBkdXJhdGlvbiBvZiB0aGlzIGRhdGEpcHB0
AAd0aW1lRW5kcH5yAEFjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZpbml0aW9u
Lm1vZGVsLlNpbXBsZVR5cGVzRW51bQAAAAAAAAAAEgAAeHIADmphdmEubGFuZy5FbnVtAAAAAAAA
AAASAAB4cHQABFRJTUVzcQB+AB10ABxJbmRpY2F0ZXMgdGhlIG1lZGlhIGR1cmF0aW9ucHB0AA5t
ZWRpYV9kdXJhdGlvbnBxAH4AJ3NxAH4AHXQAQVRydWUgb24gdGhlIGxhc3QgZGF0YSBwYWNrZXQg
b2YgdGhlIFN0cmVhbSAod2l0aCBvciB3aXRob3V0IGRhdGEpcHB0AAtlbmRPZlN0cmVhbXB+cQB+
ACV0AAdCT09MRUFOc3EAfgAddABGTGV2ZWwgb2YgcHJlY2lzaW9uIC0gY2FuIGJlIGxvd2VyIHRo
YW4gdGhlIGFjdHVhbCBudW1iZXIgb2YgdmFsaWQgYml0c3BwdAAYYWNjdXJhY3lfYml0c19wZXJf
c2FtcGxlcH5xAH4AJXQAB0lOVEVHRVJzcQB+AB10ACxJbmRpY2F0ZXMgaWYgdGhlIGZyYW1lIHJh
dGUgcmVtYWlucyBjb25zdGFudHBwdAATY29uc3RhbnRfZnJhbWVfcmF0ZXBxAH4AL3NxAH4AHXQA
MVRvdGFsIG51bWJlciBvZiB2YWxpZCBhbmQgaW52YWxpZCBiaXRzIHBlciBzYW1wbGVwcHQAF3N0
b3JhZ2VfYml0c19wZXJfc2FtcGxlcHEAfgA0c3EAfgAddAAYSW5kaWNhdGVzIHRoZSBmcmFtZSBy
YXRlcHB0AApmcmFtZV9yYXRlcH5xAH4AJXQACFJBVElPTkFMc3EAfgAddAAXSW5kaWNhdGVzIGJ5
dGUgb3JkZXJpbmdwcHQABmVuZGlhbnNyABNqYXZhLnV0aWwuQXJyYXlMaXN0eIHSHZnHYZ0DAAFJ
AARzaXpleHAAAAACdwQAAAACc3IAN2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsucGx1Z2luLnhtbC5L
YXlha0VudW1lcmF0aW9uVmFsdWUAAAAAAAAAAQIABUwAC2Rlc2NyaXB0aW9ucQB+AAVMAAtkaXNw
bGF5TmFtZXEAfgAFTAAGaGlkZGVucQB+ACFMAA52YWx1ZUF0dHJpYnV0ZXEAfgAFTAANdmFsdWVF
bWJlZGRlZHEAfgAFeHBwcHBwdAADYmlnc3EAfgBGcHBwcHQABmxpdHRsZXh+cQB+ACV0AAZTVFJJ
TkdzcQB+AB1wcHB0AA9kYXRhX2lzX21pc3NpbmdwcQB+AC9zcQB+AB10ADNQb3NpdGlvbiBvciBv
ZmZzZXQgZnJvbSB0aGUgYmVnaW5uaW5nIG9mIHRoZSBzdHJlYW1wcHQAEHBvc2l0aW9uSW5TdHJl
YW1wfnEAfgAldAAETE9OR3NyAFBjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZp
bml0aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlvbiRDb21wbGV4VHlwZQAAAAAAAAABAgACTAAI
b3B0aW9uYWxxAH4AIUwABHR5cGVxAH4ABXhxAH4AIHQARkluZGljYXRlcyB0aGUgbGlzdCBvZiBh
bmNpbGxhcnkgZGF0YSBwYWNrZXQgdHlwZXMgZm91bmQgaW4gdGhpcyBzdHJlYW1wc3IAEWphdmEu
bGFuZy5Cb29sZWFuzSBygNWc+u4CAAFaAAV2YWx1ZXhwAXQAEGFuY19kYXRhX3BhY2tldHNwdAAT
QW5jaWxsYXJ5RGF0YVBhY2tldHNxAH4AHXQAKkludGVycHJldCB0aGUgc2FtcGxlIGFzIHNpZ25l
ZCBvciB1bnNpZ25lZHBwdAANc2FtcGxlX3NpZ25lZHBxAH4AL3NxAH4AHXQAH051bWJlciBvZiB2
YWxpZCBiaXRzIHBlciBzYW1wbGVwcHQAD2JpdHNfcGVyX3NhbXBsZXBxAH4ANHNxAH4AHXQALVRo
ZSBtb3N0IHJlbGV2YW50IHRpbWUgcGVydGFpbmluZyB0byB0aGUgZGF0YXBwdAAEdGltZXBxAH4A
J3NxAH4AVHQANUluZGljYXRlcyB0aGUgdGFyZ2V0IHZpZGVvIGZvcm1hdCBmb3IgdGhlc2Ugc3Vi
dGl0bGVzcHB0ABpzeXN0ZW1fdGFyZ2V0X3ZpZGVvX2Zvcm1hdHEAfgBYdAAFVmlkZW9zcQB+AB1w
cHB0ABRkYXRhX2lzX21hbnVmYWN0dXJlZHBxAH4AL3NxAH4AHXQAI1RvdGFsIGxlbmd0aCBvZiB0
aGUgc3RyZWFtIGlmIGtub3ducHB0AA5sZW5ndGhPZlN0cmVhbXBxAH4AUnhwc3IAEWphdmEudXRp
bC5IYXNoTWFwBQfawcMWYNEDAAJGAApsb2FkRmFjdG9ySQAJdGhyZXNob2xkeHA/AAAAAAAAIHcI
AAAAQAAAAAFxAH4AWXNyACZqYXZhLnV0aWwuQ29sbGVjdGlvbnMkVW5tb2RpZmlhYmxlTGlzdPwP
JTG17I4QAgABTAAEbGlzdHEAfgAeeHIALGphdmEudXRpbC5Db2xsZWN0aW9ucyRVbm1vZGlmaWFi
bGVDb2xsZWN0aW9uGUIAgMte9x4CAAFMAAFjdAAWTGphdmEvdXRpbC9Db2xsZWN0aW9uO3hwc3EA
fgBEAAAAAHcEAAAAAHhxAH4Ac3g=</property>
                        <pinDefinition name="AncillaryData" displayName="Unknown Ancillary Data" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="AncillaryData 2" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAASdAAGRW5kaWFudAAJRnJhbWVSYXRldAAKU3lz
dGVtRGF0YXQADURhdGFJc01pc3Npbmd0AAtNZWRpYVRpbWluZ3QADUFuY2lsbGFyeURhdGF0ABJE
YXRhSXNNYW51ZmFjdHVyZWR0ABFTYW1wbGVJbmZvcm1hdGlvbnQADFNhbXBsZUZvcm1hdHQACUNv
bnRhaW5lcnQACFRlbXBvcmFsdAALS2F5YWtCdWZmZXJ0AApCeXRlU3RyZWFtdAATQW5jaWxsYXJ5
RGF0YVNhbXBsZXQABlN0cmVhbXQAEFN5c3RlbURhdGFTYW1wbGV0ABNBbmNpbGxhcnlEYXRhU3Ry
ZWFtdAAQU3lzdGVtRGF0YVN0cmVhbXhzcQB+AAh3DAAAACA/QAAAAAAAEXNyAE9jYS5kaWdpdGFs
cmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZpbml0aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlv
biRTaW1wbGVUeXBlAAAAAAAAAAECAAJMABFlbnVtZXJhdGlvblZhbHVlc3QAEExqYXZhL3V0aWwv
TGlzdDtMAAR0eXBldABDTGNhL2RpZ2l0YWxyYXBpZHMva2F5YWsvZGF0YXR5cGVzL2RlZmluaXRp
b24vbW9kZWwvU2ltcGxlVHlwZXNFbnVtO3hyAFJjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0
eXBlcy5kZWZpbml0aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlvbiRLZXlEZWZpbml0aW9uAAAA
AAAAAAECAARMAAdjb21tZW50cQB+AAVMAAtkaXNwbGF5TmFtZXEAfgAFTAALbXVsdGlWYWx1ZWR0
ABNMamF2YS9sYW5nL0Jvb2xlYW47TAAEbmFtZXEAfgAFeHB0AElUaGUgdGltZSBwZXJ0YWluaW5n
IHRvIHRoZSBlbmQgb2YgdGhlIGRhdGEgKHRpbWUgKyBkdXJhdGlvbiBvZiB0aGlzIGRhdGEpcHB0
AAd0aW1lRW5kcH5yAEFjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZpbml0aW9u
Lm1vZGVsLlNpbXBsZVR5cGVzRW51bQAAAAAAAAAAEgAAeHIADmphdmEubGFuZy5FbnVtAAAAAAAA
AAASAAB4cHQABFRJTUVzcQB+AB10ABxJbmRpY2F0ZXMgdGhlIG1lZGlhIGR1cmF0aW9ucHB0AA5t
ZWRpYV9kdXJhdGlvbnBxAH4AJ3NxAH4AHXQAQVRydWUgb24gdGhlIGxhc3QgZGF0YSBwYWNrZXQg
b2YgdGhlIFN0cmVhbSAod2l0aCBvciB3aXRob3V0IGRhdGEpcHB0AAtlbmRPZlN0cmVhbXB+cQB+
ACV0AAdCT09MRUFOc3EAfgAddABGTGV2ZWwgb2YgcHJlY2lzaW9uIC0gY2FuIGJlIGxvd2VyIHRo
YW4gdGhlIGFjdHVhbCBudW1iZXIgb2YgdmFsaWQgYml0c3BwdAAYYWNjdXJhY3lfYml0c19wZXJf
c2FtcGxlcH5xAH4AJXQAB0lOVEVHRVJzcQB+AB10ACxJbmRpY2F0ZXMgaWYgdGhlIGZyYW1lIHJh
dGUgcmVtYWlucyBjb25zdGFudHBwdAATY29uc3RhbnRfZnJhbWVfcmF0ZXBxAH4AL3NxAH4AHXQA
MVRvdGFsIG51bWJlciBvZiB2YWxpZCBhbmQgaW52YWxpZCBiaXRzIHBlciBzYW1wbGVwcHQAF3N0
b3JhZ2VfYml0c19wZXJfc2FtcGxlcHEAfgA0c3EAfgAddAAYSW5kaWNhdGVzIHRoZSBmcmFtZSBy
YXRlcHB0AApmcmFtZV9yYXRlcH5xAH4AJXQACFJBVElPTkFMc3EAfgAddAAXSW5kaWNhdGVzIGJ5
dGUgb3JkZXJpbmdwcHQABmVuZGlhbnNyABNqYXZhLnV0aWwuQXJyYXlMaXN0eIHSHZnHYZ0DAAFJ
AARzaXpleHAAAAACdwQAAAACc3IAN2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsucGx1Z2luLnhtbC5L
YXlha0VudW1lcmF0aW9uVmFsdWUAAAAAAAAAAQIABUwAC2Rlc2NyaXB0aW9ucQB+AAVMAAtkaXNw
bGF5TmFtZXEAfgAFTAAGaGlkZGVucQB+ACFMAA52YWx1ZUF0dHJpYnV0ZXEAfgAFTAANdmFsdWVF
bWJlZGRlZHEAfgAFeHBwcHBwdAADYmlnc3EAfgBGcHBwcHQABmxpdHRsZXh+cQB+ACV0AAZTVFJJ
TkdzcQB+AB1wcHB0AA9kYXRhX2lzX21pc3NpbmdwcQB+AC9zcQB+AB10ADNQb3NpdGlvbiBvciBv
ZmZzZXQgZnJvbSB0aGUgYmVnaW5uaW5nIG9mIHRoZSBzdHJlYW1wcHQAEHBvc2l0aW9uSW5TdHJl
YW1wfnEAfgAldAAETE9OR3NyAFBjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZp
bml0aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlvbiRDb21wbGV4VHlwZQAAAAAAAAABAgACTAAI
b3B0aW9uYWxxAH4AIUwABHR5cGVxAH4ABXhxAH4AIHQARkluZGljYXRlcyB0aGUgbGlzdCBvZiBh
bmNpbGxhcnkgZGF0YSBwYWNrZXQgdHlwZXMgZm91bmQgaW4gdGhpcyBzdHJlYW1wc3IAEWphdmEu
bGFuZy5Cb29sZWFuzSBygNWc+u4CAAFaAAV2YWx1ZXhwAXQAEGFuY19kYXRhX3BhY2tldHNwdAAT
QW5jaWxsYXJ5RGF0YVBhY2tldHNxAH4AHXQAKkludGVycHJldCB0aGUgc2FtcGxlIGFzIHNpZ25l
ZCBvciB1bnNpZ25lZHBwdAANc2FtcGxlX3NpZ25lZHBxAH4AL3NxAH4AHXQAH051bWJlciBvZiB2
YWxpZCBiaXRzIHBlciBzYW1wbGVwcHQAD2JpdHNfcGVyX3NhbXBsZXBxAH4ANHNxAH4AHXQALVRo
ZSBtb3N0IHJlbGV2YW50IHRpbWUgcGVydGFpbmluZyB0byB0aGUgZGF0YXBwdAAEdGltZXBxAH4A
J3NxAH4AVHQANUluZGljYXRlcyB0aGUgdGFyZ2V0IHZpZGVvIGZvcm1hdCBmb3IgdGhlc2Ugc3Vi
dGl0bGVzcHB0ABpzeXN0ZW1fdGFyZ2V0X3ZpZGVvX2Zvcm1hdHEAfgBYdAAFVmlkZW9zcQB+AB1w
cHB0ABRkYXRhX2lzX21hbnVmYWN0dXJlZHBxAH4AL3NxAH4AHXQAI1RvdGFsIGxlbmd0aCBvZiB0
aGUgc3RyZWFtIGlmIGtub3ducHB0AA5sZW5ndGhPZlN0cmVhbXBxAH4AUnhwc3IAEWphdmEudXRp
bC5IYXNoTWFwBQfawcMWYNEDAAJGAApsb2FkRmFjdG9ySQAJdGhyZXNob2xkeHA/AAAAAAAAIHcI
AAAAQAAAAAFxAH4AWXNyACZqYXZhLnV0aWwuQ29sbGVjdGlvbnMkVW5tb2RpZmlhYmxlTGlzdPwP
JTG17I4QAgABTAAEbGlzdHEAfgAeeHIALGphdmEudXRpbC5Db2xsZWN0aW9ucyRVbm1vZGlmaWFi
bGVDb2xsZWN0aW9uGUIAgMte9x4CAAFMAAFjdAAWTGphdmEvdXRpbC9Db2xsZWN0aW9uO3hwc3EA
fgBEAAAAAHcEAAAAAHhxAH4Ac3g=</property>
                        <pinDefinition name="AncillaryData 2" displayName="Ancillary Data (DNxHD)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode 5" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAED9AAAAAAAALdAAJRnJhbWVSYXRldAAOVGltZWNvZGVTdHJl
YW10AAhUZW1wb3JhbHQACFRpbWVjb2RldAALTWVkaWFUaW1pbmd0AA1EYXRhSXNNaXNzaW5ndAAL
TWVkaWFPcmlnaW50AAZTdHJlYW10ABFTYW1wbGVJbmZvcm1hdGlvbnQAGVRpbWVjb2RlU2FtcGxl
SW5mb3JtYXRpb250AA5UaW1lY29kZVNhbXBsZXhzcQB+AAh3DAAAACA/QAAAAAAAEnNyAE9jYS5k
aWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZpbml0aW9uLm1vZGVsLkRhdGFUeXBlRGVm
aW5pdGlvbiRTaW1wbGVUeXBlAAAAAAAAAAECAAJMABFlbnVtZXJhdGlvblZhbHVlc3QAEExqYXZh
L3V0aWwvTGlzdDtMAAR0eXBldABDTGNhL2RpZ2l0YWxyYXBpZHMva2F5YWsvZGF0YXR5cGVzL2Rl
ZmluaXRpb24vbW9kZWwvU2ltcGxlVHlwZXNFbnVtO3hyAFJjYS5kaWdpdGFscmFwaWRzLmtheWFr
LmRhdGF0eXBlcy5kZWZpbml0aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlvbiRLZXlEZWZpbml0
aW9uAAAAAAAAAAECAARMAAdjb21tZW50cQB+AAVMAAtkaXNwbGF5TmFtZXEAfgAFTAALbXVsdGlW
YWx1ZWR0ABNMamF2YS9sYW5nL0Jvb2xlYW47TAAEbmFtZXEAfgAFeHB0ABxJbmRpY2F0ZXMgdGhl
IG1lZGlhIGR1cmF0aW9ucHB0AA5tZWRpYV9kdXJhdGlvbnB+cgBBY2EuZGlnaXRhbHJhcGlkcy5r
YXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5TaW1wbGVUeXBlc0VudW0AAAAAAAAAABIA
AHhyAA5qYXZhLmxhbmcuRW51bQAAAAAAAAAAEgAAeHB0AARUSU1Fc3EAfgAWdABJVGhlIHRpbWUg
cGVydGFpbmluZyB0byB0aGUgZW5kIG9mIHRoZSBkYXRhICh0aW1lICsgZHVyYXRpb24gb2YgdGhp
cyBkYXRhKXBwdAAHdGltZUVuZHBxAH4AIHNxAH4AFnQAR3RydWUgaWYgdGhlIHRpbWVjb2RlIHJl
c2V0IHRvIDAwOjAwOjAwOjAwIHdoZW4gcmVhY2hpbmcgYSBjZXJ0YWluIHZhbHVlcHB0AA50aW1l
Y29kZV9yZXNldHNyABNqYXZhLnV0aWwuQXJyYXlMaXN0eIHSHZnHYZ0DAAFJAARzaXpleHAAAAAD
dwQAAAADc3IAN2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsucGx1Z2luLnhtbC5LYXlha0VudW1lcmF0
aW9uVmFsdWUAAAAAAAAAAQIABUwAC2Rlc2NyaXB0aW9ucQB+AAVMAAtkaXNwbGF5TmFtZXEAfgAF
TAAGaGlkZGVucQB+ABpMAA52YWx1ZUF0dHJpYnV0ZXEAfgAFTAANdmFsdWVFbWJlZGRlZHEAfgAF
eHBwdAAETm9uZXB0AARub25ldAAAc3EAfgAqcHQABzEyIGhvdXJwdAADMTJocQB+AC5zcQB+ACpw
dAAHMjQgaG91cnB0AAMyNGhxAH4ALnh+cQB+AB50AAZTVFJJTkdzcQB+ABZwcHNyABFqYXZhLmxh
bmcuQm9vbGVhbs0gcoDVnPruAgABWgAFdmFsdWV4cAF0AAxtZWRpYV9vcmlnaW5zcQB+ACgAAAAX
dwQAAAAXc3EAfgAqcHBwcHQADE1hbnVmYWN0dXJlZHNxAH4AKnB0AAhEUkNWaWRlb3BxAH4AP3EA
fgAuc3EAfgAqcHQAA0dYRnBxAH4AQXEAfgAuc3EAfgAqcHQAA0xYRnBxAH4AQ3EAfgAuc3EAfgAq
cHQAA01YRnBxAH4ARXEAfgAuc3EAfgAqcHQACVF1aWNrVGltZXBxAH4AR3EAfgAuc3EAfgAqcHQA
DVdpbmRvd3MgTWVkaWFwdAAMV2luZG93c01lZGlhcQB+AC5zcQB+ACpwdAAJVXNlciBEYXRhcHQA
CVVzZXJfZGF0YXEAfgAuc3EAfgAqcHQADkFuY2lsbGFyeSBEYXRhcHQADkFuY2lsbGFyeV9kYXRh
cQB+AC5zcQB+ACpwdAACRFZwcQB+AFJxAH4ALnNxAH4AKnB0AANWQzNwcQB+AFRxAH4ALnNxAH4A
KnB0ABZBVkMgUGljdHVyZSBUaW1pbmcgU0VJcHQAFkFWQ19QaWN0dXJlX1RpbWluZ19TRUlxAH4A
LnNxAH4AKnB0ABBNUEVHMiBHT1AgSGVhZGVycHQAEE1QRUcyX0dPUF9oZWFkZXJxAH4ALnNxAH4A
KnB0ABRNWEYgTWF0ZXJpYWwgUGFja2FnZXB0ABRNWEZfbWF0ZXJpYWxfcGFja2FnZXEAfgAuc3EA
fgAqcHQAEE1YRiBTeXN0ZW0gVHJhY2twdAAQTVhGX3N5c3RlbV90cmFja3EAfgAuc3EAfgAqcHQA
E0FuY2lsbGFyeSBEYXRhIFZJVENwdAATQW5jaWxsYXJ5X2RhdGFfVklUQ3EAfgAuc3EAfgAqcHQA
EkFuY2lsbGFyeSBEYXRhIExUQ3B0ABJBbmNpbGxhcnlfZGF0YV9MVENxAH4ALnNxAH4AKnB0ABFI
RVZDIFRpbWVjb2RlIFNFSXB0ABFIRVZDX1RpbWVjb2RlX1NFSXEAfgAuc3EAfgAqcHQAEFNDVEUy
MCBVc2VyIERhdGFwdAAQVXNlcl9kYXRhX1NDVEUyMHEAfgAuc3EAfgAqcHQADkFUU0MgVXNlciBE
YXRhcHQADlVzZXJfZGF0YV9BVFNDcQB+AC5zcQB+ACpwcQB+AG5wdAADU0NDcQB+AC5zcQB+ACpw
dAAZQW5jaWxsYXJ5IERhdGEgTGVnYWN5IDYwOHB0ABlBbmNpbGxhcnlfZGF0YV9sZWdhY3lfNjA4
cQB+AC5zcQB+ACpwdAANQ3VlIFBvaW50IFhNTHB0AAtDdWVQb2ludFhNTHEAfgAueHEAfgA1c3EA
fgAWdAAidHJ1ZSBpZiB0aGUgdGltZWNvZGUgaXMgZHJvcCBmcmFtZXBwdAATdGltZWNvZGVfZHJv
cF9mcmFtZXB+cQB+AB50AAdCT09MRUFOc3EAfgAWdABBVHJ1ZSBvbiB0aGUgbGFzdCBkYXRhIHBh
Y2tldCBvZiB0aGUgU3RyZWFtICh3aXRoIG9yIHdpdGhvdXQgZGF0YSlwcHQAC2VuZE9mU3RyZWFt
cHEAfgB7c3EAfgAWdAAYSW5kaWNhdGVzIHRoZSBmcmFtZSByYXRlcHB0ABN0aW1lY29kZV9mcmFt
ZV9yYXRlcH5xAH4AHnQAB0lOVEVHRVJzcQB+ABZ0AB5JbmRpY2F0ZXMgdGhlIDMyLWJpdCB1c2Vy
IGRhdGFwcHQAEnRpbWVjb2RlX3VzZXJfYml0c3BxAH4Ag3NxAH4AFnQALEluZGljYXRlcyBpZiB0
aGUgZnJhbWUgcmF0ZSByZW1haW5zIGNvbnN0YW50cHB0ABNjb25zdGFudF9mcmFtZV9yYXRlcHEA
fgB7c3EAfgAWdAAYSW5kaWNhdGVzIHRoZSBmcmFtZSByYXRlcHB0AApmcmFtZV9yYXRlcH5xAH4A
HnQACFJBVElPTkFMc3EAfgAWcHBwdAAPZGF0YV9pc19taXNzaW5ncHEAfgB7c3EAfgAWdAAjSW5k
aWNhdGVzIHRoZSB0aW1lY29kZSBmaWVsZCBudW1iZXJwcHQADnRpbWVjb2RlX2ZpZWxkc3EAfgAo
AAAAAncEAAAAAnNxAH4AKnB0AAExcHEAfgCXcQB+AC5zcQB+ACpwdAABMnBxAH4AmXEAfgAueHEA
fgCDc3EAfgAWdAARVGhlIHRpbWVjb2RlIHR5cGVwcHQADXRpbWVjb2RlX3R5cGVzcQB+ACgAAAAD
dwQAAAADc3EAfgAqcHQACFRpbWVjb2RlcHQACHRpbWVjb2RlcQB+AC5zcQB+ACpwdAARTG9jYWwg
dGltZSBvZiBkYXlwdAARbG9jYWxfdGltZV9vZl9kYXlxAH4ALnNxAH4AKnB0AA9VVEMgdGltZSBv
ZiBkYXlwdAAPVVRDX3RpbWVfb2ZfZGF5cQB+AC54cQB+ADVzcQB+ABZ0AC1UaGUgbW9zdCByZWxl
dmFudCB0aW1lIHBlcnRhaW5pbmcgdG8gdGhlIGRhdGFwcHQABHRpbWVwcQB+ACBzcQB+ABZ0ACdJ
bmRpY2F0ZXMgaWYgdGhlIHRpbWVjb2RlIGlzIGNvbnRpbnVvdXNwcHQAFnRpbWVjb2RlX2lzX2Nv
bnRpbnVvdXNwcQB+AHtzcQB+ABZ0ACBJbmRpY2F0ZXMgdGhlIGJpbmFyeSBncm91cCBmbGFnc3Bw
dAAbdGltZWNvZGVfYmluYXJ5X2dyb3VwX2ZsYWdzcHEAfgCDc3EAfgAWdABBdHJ1ZSBpZiB0aGUg
dGltZWNvZGUgc3RyZWFtIGNvbnRhaW5zIG9uZSB0aW1lY29kZSB2YWx1ZSBwZXIgZmllbGRwcHQA
F3RpbWVjb2RlX2lzX2ZpZWxkX2Jhc2VkcHEAfgB7c3EAfgAWdABBdHJ1ZSBpZiB0aGUgdGltZSBj
b2RlIGlzIHN5bmNocm9uaXNlZCB3aXRoIGEgY29sb3IgZmllbGQgc2VxdWVuY2VwcHQAGXRpbWVj
b2RlX2NvbG9yX2ZyYW1lX2ZsYWdwcQB+AHt4cHNyABFqYXZhLnV0aWwuSGFzaE1hcAUH2sHDFmDR
AwACRgAKbG9hZEZhY3RvckkACXRocmVzaG9sZHhwPwAAAAAAACB3CAAAAEAAAAAJcQB+AB1zcgAk
Y2EuZGlnaXRhbHJhcGlkcy5rYXlhay50aW1lLlRpbWVJbXBsAAAAAAAAAAECAAJMAAhyYXRpb25h
bHQAMUxjYS9kaWdpdGFscmFwaWRzL2theWFrL2RhdGEvaW1wbC9SYXRpb25hbE51bWJlcjtMAAh0
aW1lQmFzZXQAJkxjYS9kaWdpdGFscmFwaWRzL2theWFrL3RpbWUvVGltZUJhc2U7eHBzcgAvY2Eu
ZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhLmltcGwuUmF0aW9uYWxOdW1iZXIAAAAAAAAAAQIABEoA
C2Rlbm9taW5hdG9yWgAJaXNSZWR1Y2VkWgATbmVlZHNCaWdGb3JNdWx0aXBseUoACW51bWVyYXRv
cnhyABBqYXZhLmxhbmcuTnVtYmVyhqyVHQuU4IsCAAB4cAAAAAAAAHUwAAAAAAAAACd7wXNyAChj
YS5kaWdpdGFscmFwaWRzLmtheWFrLnRpbWUuVGltZUJhc2VJbXBsAAAAAAAAAAECAAFMAA5vZmZz
ZXRSYXRpb25hbHEAfgC5eHBzcQB+ALwAAAAAO5rKAAAAAAAAAAAAAABxAH4AJ3QAAzI0aHEAfgB6
c3EAfgA4AHEAfgCscQB+ADlxAH4AtXEAfgDDcQB+ADpzcgAmamF2YS51dGlsLkNvbGxlY3Rpb25z
JFVubW9kaWZpYWJsZUxpc3T8DyUxteyOEAIAAUwABGxpc3RxAH4AF3hyACxqYXZhLnV0aWwuQ29s
bGVjdGlvbnMkVW5tb2RpZmlhYmxlQ29sbGVjdGlvbhlCAIDLXvceAgABTAABY3QAFkxqYXZhL3V0
aWwvQ29sbGVjdGlvbjt4cHNxAH4AKAAAAAF3BAAAAAF0ABRNWEZfbWF0ZXJpYWxfcGFja2FnZXhx
AH4AyHEAfgCCc3IAEWphdmEubGFuZy5JbnRlZ2VyEuKgpPeBhzgCAAFJAAV2YWx1ZXhxAH4AvQAA
AB5xAH4AjXNxAH4AvAAAAAAAAAPpAQAAAAAAAAB1MHEAfgCcdAAIdGltZWNvZGV4</property>
                        <pinDefinition name="Timecode 5" displayName="Timecode (MXF Material Package)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode 6" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAED9AAAAAAAALdAAJRnJhbWVSYXRldAAOVGltZWNvZGVTdHJl
YW10AAhUZW1wb3JhbHQACFRpbWVjb2RldAALTWVkaWFUaW1pbmd0AA1EYXRhSXNNaXNzaW5ndAAL
TWVkaWFPcmlnaW50AAZTdHJlYW10ABFTYW1wbGVJbmZvcm1hdGlvbnQAGVRpbWVjb2RlU2FtcGxl
SW5mb3JtYXRpb250AA5UaW1lY29kZVNhbXBsZXhzcQB+AAh3DAAAACA/QAAAAAAAEnNyAE9jYS5k
aWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZpbml0aW9uLm1vZGVsLkRhdGFUeXBlRGVm
aW5pdGlvbiRTaW1wbGVUeXBlAAAAAAAAAAECAAJMABFlbnVtZXJhdGlvblZhbHVlc3QAEExqYXZh
L3V0aWwvTGlzdDtMAAR0eXBldABDTGNhL2RpZ2l0YWxyYXBpZHMva2F5YWsvZGF0YXR5cGVzL2Rl
ZmluaXRpb24vbW9kZWwvU2ltcGxlVHlwZXNFbnVtO3hyAFJjYS5kaWdpdGFscmFwaWRzLmtheWFr
LmRhdGF0eXBlcy5kZWZpbml0aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlvbiRLZXlEZWZpbml0
aW9uAAAAAAAAAAECAARMAAdjb21tZW50cQB+AAVMAAtkaXNwbGF5TmFtZXEAfgAFTAALbXVsdGlW
YWx1ZWR0ABNMamF2YS9sYW5nL0Jvb2xlYW47TAAEbmFtZXEAfgAFeHB0ABxJbmRpY2F0ZXMgdGhl
IG1lZGlhIGR1cmF0aW9ucHB0AA5tZWRpYV9kdXJhdGlvbnB+cgBBY2EuZGlnaXRhbHJhcGlkcy5r
YXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5TaW1wbGVUeXBlc0VudW0AAAAAAAAAABIA
AHhyAA5qYXZhLmxhbmcuRW51bQAAAAAAAAAAEgAAeHB0AARUSU1Fc3EAfgAWdABJVGhlIHRpbWUg
cGVydGFpbmluZyB0byB0aGUgZW5kIG9mIHRoZSBkYXRhICh0aW1lICsgZHVyYXRpb24gb2YgdGhp
cyBkYXRhKXBwdAAHdGltZUVuZHBxAH4AIHNxAH4AFnQAR3RydWUgaWYgdGhlIHRpbWVjb2RlIHJl
c2V0IHRvIDAwOjAwOjAwOjAwIHdoZW4gcmVhY2hpbmcgYSBjZXJ0YWluIHZhbHVlcHB0AA50aW1l
Y29kZV9yZXNldHNyABNqYXZhLnV0aWwuQXJyYXlMaXN0eIHSHZnHYZ0DAAFJAARzaXpleHAAAAAD
dwQAAAADc3IAN2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsucGx1Z2luLnhtbC5LYXlha0VudW1lcmF0
aW9uVmFsdWUAAAAAAAAAAQIABUwAC2Rlc2NyaXB0aW9ucQB+AAVMAAtkaXNwbGF5TmFtZXEAfgAF
TAAGaGlkZGVucQB+ABpMAA52YWx1ZUF0dHJpYnV0ZXEAfgAFTAANdmFsdWVFbWJlZGRlZHEAfgAF
eHBwdAAETm9uZXB0AARub25ldAAAc3EAfgAqcHQABzEyIGhvdXJwdAADMTJocQB+AC5zcQB+ACpw
dAAHMjQgaG91cnB0AAMyNGhxAH4ALnh+cQB+AB50AAZTVFJJTkdzcQB+ABZwcHNyABFqYXZhLmxh
bmcuQm9vbGVhbs0gcoDVnPruAgABWgAFdmFsdWV4cAF0AAxtZWRpYV9vcmlnaW5zcQB+ACgAAAAX
dwQAAAAXc3EAfgAqcHBwcHQADE1hbnVmYWN0dXJlZHNxAH4AKnB0AAhEUkNWaWRlb3BxAH4AP3EA
fgAuc3EAfgAqcHQAA0dYRnBxAH4AQXEAfgAuc3EAfgAqcHQAA0xYRnBxAH4AQ3EAfgAuc3EAfgAq
cHQAA01YRnBxAH4ARXEAfgAuc3EAfgAqcHQACVF1aWNrVGltZXBxAH4AR3EAfgAuc3EAfgAqcHQA
DVdpbmRvd3MgTWVkaWFwdAAMV2luZG93c01lZGlhcQB+AC5zcQB+ACpwdAAJVXNlciBEYXRhcHQA
CVVzZXJfZGF0YXEAfgAuc3EAfgAqcHQADkFuY2lsbGFyeSBEYXRhcHQADkFuY2lsbGFyeV9kYXRh
cQB+AC5zcQB+ACpwdAACRFZwcQB+AFJxAH4ALnNxAH4AKnB0AANWQzNwcQB+AFRxAH4ALnNxAH4A
KnB0ABZBVkMgUGljdHVyZSBUaW1pbmcgU0VJcHQAFkFWQ19QaWN0dXJlX1RpbWluZ19TRUlxAH4A
LnNxAH4AKnB0ABBNUEVHMiBHT1AgSGVhZGVycHQAEE1QRUcyX0dPUF9oZWFkZXJxAH4ALnNxAH4A
KnB0ABRNWEYgTWF0ZXJpYWwgUGFja2FnZXB0ABRNWEZfbWF0ZXJpYWxfcGFja2FnZXEAfgAuc3EA
fgAqcHQAEE1YRiBTeXN0ZW0gVHJhY2twdAAQTVhGX3N5c3RlbV90cmFja3EAfgAuc3EAfgAqcHQA
E0FuY2lsbGFyeSBEYXRhIFZJVENwdAATQW5jaWxsYXJ5X2RhdGFfVklUQ3EAfgAuc3EAfgAqcHQA
EkFuY2lsbGFyeSBEYXRhIExUQ3B0ABJBbmNpbGxhcnlfZGF0YV9MVENxAH4ALnNxAH4AKnB0ABFI
RVZDIFRpbWVjb2RlIFNFSXB0ABFIRVZDX1RpbWVjb2RlX1NFSXEAfgAuc3EAfgAqcHQAEFNDVEUy
MCBVc2VyIERhdGFwdAAQVXNlcl9kYXRhX1NDVEUyMHEAfgAuc3EAfgAqcHQADkFUU0MgVXNlciBE
YXRhcHQADlVzZXJfZGF0YV9BVFNDcQB+AC5zcQB+ACpwcQB+AG5wdAADU0NDcQB+AC5zcQB+ACpw
dAAZQW5jaWxsYXJ5IERhdGEgTGVnYWN5IDYwOHB0ABlBbmNpbGxhcnlfZGF0YV9sZWdhY3lfNjA4
cQB+AC5zcQB+ACpwdAANQ3VlIFBvaW50IFhNTHB0AAtDdWVQb2ludFhNTHEAfgAueHEAfgA1c3EA
fgAWdAAidHJ1ZSBpZiB0aGUgdGltZWNvZGUgaXMgZHJvcCBmcmFtZXBwdAATdGltZWNvZGVfZHJv
cF9mcmFtZXB+cQB+AB50AAdCT09MRUFOc3EAfgAWdABBVHJ1ZSBvbiB0aGUgbGFzdCBkYXRhIHBh
Y2tldCBvZiB0aGUgU3RyZWFtICh3aXRoIG9yIHdpdGhvdXQgZGF0YSlwcHQAC2VuZE9mU3RyZWFt
cHEAfgB7c3EAfgAWdAAYSW5kaWNhdGVzIHRoZSBmcmFtZSByYXRlcHB0ABN0aW1lY29kZV9mcmFt
ZV9yYXRlcH5xAH4AHnQAB0lOVEVHRVJzcQB+ABZ0AB5JbmRpY2F0ZXMgdGhlIDMyLWJpdCB1c2Vy
IGRhdGFwcHQAEnRpbWVjb2RlX3VzZXJfYml0c3BxAH4Ag3NxAH4AFnQALEluZGljYXRlcyBpZiB0
aGUgZnJhbWUgcmF0ZSByZW1haW5zIGNvbnN0YW50cHB0ABNjb25zdGFudF9mcmFtZV9yYXRlcHEA
fgB7c3EAfgAWdAAYSW5kaWNhdGVzIHRoZSBmcmFtZSByYXRlcHB0AApmcmFtZV9yYXRlcH5xAH4A
HnQACFJBVElPTkFMc3EAfgAWcHBwdAAPZGF0YV9pc19taXNzaW5ncHEAfgB7c3EAfgAWdAAjSW5k
aWNhdGVzIHRoZSB0aW1lY29kZSBmaWVsZCBudW1iZXJwcHQADnRpbWVjb2RlX2ZpZWxkc3EAfgAo
AAAAAncEAAAAAnNxAH4AKnB0AAExcHEAfgCXcQB+AC5zcQB+ACpwdAABMnBxAH4AmXEAfgAueHEA
fgCDc3EAfgAWdAARVGhlIHRpbWVjb2RlIHR5cGVwcHQADXRpbWVjb2RlX3R5cGVzcQB+ACgAAAAD
dwQAAAADc3EAfgAqcHQACFRpbWVjb2RlcHQACHRpbWVjb2RlcQB+AC5zcQB+ACpwdAARTG9jYWwg
dGltZSBvZiBkYXlwdAARbG9jYWxfdGltZV9vZl9kYXlxAH4ALnNxAH4AKnB0AA9VVEMgdGltZSBv
ZiBkYXlwdAAPVVRDX3RpbWVfb2ZfZGF5cQB+AC54cQB+ADVzcQB+ABZ0AC1UaGUgbW9zdCByZWxl
dmFudCB0aW1lIHBlcnRhaW5pbmcgdG8gdGhlIGRhdGFwcHQABHRpbWVwcQB+ACBzcQB+ABZ0ACdJ
bmRpY2F0ZXMgaWYgdGhlIHRpbWVjb2RlIGlzIGNvbnRpbnVvdXNwcHQAFnRpbWVjb2RlX2lzX2Nv
bnRpbnVvdXNwcQB+AHtzcQB+ABZ0ACBJbmRpY2F0ZXMgdGhlIGJpbmFyeSBncm91cCBmbGFnc3Bw
dAAbdGltZWNvZGVfYmluYXJ5X2dyb3VwX2ZsYWdzcHEAfgCDc3EAfgAWdABBdHJ1ZSBpZiB0aGUg
dGltZWNvZGUgc3RyZWFtIGNvbnRhaW5zIG9uZSB0aW1lY29kZSB2YWx1ZSBwZXIgZmllbGRwcHQA
F3RpbWVjb2RlX2lzX2ZpZWxkX2Jhc2VkcHEAfgB7c3EAfgAWdABBdHJ1ZSBpZiB0aGUgdGltZSBj
b2RlIGlzIHN5bmNocm9uaXNlZCB3aXRoIGEgY29sb3IgZmllbGQgc2VxdWVuY2VwcHQAGXRpbWVj
b2RlX2NvbG9yX2ZyYW1lX2ZsYWdwcQB+AHt4cHNyABFqYXZhLnV0aWwuSGFzaE1hcAUH2sHDFmDR
AwACRgAKbG9hZEZhY3RvckkACXRocmVzaG9sZHhwPwAAAAAAACB3CAAAAEAAAAAJcQB+AB1zcgAk
Y2EuZGlnaXRhbHJhcGlkcy5rYXlhay50aW1lLlRpbWVJbXBsAAAAAAAAAAECAAJMAAhyYXRpb25h
bHQAMUxjYS9kaWdpdGFscmFwaWRzL2theWFrL2RhdGEvaW1wbC9SYXRpb25hbE51bWJlcjtMAAh0
aW1lQmFzZXQAJkxjYS9kaWdpdGFscmFwaWRzL2theWFrL3RpbWUvVGltZUJhc2U7eHBzcgAvY2Eu
ZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhLmltcGwuUmF0aW9uYWxOdW1iZXIAAAAAAAAAAQIABEoA
C2Rlbm9taW5hdG9yWgAJaXNSZWR1Y2VkWgATbmVlZHNCaWdGb3JNdWx0aXBseUoACW51bWVyYXRv
cnhyABBqYXZhLmxhbmcuTnVtYmVyhqyVHQuU4IsCAAB4cAAAAAAAAHUwAAAAAAAAACd7wXNyAChj
YS5kaWdpdGFscmFwaWRzLmtheWFrLnRpbWUuVGltZUJhc2VJbXBsAAAAAAAAAAECAAFMAA5vZmZz
ZXRSYXRpb25hbHEAfgC5eHBzcQB+ALwAAAAAO5rKAAAAAAAAAAAAAABxAH4AJ3QAAzI0aHEAfgB6
c3EAfgA4AHEAfgCscQB+ADlxAH4AtXEAfgDDcQB+ADpzcgAmamF2YS51dGlsLkNvbGxlY3Rpb25z
JFVubW9kaWZpYWJsZUxpc3T8DyUxteyOEAIAAUwABGxpc3RxAH4AF3hyACxqYXZhLnV0aWwuQ29s
bGVjdGlvbnMkVW5tb2RpZmlhYmxlQ29sbGVjdGlvbhlCAIDLXvceAgABTAABY3QAFkxqYXZhL3V0
aWwvQ29sbGVjdGlvbjt4cHNxAH4AKAAAAAF3BAAAAAF0ABBNWEZfc3lzdGVtX3RyYWNreHEAfgDI
cQB+AIJzcgARamF2YS5sYW5nLkludGVnZXIS4qCk94GHOAIAAUkABXZhbHVleHEAfgC9AAAAHnEA
fgCNc3EAfgC8AAAAAAAAA+kBAAAAAAAAAHUwcQB+AJx0AAh0aW1lY29kZXg=</property>
                        <pinDefinition name="Timecode 6" displayName="Timecode (MXF System Track)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <property name="5_1_to_stereo" isNull="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">626.0,1630.2738037109375</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="assign_missing_speaker_positions">true</property>
                    <property name="attenuation3db">false</property>
                    <property name="bitstreamformat">ADTSMP4</property>
                    <property name="center_gain_db">-3.0</property>
                    <property name="channel_position_preset">L_R</property>
                    <property name="copyright">true</property>
                    <property name="datarate">128000</property>
                    <property name="defaultInputPin">audio</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="dialoguenormalization">-27</property>
                    <property name="downmix_to_mono">false</property>
                    <property name="downmixstyle">Disabled</property>
                    <property name="drclinemode">COMPPROF_FILMSTD</property>
                    <property name="drcrfmode">COMPPROF_FILMSTD</property>
                    <property name="dsurrmode">DSM_NOTINDICATED</property>
                    <property name="encodemode">DP_AAC</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="left_gain_db">0.0</property>
                    <property name="lfe_gain_db">-12.0</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="lorocmix">-3 dB</property>
                    <property name="lorosmix">-3 dB</property>
                    <property name="lowpassfilterlfe">true</property>
                    <property name="ltrtcmix">-3 dB</property>
                    <property name="ltrtsmix">-3 dB</property>
                    <property name="originalbitstream">true</property>
                    <property name="output_bits_per_sample">16</property>
                    <property name="output_sample_rate" isNull="true"/>
                    <property name="phaseshift90">true</property>
                    <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                    <property name="removepce">false</property>
                    <property name="right_gain_db">0.0</property>
                    <property name="signallingmode">SBR_BC</property>
                    <property name="surround_gain_db">-3.0</property>
                    <property name="usePSv2">false</property>
                    <property name="use_metadata">true</property>
                    <componentName>AAC Encoder (Dolby) En128</componentName>
                    <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                    <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                    <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                    <childComponents/>
                    <pin name="audio" type="INPUT_IO"/>
                    <pin name="out" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="AVCMuxMode">avc1</property>
                    <property name="AlternateAudioTracks">true</property>
                    <property name="AlternateSubtitleTracks">true</property>
                    <property name="ChunkDuration">1000</property>
                    <property name="ChunkMode">GOP count or duration</property>
                    <property name="FragmentDuration">3000</property>
                    <property name="Fragmentation">false</property>
                    <property name="NbGopsPerChunk">1</property>
                    <property name="ProgressiveDownload">false</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1561.0,1606.2738037109375</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">mp4</property>
                    <property name="drc_iso_file_format">MPEG4</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">false</property>
                    <property name="metadata" isNull="true"/>
                    <property name="onFormatChange">Update Metadata</property>
                    <componentName>ISO MPEG-4 Multiplexer Eb128</componentName>
                    <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                    <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                    <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                    <childComponents/>
                    <pin name="mp4" type="OUTPUT_IO"/>
                    <pin name="Track 1" type="INPUT_IO">
                        <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Track 2" type="INPUT_IO">
                        <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1997.0,1558.2738037109375</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="exclusiveMode">false</property>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="ROOT_language_code" path="../../language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${ROOT_language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${ROOT_language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output - En128</componentName>
                    <componentDefinitionName>File Output</componentDefinitionName>
                    <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                    <childComponents/>
                    <pin name="write" type="INPUT_IO"/>
                    <pin name="filename" type="INPUT_IO"/>
                    <pin name="writeComplete" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">2501.491943359375,1892.3575134277344</property>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="default_audio" isNull="true"/>
                    <property name="file" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../sourceFileBaseName"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_manifest.ism"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_manifest.ism"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>AMS Manifest Writer</componentName>
                    <componentDefinitionName>AMSManifestWriter</componentDefinitionName>
                    <componentDefinitionGuid>3780304E-D2B1-4AA6-B109-893B4866DE5E</componentDefinitionGuid>
                    <componentOwningPluginName>AMSManifestWriter</componentOwningPluginName>
                    <componentOwningPluginId>com.imaginecommunications.AMSManifestWriter</componentOwningPluginId>
                    <childComponents/>
                    <pin name="writeComplete" type="EVENT"/>
                    <pin name="Asset 1" type="INPUT_PUSH">
                        <pinDefinition name="Asset 1" displayName="Asset 1" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 2" type="INPUT_PUSH">
                        <pinDefinition name="Asset 2" displayName="Asset 2" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 3" type="INPUT_PUSH">
                        <pinDefinition name="Asset 3" displayName="Asset 3" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 4" type="INPUT_PUSH">
                        <pinDefinition name="Asset 4" displayName="Asset 4" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 5" type="INPUT_PUSH">
                        <pinDefinition name="Asset 5" displayName="Asset 5" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 6" type="INPUT_PUSH">
                        <pinDefinition name="Asset 6" displayName="Asset 6" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 7" type="INPUT_PUSH">
                        <pinDefinition name="Asset 7" displayName="Asset 7" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 8" type="INPUT_PUSH">
                        <pinDefinition name="Asset 8" displayName="Asset 8" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 9" type="INPUT_PUSH">
                        <pinDefinition name="Asset 9" displayName="Asset 9" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 10" type="INPUT_PUSH">
                        <pinDefinition name="Asset 10" displayName="Asset 10" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 11" type="INPUT_PUSH">
                        <pinDefinition name="Asset 11" displayName="Asset 11" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 12" type="INPUT_PUSH">
                        <pinDefinition name="Asset 12" displayName="Asset 12" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 13" type="INPUT_PUSH">
                        <pinDefinition name="Asset 13" displayName="Asset 13" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 14" type="INPUT_PUSH">
                        <pinDefinition name="Asset 14" displayName="Asset 14" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 15" type="INPUT_PUSH">
                        <pinDefinition name="Asset 15" displayName="Asset 15" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 16" type="INPUT_PUSH">
                        <pinDefinition name="Asset 16" displayName="Asset 16" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 17" type="INPUT_PUSH">
                        <pinDefinition name="Asset 17" displayName="Asset 17" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 18" type="INPUT_PUSH">
                        <pinDefinition name="Asset 18" displayName="Asset 18" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 19" type="INPUT_PUSH">
                        <pinDefinition name="Asset 19" displayName="Asset 19" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 20" type="INPUT_PUSH">
                        <pinDefinition name="Asset 20" displayName="Asset 20" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 21" type="INPUT_PUSH">
                        <pinDefinition name="Asset 21" displayName="Asset 21" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 22" type="INPUT_PUSH">
                        <pinDefinition name="Asset 22" displayName="Asset 22" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 23" type="INPUT_PUSH">
                        <pinDefinition name="Asset 23" displayName="Asset 23" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 24" type="INPUT_PUSH">
                        <pinDefinition name="Asset 24" displayName="Asset 24" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 25" type="INPUT_PUSH">
                        <pinDefinition name="Asset 25" displayName="Asset 25" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 26" type="INPUT_PUSH">
                        <pinDefinition name="Asset 26" displayName="Asset 26" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 27" type="INPUT_PUSH">
                        <pinDefinition name="Asset 27" displayName="Asset 27" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                    <pin name="Asset 28" type="INPUT_PUSH">
                        <pinDefinition name="Asset 28" displayName="Asset 28" type="INPUT_PUSH" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">343.0,1628.9191284179688</property>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="language_code">en</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="override_language_code">true</property>
                    <componentName>Language</componentName>
                    <componentDefinitionName>Language Code Updater</componentDefinitionName>
                    <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                    <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_PUSH"/>
                    <pin name="out" type="OUTPUT_PUSH"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">715.0,7.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="output.data_layout.layout_type" isNull="true"/>
                    <property name="output.data_layout.package_type">YUYV</property>
                    <property name="output.data_layout.packaged_bit_depth">8</property>
                    <property name="output.data_layout.packaged_scanline_alignment">1</property>
                    <property name="output.data_layout.planar_alpha_channel" isNull="true"/>
                    <property name="output.data_layout.planar_bit_depth">8</property>
                    <property name="output.data_layout.planar_chroma_sampling">422</property>
                    <property name="output.data_layout.planar_scanline_alignment">32</property>
                    <property name="output.raster_orientation" isNull="true"/>
                    <property name="output.video_format.color_space" isNull="true"/>
                    <property name="output.video_format.dar" isNull="true"/>
                    <property name="output.video_format.frame_rate" isNull="true"/>
                    <property name="output.video_format.height" isNull="true"/>
                    <property name="output.video_format.scan_type">Progressive</property>
                    <property name="output.video_format.width" isNull="true"/>
                    <property name="output.video_format.widthheight" isNull="true"/>
                    <property name="preference.threads.bit_depth_down_converter">1</property>
                    <property name="preference.threads.bit_depth_up_converter">1</property>
                    <property name="preference.threads.chroma_resample">1</property>
                    <property name="preference.threads.color_space_convert">1</property>
                    <property name="preference.threads.data_layout_converter">1</property>
                    <property name="preference.threads.de_interlacing">1</property>
                    <property name="preference.threads.progressive_to_interlaced">1</property>
                    <property name="preference.threads.scaling">1</property>
                    <property name="preference.threads.temporal_noise_reduction">1</property>
                    <property name="processing.bit_depth_down_conversion.dithering">FS</property>
                    <property name="processing.bit_depth_down_conversion.mode">truncate</property>
                    <property name="processing.bit_depth_down_conversion.noise">0</property>
                    <property name="processing.chroma_resample.filterType">imagine</property>
                    <property name="processing.chroma_resample.processing_mode">high_speed</property>
                    <property name="processing.colorspace_conversion.conversionMethod">highSpeed</property>
                    <property name="processing.cropping.afd_alternative_center">ignore</property>
                    <property name="processing.cropping.bottom">0</property>
                    <property name="processing.cropping.left">0</property>
                    <property name="processing.cropping.mode">auto</property>
                    <property name="processing.cropping.reserved_afd_behaviour">fail</property>
                    <property name="processing.cropping.right">0</property>
                    <property name="processing.cropping.top">0</property>
                    <property name="processing.de_interlacing.cadenceReEntryMode">0</property>
                    <property name="processing.de_interlacing.detect2224Cadence">true</property>
                    <property name="processing.de_interlacing.detect22Cadence">true</property>
                    <property name="processing.de_interlacing.detect2332Cadence">true</property>
                    <property name="processing.de_interlacing.detect32322Cadence">true</property>
                    <property name="processing.de_interlacing.detect32Cadence">true</property>
                    <property name="processing.de_interlacing.detect55Cadence">true</property>
                    <property name="processing.de_interlacing.detect64Cadence">true</property>
                    <property name="processing.de_interlacing.detect87Cadence">true</property>
                    <property name="processing.de_interlacing.latency">3</property>
                    <property name="processing.de_interlacing.m2LowNoiseModeFactor">1</property>
                    <property name="processing.de_interlacing.noiseTolerance">0</property>
                    <property name="processing.de_interlacing.remainIn2224Cadence">false</property>
                    <property name="processing.de_interlacing.remainIn22Cadence">false</property>
                    <property name="processing.de_interlacing.remainIn2332Cadence">false</property>
                    <property name="processing.de_interlacing.remainIn32322Cadence">false</property>
                    <property name="processing.de_interlacing.remainIn32Cadence">false</property>
                    <property name="processing.de_interlacing.remainIn55Cadence">false</property>
                    <property name="processing.de_interlacing.remainIn64Cadence">false</property>
                    <property name="processing.de_interlacing.remainIn87Cadence">false</property>
                    <property name="processing.pipeline_configuration.mode">performance</property>
                    <property name="processing.progressive_to_interlaced.filterControl">0</property>
                    <property name="processing.progressive_to_interlaced.filterType">0</property>
                    <property name="processing.progressive_to_interlaced.verticalBandwidthControl">1</property>
                    <property name="processing.pulldown.pattern">2_3</property>
                    <property name="processing.scaling.filterControl">0</property>
                    <property name="processing.scaling.filterType">0</property>
                    <property name="processing.temporal_noise_reduction" isNull="true"/>
                    <componentName>Video Format Converter</componentName>
                    <componentDefinitionName>VideoFormatConverter2</componentDefinitionName>
                    <componentDefinitionGuid>044A97C7-A980-433e-83FF-FC15067F627F</componentDefinitionGuid>
                    <componentOwningPluginName>VideoFormatConverter2</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.VideoFormatConverter2</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_PUSH"/>
                    <pin name="out" type="OUTPUT_PUSH"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">415.0,5.0</property>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="color_space_standard">best_guess</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="override_input">FALSE</property>
                    <componentName>Color Space Standard Updater</componentName>
                    <componentDefinitionName>Color Space Standard Updater</componentDefinitionName>
                    <componentDefinitionGuid>28825610-F32A-4b0b-B353-A368AA05B393</componentDefinitionGuid>
                    <componentOwningPluginName>VideoFormatUtilities</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.VideoFormatUtilities</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_PUSH"/>
                    <pin name="out" type="OUTPUT_PUSH"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">320.0,1849.2738037109375</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 1</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">de</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1311.0,14.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">318.0000305175781,1940.273681640625</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 2</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1832.0,1211.0</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="exclusiveMode">false</property>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_640x360_650.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_640x360_650.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output - Video 7</componentName>
                    <componentDefinitionName>File Output</componentDefinitionName>
                    <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                    <childComponents/>
                    <pin name="write" type="INPUT_IO"/>
                    <pin name="filename" type="INPUT_IO"/>
                    <pin name="writeComplete" type="OUTPUT_IO"/>
                </component>
                <component>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1836.0,1396.0</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="exclusiveMode">false</property>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_320x180_400.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_320x180_400.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output - Video 8</componentName>
                    <componentDefinitionName>File Output</componentDefinitionName>
                    <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                    <childComponents/>
                    <pin name="write" type="INPUT_IO"/>
                    <pin name="filename" type="INPUT_IO"/>
                    <pin name="writeComplete" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1066.8123779296875,17.0</property>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="adv.constraint_set_flag_mode">0</property>
                    <property name="adv.custom_frame_rate_timescale">240000</property>
                    <property name="adv.direct_mode">0</property>
                    <property name="adv.enable_custom_frame_rate_timescale">false</property>
                    <property name="adv.intra_precision">2</property>
                    <property name="adv.mbaff_scan_type">1</property>
                    <property name="adv.ps3_mode">0</property>
                    <property name="adv.qt_mode">0</property>
                    <property name="adv.transform_8x8">0</property>
                    <property name="adv.weighted_pred_flag">1</property>
                    <property name="afd.active_format">8</property>
                    <property name="afd.afd_source">NONE</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="gen.enable_3d_encoding">false</property>
                    <property name="gen.level_idc">0</property>
                    <property name="gen.mbaff_mode">false</property>
                    <property name="gen.profile_idc">100</property>
                    <property name="gen.speed">8</property>
                    <property name="gen.sym_mode">1</property>
                    <property name="gop.adaptive_bframes">true</property>
                    <property name="gop.bframes">2</property>
                    <property name="gop.duration">2</property>
                    <property name="gop.idr_control">1</property>
                    <property name="gop.idr_period">1</property>
                    <property name="gop.min_intra_period">4</property>
                    <property name="gop.mode">0</property>
                    <property name="gop.sps_period">0</property>
                    <property name="load_config_file" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="me.max_refs">1</property>
                    <property name="me.search_range">-1</property>
                    <property name="me.subdiv">7</property>
                    <property name="mt.max_pict_tasks">-1</property>
                    <property name="mt.mode">1</property>
                    <property name="mt.num_threads">8</property>
                    <property name="output.bitdepth" isNull="true"/>
                    <property name="output.video_format.color_space" isNull="true"/>
                    <property name="output.video_format.dar" isNull="true"/>
                    <property name="output.video_format.deinterleave_fields" isNull="true"/>
                    <property name="output.video_format.frame_rate" isNull="true"/>
                    <property name="output.video_format.height">1080</property>
                    <property name="output.video_format.scan_type" isNull="true"/>
                    <property name="output.video_format.width">1920</property>
                    <property name="preference.threads.bit_depth_down_converter">1</property>
                    <property name="preference.threads.bit_depth_up_converter">1</property>
                    <property name="preference.threads.chroma_resample">1</property>
                    <property name="preference.threads.color_space_convert">1</property>
                    <property name="preference.threads.data_layout_converter">1</property>
                    <property name="preference.threads.de_interlacing">1</property>
                    <property name="preference.threads.progressive_to_interlaced">1</property>
                    <property name="preference.threads.scaling">1</property>
                    <property name="preference.threads.temporal_noise_reduction">1</property>
                    <property name="rc.auto_qp">true</property>
                    <property name="rc.initial_cpb_removal_delay">-1</property>
                    <property name="rc.kbps">6000</property>
                    <property name="rc.max_inter_frame_bytes">0</property>
                    <property name="rc.max_intra_frame_bytes">0</property>
                    <property name="rc.max_kbps">0</property>
                    <property name="rc.min_intra_frame_bytes">0</property>
                    <property name="rc.qp_delta_b">4</property>
                    <property name="rc.qp_delta_p">2</property>
                    <property name="rc.qp_intra">30</property>
                    <property name="rc.qp_max">51</property>
                    <property name="rc.qp_min">8</property>
                    <property name="rc.scene_detect">35</property>
                    <property name="rc.type">2</property>
                    <property name="rc.unrestricted_iframepicsize">0</property>
                    <property name="rc.vbv_length">1000</property>
                    <property name="save_config_file" isNull="true"/>
                    <property name="save_post_validation_config_file" isNull="true"/>
                    <property name="save_pre_validation_config_file" isNull="true"/>
                    <property name="sei.afd_repetition">0</property>
                    <property name="sei.pic_timing_flag">1</property>
                    <property name="sei.split_sei_payload">0</property>
                    <property name="slice.deblock">1</property>
                    <property name="slice.mode">0</property>
                    <property name="slice.param">1</property>
                    <property name="suppress_validation">false</property>
                    <property name="tc.enable_timecode_frame_rate_above_30fps_retimer">FALSE</property>
                    <property name="tc.input">none</property>
                    <property name="tc.retimer_behavior">Data is Missing</property>
                    <property name="tc.start_timecode">00:00:00:00/30</property>
                    <property name="tc.video_format">Same as Input(1tc/frame)</property>
                    <property name="tc.video_format_retimer" isNull="true"/>
                    <property name="ud.field_dominance">INPUT_PIN</property>
                    <property name="ud.target_format">ATSC53</property>
                    <property name="ud.target_stream">AVC</property>
                    <property name="vui.bitstream_restriction_flag">false</property>
                    <property name="vui.colour_description_present_flag">false</property>
                    <property name="vui.colour_primaries">2</property>
                    <property name="vui.extended_sar" isNull="true"/>
                    <property name="vui.matrix_coeffients">2</property>
                    <property name="vui.max_bytes_per_pic_denom">false</property>
                    <property name="vui.nal_hrd_parameters_present_flag">false</property>
                    <property name="vui.pic_struct_present_flag">true</property>
                    <property name="vui.sar">0</property>
                    <property name="vui.timing_info_present_flag">false</property>
                    <property name="vui.transfer_characteristics">2</property>
                    <property name="vui.vcl_hrd_parameters_present_flag">false</property>
                    <property name="vui.video_format">2</property>
                    <property name="vui.video_signal_type_present_flag">true</property>
                    <property name="xds.adult-language">FALSE</property>
                    <property name="xds.analog-source-bit">0</property>
                    <property name="xds.aps-bit">0</property>
                    <property name="xds.basic_category" isNull="true"/>
                    <property name="xds.basic_category_1" isNull="true"/>
                    <property name="xds.basic_category_2" isNull="true"/>
                    <property name="xds.basic_category_3" isNull="true"/>
                    <property name="xds.basic_category_4" isNull="true"/>
                    <property name="xds.basic_category_5" isNull="true"/>
                    <property name="xds.basic_category_6" isNull="true"/>
                    <property name="xds.cgms-a-bit">0</property>
                    <property name="xds.cgms-a-repetition-rate">2.50</property>
                    <property name="xds.detailed_category" isNull="true"/>
                    <property name="xds.detailed_category_1" isNull="true"/>
                    <property name="xds.detailed_category_10" isNull="true"/>
                    <property name="xds.detailed_category_11" isNull="true"/>
                    <property name="xds.detailed_category_12" isNull="true"/>
                    <property name="xds.detailed_category_13" isNull="true"/>
                    <property name="xds.detailed_category_14" isNull="true"/>
                    <property name="xds.detailed_category_15" isNull="true"/>
                    <property name="xds.detailed_category_16" isNull="true"/>
                    <property name="xds.detailed_category_17" isNull="true"/>
                    <property name="xds.detailed_category_18" isNull="true"/>
                    <property name="xds.detailed_category_19" isNull="true"/>
                    <property name="xds.detailed_category_2" isNull="true"/>
                    <property name="xds.detailed_category_20" isNull="true"/>
                    <property name="xds.detailed_category_21" isNull="true"/>
                    <property name="xds.detailed_category_22" isNull="true"/>
                    <property name="xds.detailed_category_23" isNull="true"/>
                    <property name="xds.detailed_category_24" isNull="true"/>
                    <property name="xds.detailed_category_25" isNull="true"/>
                    <property name="xds.detailed_category_26" isNull="true"/>
                    <property name="xds.detailed_category_27" isNull="true"/>
                    <property name="xds.detailed_category_28" isNull="true"/>
                    <property name="xds.detailed_category_29" isNull="true"/>
                    <property name="xds.detailed_category_3" isNull="true"/>
                    <property name="xds.detailed_category_30" isNull="true"/>
                    <property name="xds.detailed_category_31" isNull="true"/>
                    <property name="xds.detailed_category_4" isNull="true"/>
                    <property name="xds.detailed_category_5" isNull="true"/>
                    <property name="xds.detailed_category_6" isNull="true"/>
                    <property name="xds.detailed_category_7" isNull="true"/>
                    <property name="xds.detailed_category_8" isNull="true"/>
                    <property name="xds.detailed_category_9" isNull="true"/>
                    <property name="xds.enable-xds-removal">FALSE</property>
                    <property name="xds.fantasy-violence">FALSE</property>
                    <property name="xds.insert-cgms-a">FALSE</property>
                    <property name="xds.insert-network-name">FALSE</property>
                    <property name="xds.insert-program-description">FALSE</property>
                    <property name="xds.insert-program-name">FALSE</property>
                    <property name="xds.insert-program-type">FALSE</property>
                    <property name="xds.insert-station-id-native-channel">FALSE</property>
                    <property name="xds.insert-tsid">FALSE</property>
                    <property name="xds.insert-v-chip-info">FALSE</property>
                    <property name="xds.native-channel" isNull="true"/>
                    <property name="xds.network-name" isNull="true"/>
                    <property name="xds.network-name-repetition-rate">2.50</property>
                    <property name="xds.number_of_basic_categories">1</property>
                    <property name="xds.number_of_detailed_categories">0</property>
                    <property name="xds.program-description" isNull="true"/>
                    <property name="xds.program-description-repetition-rate">20.0</property>
                    <property name="xds.program-name" isNull="true"/>
                    <property name="xds.program-name-repetition-rate">2.50</property>
                    <property name="xds.program-type-repetition-rate">20.0</property>
                    <property name="xds.rating" isNull="true"/>
                    <property name="xds.sexual-situations">FALSE</property>
                    <property name="xds.sexually-suggestive-dialog">FALSE</property>
                    <property name="xds.station-id" isNull="true"/>
                    <property name="xds.station-id-native-channel-repetition-rate">2.50</property>
                    <property name="xds.system">0</property>
                    <property name="xds.tsid" isNull="true"/>
                    <property name="xds.tsid-repetition-rate">2.50</property>
                    <property name="xds.v-chip-repetition-rate">2.50</property>
                    <property name="xds.violence">FALSE</property>
                    <componentName>AVC Video Encoder 1920x1080 6000 kbps</componentName>
                    <componentDefinitionName>AVC Video Encoder</componentDefinitionName>
                    <componentDefinitionGuid>43e72a55-1e8f-4827-a6e3-3217b07ba7e9</componentDefinitionGuid>
                    <componentOwningPluginName>DRAVCEncoder</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.DRAVCEncoder</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_IO"/>
                    <pin name="tc" type="INPUT_IO"/>
                    <pin name="cc608" type="INPUT_IO"/>
                    <pin name="cc708" type="INPUT_IO"/>
                    <pin name="afd" type="INPUT_IO"/>
                    <pin name="ud" type="INPUT_IO"/>
                    <pin name="out" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="AVCMuxMode">avc1</property>
                    <property name="AlternateAudioTracks">true</property>
                    <property name="AlternateSubtitleTracks">true</property>
                    <property name="ChunkDuration">1000</property>
                    <property name="ChunkMode">GOP count or duration</property>
                    <property name="FragmentDuration">3000</property>
                    <property name="Fragmentation">false</property>
                    <property name="NbGopsPerChunk">1</property>
                    <property name="ProgressiveDownload">false</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1498.260009765625,17.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">mp4</property>
                    <property name="drc_iso_file_format">MPEG4</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">false</property>
                    <property name="metadata" isNull="true"/>
                    <property name="onFormatChange">Update Metadata</property>
                    <componentName>ISO MPEG-4 Multiplexer 1</componentName>
                    <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                    <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                    <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                    <childComponents/>
                    <pin name="mp4" type="OUTPUT_IO"/>
                    <pin name="Track 1" type="INPUT_IO">
                        <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Track 2" type="INPUT_IO">
                        <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="AVCMuxMode">avc1</property>
                    <property name="AlternateAudioTracks">true</property>
                    <property name="AlternateSubtitleTracks">true</property>
                    <property name="ChunkDuration">1000</property>
                    <property name="ChunkMode">GOP count or duration</property>
                    <property name="FragmentDuration">3000</property>
                    <property name="Fragmentation">false</property>
                    <property name="NbGopsPerChunk">1</property>
                    <property name="ProgressiveDownload">false</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1500.260009765625,229.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">mp4</property>
                    <property name="drc_iso_file_format">MPEG4</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">false</property>
                    <property name="metadata" isNull="true"/>
                    <property name="onFormatChange">Update Metadata</property>
                    <componentName>ISO MPEG-4 Multiplexer 2</componentName>
                    <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                    <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                    <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                    <childComponents/>
                    <pin name="mp4" type="OUTPUT_IO"/>
                    <pin name="Track 1" type="INPUT_IO">
                        <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Track 2" type="INPUT_IO">
                        <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1058.8123779296875,216.0</property>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="adv.constraint_set_flag_mode">0</property>
                    <property name="adv.custom_frame_rate_timescale">240000</property>
                    <property name="adv.direct_mode">0</property>
                    <property name="adv.enable_custom_frame_rate_timescale">false</property>
                    <property name="adv.intra_precision">2</property>
                    <property name="adv.mbaff_scan_type">1</property>
                    <property name="adv.ps3_mode">0</property>
                    <property name="adv.qt_mode">0</property>
                    <property name="adv.transform_8x8">0</property>
                    <property name="adv.weighted_pred_flag">1</property>
                    <property name="afd.active_format">8</property>
                    <property name="afd.afd_source">NONE</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="gen.enable_3d_encoding">false</property>
                    <property name="gen.level_idc">0</property>
                    <property name="gen.mbaff_mode">false</property>
                    <property name="gen.profile_idc">100</property>
                    <property name="gen.speed">8</property>
                    <property name="gen.sym_mode">1</property>
                    <property name="gop.adaptive_bframes">true</property>
                    <property name="gop.bframes">2</property>
                    <property name="gop.duration">2</property>
                    <property name="gop.idr_control">1</property>
                    <property name="gop.idr_period">1</property>
                    <property name="gop.min_intra_period">4</property>
                    <property name="gop.mode">0</property>
                    <property name="gop.sps_period">0</property>
                    <property name="load_config_file" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="me.max_refs">1</property>
                    <property name="me.search_range">-1</property>
                    <property name="me.subdiv">7</property>
                    <property name="mt.max_pict_tasks">-1</property>
                    <property name="mt.mode">1</property>
                    <property name="mt.num_threads">8</property>
                    <property name="output.bitdepth" isNull="true"/>
                    <property name="output.video_format.color_space" isNull="true"/>
                    <property name="output.video_format.dar" isNull="true"/>
                    <property name="output.video_format.deinterleave_fields" isNull="true"/>
                    <property name="output.video_format.frame_rate" isNull="true"/>
                    <property name="output.video_format.height">1080</property>
                    <property name="output.video_format.scan_type" isNull="true"/>
                    <property name="output.video_format.width">1920</property>
                    <property name="preference.threads.bit_depth_down_converter">1</property>
                    <property name="preference.threads.bit_depth_up_converter">1</property>
                    <property name="preference.threads.chroma_resample">1</property>
                    <property name="preference.threads.color_space_convert">1</property>
                    <property name="preference.threads.data_layout_converter">1</property>
                    <property name="preference.threads.de_interlacing">1</property>
                    <property name="preference.threads.progressive_to_interlaced">1</property>
                    <property name="preference.threads.scaling">1</property>
                    <property name="preference.threads.temporal_noise_reduction">1</property>
                    <property name="rc.auto_qp">true</property>
                    <property name="rc.initial_cpb_removal_delay">-1</property>
                    <property name="rc.kbps">4700</property>
                    <property name="rc.max_inter_frame_bytes">0</property>
                    <property name="rc.max_intra_frame_bytes">0</property>
                    <property name="rc.max_kbps">0</property>
                    <property name="rc.min_intra_frame_bytes">0</property>
                    <property name="rc.qp_delta_b">4</property>
                    <property name="rc.qp_delta_p">2</property>
                    <property name="rc.qp_intra">30</property>
                    <property name="rc.qp_max">51</property>
                    <property name="rc.qp_min">8</property>
                    <property name="rc.scene_detect">35</property>
                    <property name="rc.type">2</property>
                    <property name="rc.unrestricted_iframepicsize">0</property>
                    <property name="rc.vbv_length">1000</property>
                    <property name="save_config_file" isNull="true"/>
                    <property name="save_post_validation_config_file" isNull="true"/>
                    <property name="save_pre_validation_config_file" isNull="true"/>
                    <property name="sei.afd_repetition">0</property>
                    <property name="sei.pic_timing_flag">1</property>
                    <property name="sei.split_sei_payload">0</property>
                    <property name="slice.deblock">1</property>
                    <property name="slice.mode">0</property>
                    <property name="slice.param">1</property>
                    <property name="suppress_validation">false</property>
                    <property name="tc.enable_timecode_frame_rate_above_30fps_retimer">FALSE</property>
                    <property name="tc.input">none</property>
                    <property name="tc.retimer_behavior">Data is Missing</property>
                    <property name="tc.start_timecode">00:00:00:00/30</property>
                    <property name="tc.video_format">Same as Input(1tc/frame)</property>
                    <property name="tc.video_format_retimer" isNull="true"/>
                    <property name="ud.field_dominance">INPUT_PIN</property>
                    <property name="ud.target_format">ATSC53</property>
                    <property name="ud.target_stream">AVC</property>
                    <property name="vui.bitstream_restriction_flag">false</property>
                    <property name="vui.colour_description_present_flag">false</property>
                    <property name="vui.colour_primaries">2</property>
                    <property name="vui.extended_sar" isNull="true"/>
                    <property name="vui.matrix_coeffients">2</property>
                    <property name="vui.max_bytes_per_pic_denom">false</property>
                    <property name="vui.nal_hrd_parameters_present_flag">false</property>
                    <property name="vui.pic_struct_present_flag">true</property>
                    <property name="vui.sar">0</property>
                    <property name="vui.timing_info_present_flag">false</property>
                    <property name="vui.transfer_characteristics">2</property>
                    <property name="vui.vcl_hrd_parameters_present_flag">false</property>
                    <property name="vui.video_format">2</property>
                    <property name="vui.video_signal_type_present_flag">true</property>
                    <property name="xds.adult-language">FALSE</property>
                    <property name="xds.analog-source-bit">0</property>
                    <property name="xds.aps-bit">0</property>
                    <property name="xds.basic_category" isNull="true"/>
                    <property name="xds.basic_category_1" isNull="true"/>
                    <property name="xds.basic_category_2" isNull="true"/>
                    <property name="xds.basic_category_3" isNull="true"/>
                    <property name="xds.basic_category_4" isNull="true"/>
                    <property name="xds.basic_category_5" isNull="true"/>
                    <property name="xds.basic_category_6" isNull="true"/>
                    <property name="xds.cgms-a-bit">0</property>
                    <property name="xds.cgms-a-repetition-rate">2.50</property>
                    <property name="xds.detailed_category" isNull="true"/>
                    <property name="xds.detailed_category_1" isNull="true"/>
                    <property name="xds.detailed_category_10" isNull="true"/>
                    <property name="xds.detailed_category_11" isNull="true"/>
                    <property name="xds.detailed_category_12" isNull="true"/>
                    <property name="xds.detailed_category_13" isNull="true"/>
                    <property name="xds.detailed_category_14" isNull="true"/>
                    <property name="xds.detailed_category_15" isNull="true"/>
                    <property name="xds.detailed_category_16" isNull="true"/>
                    <property name="xds.detailed_category_17" isNull="true"/>
                    <property name="xds.detailed_category_18" isNull="true"/>
                    <property name="xds.detailed_category_19" isNull="true"/>
                    <property name="xds.detailed_category_2" isNull="true"/>
                    <property name="xds.detailed_category_20" isNull="true"/>
                    <property name="xds.detailed_category_21" isNull="true"/>
                    <property name="xds.detailed_category_22" isNull="true"/>
                    <property name="xds.detailed_category_23" isNull="true"/>
                    <property name="xds.detailed_category_24" isNull="true"/>
                    <property name="xds.detailed_category_25" isNull="true"/>
                    <property name="xds.detailed_category_26" isNull="true"/>
                    <property name="xds.detailed_category_27" isNull="true"/>
                    <property name="xds.detailed_category_28" isNull="true"/>
                    <property name="xds.detailed_category_29" isNull="true"/>
                    <property name="xds.detailed_category_3" isNull="true"/>
                    <property name="xds.detailed_category_30" isNull="true"/>
                    <property name="xds.detailed_category_31" isNull="true"/>
                    <property name="xds.detailed_category_4" isNull="true"/>
                    <property name="xds.detailed_category_5" isNull="true"/>
                    <property name="xds.detailed_category_6" isNull="true"/>
                    <property name="xds.detailed_category_7" isNull="true"/>
                    <property name="xds.detailed_category_8" isNull="true"/>
                    <property name="xds.detailed_category_9" isNull="true"/>
                    <property name="xds.enable-xds-removal">FALSE</property>
                    <property name="xds.fantasy-violence">FALSE</property>
                    <property name="xds.insert-cgms-a">FALSE</property>
                    <property name="xds.insert-network-name">FALSE</property>
                    <property name="xds.insert-program-description">FALSE</property>
                    <property name="xds.insert-program-name">FALSE</property>
                    <property name="xds.insert-program-type">FALSE</property>
                    <property name="xds.insert-station-id-native-channel">FALSE</property>
                    <property name="xds.insert-tsid">FALSE</property>
                    <property name="xds.insert-v-chip-info">FALSE</property>
                    <property name="xds.native-channel" isNull="true"/>
                    <property name="xds.network-name" isNull="true"/>
                    <property name="xds.network-name-repetition-rate">2.50</property>
                    <property name="xds.number_of_basic_categories">1</property>
                    <property name="xds.number_of_detailed_categories">0</property>
                    <property name="xds.program-description" isNull="true"/>
                    <property name="xds.program-description-repetition-rate">20.0</property>
                    <property name="xds.program-name" isNull="true"/>
                    <property name="xds.program-name-repetition-rate">2.50</property>
                    <property name="xds.program-type-repetition-rate">20.0</property>
                    <property name="xds.rating" isNull="true"/>
                    <property name="xds.sexual-situations">FALSE</property>
                    <property name="xds.sexually-suggestive-dialog">FALSE</property>
                    <property name="xds.station-id" isNull="true"/>
                    <property name="xds.station-id-native-channel-repetition-rate">2.50</property>
                    <property name="xds.system">0</property>
                    <property name="xds.tsid" isNull="true"/>
                    <property name="xds.tsid-repetition-rate">2.50</property>
                    <property name="xds.v-chip-repetition-rate">2.50</property>
                    <property name="xds.violence">FALSE</property>
                    <componentName>AVC Video Encoder 1920x1080 4700 kbps</componentName>
                    <componentDefinitionName>AVC Video Encoder</componentDefinitionName>
                    <componentDefinitionGuid>43e72a55-1e8f-4827-a6e3-3217b07ba7e9</componentDefinitionGuid>
                    <componentOwningPluginName>DRAVCEncoder</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.DRAVCEncoder</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_IO"/>
                    <pin name="tc" type="INPUT_IO"/>
                    <pin name="cc608" type="INPUT_IO"/>
                    <pin name="cc708" type="INPUT_IO"/>
                    <pin name="afd" type="INPUT_IO"/>
                    <pin name="ud" type="INPUT_IO"/>
                    <pin name="out" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="AVCMuxMode">avc1</property>
                    <property name="AlternateAudioTracks">true</property>
                    <property name="AlternateSubtitleTracks">true</property>
                    <property name="ChunkDuration">1000</property>
                    <property name="ChunkMode">GOP count or duration</property>
                    <property name="FragmentDuration">3000</property>
                    <property name="Fragmentation">false</property>
                    <property name="NbGopsPerChunk">1</property>
                    <property name="ProgressiveDownload">false</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1486.260009765625,452.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">mp4</property>
                    <property name="drc_iso_file_format">MPEG4</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">false</property>
                    <property name="metadata" isNull="true"/>
                    <property name="onFormatChange">Update Metadata</property>
                    <componentName>ISO MPEG-4 Multiplexer 3</componentName>
                    <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                    <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                    <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                    <childComponents/>
                    <pin name="mp4" type="OUTPUT_IO"/>
                    <pin name="Track 1" type="INPUT_IO">
                        <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Track 2" type="INPUT_IO">
                        <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1050.8123779296875,418.0</property>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="adv.constraint_set_flag_mode">0</property>
                    <property name="adv.custom_frame_rate_timescale">240000</property>
                    <property name="adv.direct_mode">0</property>
                    <property name="adv.enable_custom_frame_rate_timescale">false</property>
                    <property name="adv.intra_precision">2</property>
                    <property name="adv.mbaff_scan_type">1</property>
                    <property name="adv.ps3_mode">0</property>
                    <property name="adv.qt_mode">0</property>
                    <property name="adv.transform_8x8">0</property>
                    <property name="adv.weighted_pred_flag">1</property>
                    <property name="afd.active_format">8</property>
                    <property name="afd.afd_source">NONE</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="gen.enable_3d_encoding">false</property>
                    <property name="gen.level_idc">0</property>
                    <property name="gen.mbaff_mode">false</property>
                    <property name="gen.profile_idc">100</property>
                    <property name="gen.speed">8</property>
                    <property name="gen.sym_mode">1</property>
                    <property name="gop.adaptive_bframes">true</property>
                    <property name="gop.bframes">2</property>
                    <property name="gop.duration">2</property>
                    <property name="gop.idr_control">1</property>
                    <property name="gop.idr_period">1</property>
                    <property name="gop.min_intra_period">4</property>
                    <property name="gop.mode">0</property>
                    <property name="gop.sps_period">0</property>
                    <property name="load_config_file" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="me.max_refs">1</property>
                    <property name="me.search_range">-1</property>
                    <property name="me.subdiv">7</property>
                    <property name="mt.max_pict_tasks">-1</property>
                    <property name="mt.mode">1</property>
                    <property name="mt.num_threads">8</property>
                    <property name="output.bitdepth" isNull="true"/>
                    <property name="output.video_format.color_space" isNull="true"/>
                    <property name="output.video_format.dar" isNull="true"/>
                    <property name="output.video_format.deinterleave_fields" isNull="true"/>
                    <property name="output.video_format.frame_rate" isNull="true"/>
                    <property name="output.video_format.height">720</property>
                    <property name="output.video_format.scan_type" isNull="true"/>
                    <property name="output.video_format.width">1280</property>
                    <property name="preference.threads.bit_depth_down_converter">1</property>
                    <property name="preference.threads.bit_depth_up_converter">1</property>
                    <property name="preference.threads.chroma_resample">1</property>
                    <property name="preference.threads.color_space_convert">1</property>
                    <property name="preference.threads.data_layout_converter">1</property>
                    <property name="preference.threads.de_interlacing">1</property>
                    <property name="preference.threads.progressive_to_interlaced">1</property>
                    <property name="preference.threads.scaling">1</property>
                    <property name="preference.threads.temporal_noise_reduction">1</property>
                    <property name="rc.auto_qp">true</property>
                    <property name="rc.initial_cpb_removal_delay">-1</property>
                    <property name="rc.kbps">3400</property>
                    <property name="rc.max_inter_frame_bytes">0</property>
                    <property name="rc.max_intra_frame_bytes">0</property>
                    <property name="rc.max_kbps">0</property>
                    <property name="rc.min_intra_frame_bytes">0</property>
                    <property name="rc.qp_delta_b">4</property>
                    <property name="rc.qp_delta_p">2</property>
                    <property name="rc.qp_intra">30</property>
                    <property name="rc.qp_max">51</property>
                    <property name="rc.qp_min">8</property>
                    <property name="rc.scene_detect">35</property>
                    <property name="rc.type">2</property>
                    <property name="rc.unrestricted_iframepicsize">0</property>
                    <property name="rc.vbv_length">1000</property>
                    <property name="save_config_file" isNull="true"/>
                    <property name="save_post_validation_config_file" isNull="true"/>
                    <property name="save_pre_validation_config_file" isNull="true"/>
                    <property name="sei.afd_repetition">0</property>
                    <property name="sei.pic_timing_flag">1</property>
                    <property name="sei.split_sei_payload">0</property>
                    <property name="slice.deblock">1</property>
                    <property name="slice.mode">0</property>
                    <property name="slice.param">1</property>
                    <property name="suppress_validation">false</property>
                    <property name="tc.enable_timecode_frame_rate_above_30fps_retimer">FALSE</property>
                    <property name="tc.input">none</property>
                    <property name="tc.retimer_behavior">Data is Missing</property>
                    <property name="tc.start_timecode">00:00:00:00/30</property>
                    <property name="tc.video_format">Same as Input(1tc/frame)</property>
                    <property name="tc.video_format_retimer" isNull="true"/>
                    <property name="ud.field_dominance">INPUT_PIN</property>
                    <property name="ud.target_format">ATSC53</property>
                    <property name="ud.target_stream">AVC</property>
                    <property name="vui.bitstream_restriction_flag">false</property>
                    <property name="vui.colour_description_present_flag">false</property>
                    <property name="vui.colour_primaries">2</property>
                    <property name="vui.extended_sar" isNull="true"/>
                    <property name="vui.matrix_coeffients">2</property>
                    <property name="vui.max_bytes_per_pic_denom">false</property>
                    <property name="vui.nal_hrd_parameters_present_flag">false</property>
                    <property name="vui.pic_struct_present_flag">true</property>
                    <property name="vui.sar">0</property>
                    <property name="vui.timing_info_present_flag">false</property>
                    <property name="vui.transfer_characteristics">2</property>
                    <property name="vui.vcl_hrd_parameters_present_flag">false</property>
                    <property name="vui.video_format">2</property>
                    <property name="vui.video_signal_type_present_flag">true</property>
                    <property name="xds.adult-language">FALSE</property>
                    <property name="xds.analog-source-bit">0</property>
                    <property name="xds.aps-bit">0</property>
                    <property name="xds.basic_category" isNull="true"/>
                    <property name="xds.basic_category_1" isNull="true"/>
                    <property name="xds.basic_category_2" isNull="true"/>
                    <property name="xds.basic_category_3" isNull="true"/>
                    <property name="xds.basic_category_4" isNull="true"/>
                    <property name="xds.basic_category_5" isNull="true"/>
                    <property name="xds.basic_category_6" isNull="true"/>
                    <property name="xds.cgms-a-bit">0</property>
                    <property name="xds.cgms-a-repetition-rate">2.50</property>
                    <property name="xds.detailed_category" isNull="true"/>
                    <property name="xds.detailed_category_1" isNull="true"/>
                    <property name="xds.detailed_category_10" isNull="true"/>
                    <property name="xds.detailed_category_11" isNull="true"/>
                    <property name="xds.detailed_category_12" isNull="true"/>
                    <property name="xds.detailed_category_13" isNull="true"/>
                    <property name="xds.detailed_category_14" isNull="true"/>
                    <property name="xds.detailed_category_15" isNull="true"/>
                    <property name="xds.detailed_category_16" isNull="true"/>
                    <property name="xds.detailed_category_17" isNull="true"/>
                    <property name="xds.detailed_category_18" isNull="true"/>
                    <property name="xds.detailed_category_19" isNull="true"/>
                    <property name="xds.detailed_category_2" isNull="true"/>
                    <property name="xds.detailed_category_20" isNull="true"/>
                    <property name="xds.detailed_category_21" isNull="true"/>
                    <property name="xds.detailed_category_22" isNull="true"/>
                    <property name="xds.detailed_category_23" isNull="true"/>
                    <property name="xds.detailed_category_24" isNull="true"/>
                    <property name="xds.detailed_category_25" isNull="true"/>
                    <property name="xds.detailed_category_26" isNull="true"/>
                    <property name="xds.detailed_category_27" isNull="true"/>
                    <property name="xds.detailed_category_28" isNull="true"/>
                    <property name="xds.detailed_category_29" isNull="true"/>
                    <property name="xds.detailed_category_3" isNull="true"/>
                    <property name="xds.detailed_category_30" isNull="true"/>
                    <property name="xds.detailed_category_31" isNull="true"/>
                    <property name="xds.detailed_category_4" isNull="true"/>
                    <property name="xds.detailed_category_5" isNull="true"/>
                    <property name="xds.detailed_category_6" isNull="true"/>
                    <property name="xds.detailed_category_7" isNull="true"/>
                    <property name="xds.detailed_category_8" isNull="true"/>
                    <property name="xds.detailed_category_9" isNull="true"/>
                    <property name="xds.enable-xds-removal">FALSE</property>
                    <property name="xds.fantasy-violence">FALSE</property>
                    <property name="xds.insert-cgms-a">FALSE</property>
                    <property name="xds.insert-network-name">FALSE</property>
                    <property name="xds.insert-program-description">FALSE</property>
                    <property name="xds.insert-program-name">FALSE</property>
                    <property name="xds.insert-program-type">FALSE</property>
                    <property name="xds.insert-station-id-native-channel">FALSE</property>
                    <property name="xds.insert-tsid">FALSE</property>
                    <property name="xds.insert-v-chip-info">FALSE</property>
                    <property name="xds.native-channel" isNull="true"/>
                    <property name="xds.network-name" isNull="true"/>
                    <property name="xds.network-name-repetition-rate">2.50</property>
                    <property name="xds.number_of_basic_categories">1</property>
                    <property name="xds.number_of_detailed_categories">0</property>
                    <property name="xds.program-description" isNull="true"/>
                    <property name="xds.program-description-repetition-rate">20.0</property>
                    <property name="xds.program-name" isNull="true"/>
                    <property name="xds.program-name-repetition-rate">2.50</property>
                    <property name="xds.program-type-repetition-rate">20.0</property>
                    <property name="xds.rating" isNull="true"/>
                    <property name="xds.sexual-situations">FALSE</property>
                    <property name="xds.sexually-suggestive-dialog">FALSE</property>
                    <property name="xds.station-id" isNull="true"/>
                    <property name="xds.station-id-native-channel-repetition-rate">2.50</property>
                    <property name="xds.system">0</property>
                    <property name="xds.tsid" isNull="true"/>
                    <property name="xds.tsid-repetition-rate">2.50</property>
                    <property name="xds.v-chip-repetition-rate">2.50</property>
                    <property name="xds.violence">FALSE</property>
                    <componentName>AVC Video Encoder 1280x720 3400 kbps</componentName>
                    <componentDefinitionName>AVC Video Encoder</componentDefinitionName>
                    <componentDefinitionGuid>43e72a55-1e8f-4827-a6e3-3217b07ba7e9</componentDefinitionGuid>
                    <componentOwningPluginName>DRAVCEncoder</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.DRAVCEncoder</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_IO"/>
                    <pin name="tc" type="INPUT_IO"/>
                    <pin name="cc608" type="INPUT_IO"/>
                    <pin name="cc708" type="INPUT_IO"/>
                    <pin name="afd" type="INPUT_IO"/>
                    <pin name="ud" type="INPUT_IO"/>
                    <pin name="out" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="AVCMuxMode">avc1</property>
                    <property name="AlternateAudioTracks">true</property>
                    <property name="AlternateSubtitleTracks">true</property>
                    <property name="ChunkDuration">1000</property>
                    <property name="ChunkMode">GOP count or duration</property>
                    <property name="FragmentDuration">3000</property>
                    <property name="Fragmentation">false</property>
                    <property name="NbGopsPerChunk">1</property>
                    <property name="ProgressiveDownload">false</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1497.260009765625,635.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">mp4</property>
                    <property name="drc_iso_file_format">MPEG4</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">false</property>
                    <property name="metadata" isNull="true"/>
                    <property name="onFormatChange">Update Metadata</property>
                    <componentName>ISO MPEG-4 Multiplexer 4</componentName>
                    <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                    <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                    <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                    <childComponents/>
                    <pin name="mp4" type="OUTPUT_IO"/>
                    <pin name="Track 1" type="INPUT_IO">
                        <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Track 2" type="INPUT_IO">
                        <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1050.8123779296875,623.9999694824219</property>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="adv.constraint_set_flag_mode">0</property>
                    <property name="adv.custom_frame_rate_timescale">240000</property>
                    <property name="adv.direct_mode">0</property>
                    <property name="adv.enable_custom_frame_rate_timescale">false</property>
                    <property name="adv.intra_precision">2</property>
                    <property name="adv.mbaff_scan_type">1</property>
                    <property name="adv.ps3_mode">0</property>
                    <property name="adv.qt_mode">0</property>
                    <property name="adv.transform_8x8">0</property>
                    <property name="adv.weighted_pred_flag">1</property>
                    <property name="afd.active_format">8</property>
                    <property name="afd.afd_source">NONE</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="gen.enable_3d_encoding">false</property>
                    <property name="gen.level_idc">0</property>
                    <property name="gen.mbaff_mode">false</property>
                    <property name="gen.profile_idc">77</property>
                    <property name="gen.speed">8</property>
                    <property name="gen.sym_mode">1</property>
                    <property name="gop.adaptive_bframes">true</property>
                    <property name="gop.bframes">2</property>
                    <property name="gop.duration">2</property>
                    <property name="gop.idr_control">1</property>
                    <property name="gop.idr_period">1</property>
                    <property name="gop.min_intra_period">4</property>
                    <property name="gop.mode">0</property>
                    <property name="gop.sps_period">0</property>
                    <property name="load_config_file" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="me.max_refs">1</property>
                    <property name="me.search_range">-1</property>
                    <property name="me.subdiv">7</property>
                    <property name="mt.max_pict_tasks">-1</property>
                    <property name="mt.mode">1</property>
                    <property name="mt.num_threads">8</property>
                    <property name="output.bitdepth" isNull="true"/>
                    <property name="output.video_format.color_space" isNull="true"/>
                    <property name="output.video_format.dar" isNull="true"/>
                    <property name="output.video_format.deinterleave_fields" isNull="true"/>
                    <property name="output.video_format.frame_rate" isNull="true"/>
                    <property name="output.video_format.height">540</property>
                    <property name="output.video_format.scan_type" isNull="true"/>
                    <property name="output.video_format.width">960</property>
                    <property name="preference.threads.bit_depth_down_converter">1</property>
                    <property name="preference.threads.bit_depth_up_converter">1</property>
                    <property name="preference.threads.chroma_resample">1</property>
                    <property name="preference.threads.color_space_convert">1</property>
                    <property name="preference.threads.data_layout_converter">1</property>
                    <property name="preference.threads.de_interlacing">1</property>
                    <property name="preference.threads.progressive_to_interlaced">1</property>
                    <property name="preference.threads.scaling">1</property>
                    <property name="preference.threads.temporal_noise_reduction">1</property>
                    <property name="rc.auto_qp">true</property>
                    <property name="rc.initial_cpb_removal_delay">-1</property>
                    <property name="rc.kbps">2250</property>
                    <property name="rc.max_inter_frame_bytes">0</property>
                    <property name="rc.max_intra_frame_bytes">0</property>
                    <property name="rc.max_kbps">0</property>
                    <property name="rc.min_intra_frame_bytes">0</property>
                    <property name="rc.qp_delta_b">4</property>
                    <property name="rc.qp_delta_p">2</property>
                    <property name="rc.qp_intra">30</property>
                    <property name="rc.qp_max">51</property>
                    <property name="rc.qp_min">8</property>
                    <property name="rc.scene_detect">35</property>
                    <property name="rc.type">2</property>
                    <property name="rc.unrestricted_iframepicsize">0</property>
                    <property name="rc.vbv_length">1000</property>
                    <property name="save_config_file" isNull="true"/>
                    <property name="save_post_validation_config_file" isNull="true"/>
                    <property name="save_pre_validation_config_file" isNull="true"/>
                    <property name="sei.afd_repetition">0</property>
                    <property name="sei.pic_timing_flag">1</property>
                    <property name="sei.split_sei_payload">0</property>
                    <property name="slice.deblock">1</property>
                    <property name="slice.mode">0</property>
                    <property name="slice.param">1</property>
                    <property name="suppress_validation">false</property>
                    <property name="tc.enable_timecode_frame_rate_above_30fps_retimer">FALSE</property>
                    <property name="tc.input">none</property>
                    <property name="tc.retimer_behavior">Data is Missing</property>
                    <property name="tc.start_timecode">00:00:00:00/30</property>
                    <property name="tc.video_format">Same as Input(1tc/frame)</property>
                    <property name="tc.video_format_retimer" isNull="true"/>
                    <property name="ud.field_dominance">INPUT_PIN</property>
                    <property name="ud.target_format">ATSC53</property>
                    <property name="ud.target_stream">AVC</property>
                    <property name="vui.bitstream_restriction_flag">false</property>
                    <property name="vui.colour_description_present_flag">false</property>
                    <property name="vui.colour_primaries">2</property>
                    <property name="vui.extended_sar" isNull="true"/>
                    <property name="vui.matrix_coeffients">2</property>
                    <property name="vui.max_bytes_per_pic_denom">false</property>
                    <property name="vui.nal_hrd_parameters_present_flag">false</property>
                    <property name="vui.pic_struct_present_flag">true</property>
                    <property name="vui.sar">0</property>
                    <property name="vui.timing_info_present_flag">false</property>
                    <property name="vui.transfer_characteristics">2</property>
                    <property name="vui.vcl_hrd_parameters_present_flag">false</property>
                    <property name="vui.video_format">2</property>
                    <property name="vui.video_signal_type_present_flag">true</property>
                    <property name="xds.adult-language">FALSE</property>
                    <property name="xds.analog-source-bit">0</property>
                    <property name="xds.aps-bit">0</property>
                    <property name="xds.basic_category" isNull="true"/>
                    <property name="xds.basic_category_1" isNull="true"/>
                    <property name="xds.basic_category_2" isNull="true"/>
                    <property name="xds.basic_category_3" isNull="true"/>
                    <property name="xds.basic_category_4" isNull="true"/>
                    <property name="xds.basic_category_5" isNull="true"/>
                    <property name="xds.basic_category_6" isNull="true"/>
                    <property name="xds.cgms-a-bit">0</property>
                    <property name="xds.cgms-a-repetition-rate">2.50</property>
                    <property name="xds.detailed_category" isNull="true"/>
                    <property name="xds.detailed_category_1" isNull="true"/>
                    <property name="xds.detailed_category_10" isNull="true"/>
                    <property name="xds.detailed_category_11" isNull="true"/>
                    <property name="xds.detailed_category_12" isNull="true"/>
                    <property name="xds.detailed_category_13" isNull="true"/>
                    <property name="xds.detailed_category_14" isNull="true"/>
                    <property name="xds.detailed_category_15" isNull="true"/>
                    <property name="xds.detailed_category_16" isNull="true"/>
                    <property name="xds.detailed_category_17" isNull="true"/>
                    <property name="xds.detailed_category_18" isNull="true"/>
                    <property name="xds.detailed_category_19" isNull="true"/>
                    <property name="xds.detailed_category_2" isNull="true"/>
                    <property name="xds.detailed_category_20" isNull="true"/>
                    <property name="xds.detailed_category_21" isNull="true"/>
                    <property name="xds.detailed_category_22" isNull="true"/>
                    <property name="xds.detailed_category_23" isNull="true"/>
                    <property name="xds.detailed_category_24" isNull="true"/>
                    <property name="xds.detailed_category_25" isNull="true"/>
                    <property name="xds.detailed_category_26" isNull="true"/>
                    <property name="xds.detailed_category_27" isNull="true"/>
                    <property name="xds.detailed_category_28" isNull="true"/>
                    <property name="xds.detailed_category_29" isNull="true"/>
                    <property name="xds.detailed_category_3" isNull="true"/>
                    <property name="xds.detailed_category_30" isNull="true"/>
                    <property name="xds.detailed_category_31" isNull="true"/>
                    <property name="xds.detailed_category_4" isNull="true"/>
                    <property name="xds.detailed_category_5" isNull="true"/>
                    <property name="xds.detailed_category_6" isNull="true"/>
                    <property name="xds.detailed_category_7" isNull="true"/>
                    <property name="xds.detailed_category_8" isNull="true"/>
                    <property name="xds.detailed_category_9" isNull="true"/>
                    <property name="xds.enable-xds-removal">FALSE</property>
                    <property name="xds.fantasy-violence">FALSE</property>
                    <property name="xds.insert-cgms-a">FALSE</property>
                    <property name="xds.insert-network-name">FALSE</property>
                    <property name="xds.insert-program-description">FALSE</property>
                    <property name="xds.insert-program-name">FALSE</property>
                    <property name="xds.insert-program-type">FALSE</property>
                    <property name="xds.insert-station-id-native-channel">FALSE</property>
                    <property name="xds.insert-tsid">FALSE</property>
                    <property name="xds.insert-v-chip-info">FALSE</property>
                    <property name="xds.native-channel" isNull="true"/>
                    <property name="xds.network-name" isNull="true"/>
                    <property name="xds.network-name-repetition-rate">2.50</property>
                    <property name="xds.number_of_basic_categories">1</property>
                    <property name="xds.number_of_detailed_categories">0</property>
                    <property name="xds.program-description" isNull="true"/>
                    <property name="xds.program-description-repetition-rate">20.0</property>
                    <property name="xds.program-name" isNull="true"/>
                    <property name="xds.program-name-repetition-rate">2.50</property>
                    <property name="xds.program-type-repetition-rate">20.0</property>
                    <property name="xds.rating" isNull="true"/>
                    <property name="xds.sexual-situations">FALSE</property>
                    <property name="xds.sexually-suggestive-dialog">FALSE</property>
                    <property name="xds.station-id" isNull="true"/>
                    <property name="xds.station-id-native-channel-repetition-rate">2.50</property>
                    <property name="xds.system">0</property>
                    <property name="xds.tsid" isNull="true"/>
                    <property name="xds.tsid-repetition-rate">2.50</property>
                    <property name="xds.v-chip-repetition-rate">2.50</property>
                    <property name="xds.violence">FALSE</property>
                    <componentName>AVC Video Encoder 960x540 2250 kbps</componentName>
                    <componentDefinitionName>AVC Video Encoder</componentDefinitionName>
                    <componentDefinitionGuid>43e72a55-1e8f-4827-a6e3-3217b07ba7e9</componentDefinitionGuid>
                    <componentOwningPluginName>DRAVCEncoder</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.DRAVCEncoder</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_IO"/>
                    <pin name="tc" type="INPUT_IO"/>
                    <pin name="cc608" type="INPUT_IO"/>
                    <pin name="cc708" type="INPUT_IO"/>
                    <pin name="afd" type="INPUT_IO"/>
                    <pin name="ud" type="INPUT_IO"/>
                    <pin name="out" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="AVCMuxMode">avc1</property>
                    <property name="AlternateAudioTracks">true</property>
                    <property name="AlternateSubtitleTracks">true</property>
                    <property name="ChunkDuration">1000</property>
                    <property name="ChunkMode">GOP count or duration</property>
                    <property name="FragmentDuration">3000</property>
                    <property name="Fragmentation">false</property>
                    <property name="NbGopsPerChunk">1</property>
                    <property name="ProgressiveDownload">false</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1496.260009765625,843.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">mp4</property>
                    <property name="drc_iso_file_format">MPEG4</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">false</property>
                    <property name="metadata" isNull="true"/>
                    <property name="onFormatChange">Update Metadata</property>
                    <componentName>ISO MPEG-4 Multiplexer 5</componentName>
                    <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                    <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                    <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                    <childComponents/>
                    <pin name="mp4" type="OUTPUT_IO"/>
                    <pin name="Track 1" type="INPUT_IO">
                        <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Track 2" type="INPUT_IO">
                        <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1048.8123779296875,825.9999694824219</property>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="adv.constraint_set_flag_mode">0</property>
                    <property name="adv.custom_frame_rate_timescale">240000</property>
                    <property name="adv.direct_mode">0</property>
                    <property name="adv.enable_custom_frame_rate_timescale">false</property>
                    <property name="adv.intra_precision">2</property>
                    <property name="adv.mbaff_scan_type">1</property>
                    <property name="adv.ps3_mode">0</property>
                    <property name="adv.qt_mode">0</property>
                    <property name="adv.transform_8x8">0</property>
                    <property name="adv.weighted_pred_flag">1</property>
                    <property name="afd.active_format">8</property>
                    <property name="afd.afd_source">NONE</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="gen.enable_3d_encoding">false</property>
                    <property name="gen.level_idc">0</property>
                    <property name="gen.mbaff_mode">false</property>
                    <property name="gen.profile_idc">77</property>
                    <property name="gen.speed">8</property>
                    <property name="gen.sym_mode">1</property>
                    <property name="gop.adaptive_bframes">true</property>
                    <property name="gop.bframes">2</property>
                    <property name="gop.duration">2</property>
                    <property name="gop.idr_control">1</property>
                    <property name="gop.idr_period">1</property>
                    <property name="gop.min_intra_period">4</property>
                    <property name="gop.mode">0</property>
                    <property name="gop.sps_period">0</property>
                    <property name="load_config_file" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="me.max_refs">1</property>
                    <property name="me.search_range">-1</property>
                    <property name="me.subdiv">7</property>
                    <property name="mt.max_pict_tasks">-1</property>
                    <property name="mt.mode">1</property>
                    <property name="mt.num_threads">8</property>
                    <property name="output.bitdepth" isNull="true"/>
                    <property name="output.video_format.color_space" isNull="true"/>
                    <property name="output.video_format.dar" isNull="true"/>
                    <property name="output.video_format.deinterleave_fields" isNull="true"/>
                    <property name="output.video_format.frame_rate" isNull="true"/>
                    <property name="output.video_format.height">540</property>
                    <property name="output.video_format.scan_type" isNull="true"/>
                    <property name="output.video_format.width">960</property>
                    <property name="preference.threads.bit_depth_down_converter">1</property>
                    <property name="preference.threads.bit_depth_up_converter">1</property>
                    <property name="preference.threads.chroma_resample">1</property>
                    <property name="preference.threads.color_space_convert">1</property>
                    <property name="preference.threads.data_layout_converter">1</property>
                    <property name="preference.threads.de_interlacing">1</property>
                    <property name="preference.threads.progressive_to_interlaced">1</property>
                    <property name="preference.threads.scaling">1</property>
                    <property name="preference.threads.temporal_noise_reduction">1</property>
                    <property name="rc.auto_qp">true</property>
                    <property name="rc.initial_cpb_removal_delay">-1</property>
                    <property name="rc.kbps">1500</property>
                    <property name="rc.max_inter_frame_bytes">0</property>
                    <property name="rc.max_intra_frame_bytes">0</property>
                    <property name="rc.max_kbps">0</property>
                    <property name="rc.min_intra_frame_bytes">0</property>
                    <property name="rc.qp_delta_b">4</property>
                    <property name="rc.qp_delta_p">2</property>
                    <property name="rc.qp_intra">30</property>
                    <property name="rc.qp_max">51</property>
                    <property name="rc.qp_min">8</property>
                    <property name="rc.scene_detect">35</property>
                    <property name="rc.type">2</property>
                    <property name="rc.unrestricted_iframepicsize">0</property>
                    <property name="rc.vbv_length">1000</property>
                    <property name="save_config_file" isNull="true"/>
                    <property name="save_post_validation_config_file" isNull="true"/>
                    <property name="save_pre_validation_config_file" isNull="true"/>
                    <property name="sei.afd_repetition">0</property>
                    <property name="sei.pic_timing_flag">1</property>
                    <property name="sei.split_sei_payload">0</property>
                    <property name="slice.deblock">1</property>
                    <property name="slice.mode">0</property>
                    <property name="slice.param">1</property>
                    <property name="suppress_validation">false</property>
                    <property name="tc.enable_timecode_frame_rate_above_30fps_retimer">FALSE</property>
                    <property name="tc.input">none</property>
                    <property name="tc.retimer_behavior">Data is Missing</property>
                    <property name="tc.start_timecode">00:00:00:00/30</property>
                    <property name="tc.video_format">Same as Input(1tc/frame)</property>
                    <property name="tc.video_format_retimer" isNull="true"/>
                    <property name="ud.field_dominance">INPUT_PIN</property>
                    <property name="ud.target_format">ATSC53</property>
                    <property name="ud.target_stream">AVC</property>
                    <property name="vui.bitstream_restriction_flag">false</property>
                    <property name="vui.colour_description_present_flag">false</property>
                    <property name="vui.colour_primaries">2</property>
                    <property name="vui.extended_sar" isNull="true"/>
                    <property name="vui.matrix_coeffients">2</property>
                    <property name="vui.max_bytes_per_pic_denom">false</property>
                    <property name="vui.nal_hrd_parameters_present_flag">false</property>
                    <property name="vui.pic_struct_present_flag">true</property>
                    <property name="vui.sar">0</property>
                    <property name="vui.timing_info_present_flag">false</property>
                    <property name="vui.transfer_characteristics">2</property>
                    <property name="vui.vcl_hrd_parameters_present_flag">false</property>
                    <property name="vui.video_format">2</property>
                    <property name="vui.video_signal_type_present_flag">true</property>
                    <property name="xds.adult-language">FALSE</property>
                    <property name="xds.analog-source-bit">0</property>
                    <property name="xds.aps-bit">0</property>
                    <property name="xds.basic_category" isNull="true"/>
                    <property name="xds.basic_category_1" isNull="true"/>
                    <property name="xds.basic_category_2" isNull="true"/>
                    <property name="xds.basic_category_3" isNull="true"/>
                    <property name="xds.basic_category_4" isNull="true"/>
                    <property name="xds.basic_category_5" isNull="true"/>
                    <property name="xds.basic_category_6" isNull="true"/>
                    <property name="xds.cgms-a-bit">0</property>
                    <property name="xds.cgms-a-repetition-rate">2.50</property>
                    <property name="xds.detailed_category" isNull="true"/>
                    <property name="xds.detailed_category_1" isNull="true"/>
                    <property name="xds.detailed_category_10" isNull="true"/>
                    <property name="xds.detailed_category_11" isNull="true"/>
                    <property name="xds.detailed_category_12" isNull="true"/>
                    <property name="xds.detailed_category_13" isNull="true"/>
                    <property name="xds.detailed_category_14" isNull="true"/>
                    <property name="xds.detailed_category_15" isNull="true"/>
                    <property name="xds.detailed_category_16" isNull="true"/>
                    <property name="xds.detailed_category_17" isNull="true"/>
                    <property name="xds.detailed_category_18" isNull="true"/>
                    <property name="xds.detailed_category_19" isNull="true"/>
                    <property name="xds.detailed_category_2" isNull="true"/>
                    <property name="xds.detailed_category_20" isNull="true"/>
                    <property name="xds.detailed_category_21" isNull="true"/>
                    <property name="xds.detailed_category_22" isNull="true"/>
                    <property name="xds.detailed_category_23" isNull="true"/>
                    <property name="xds.detailed_category_24" isNull="true"/>
                    <property name="xds.detailed_category_25" isNull="true"/>
                    <property name="xds.detailed_category_26" isNull="true"/>
                    <property name="xds.detailed_category_27" isNull="true"/>
                    <property name="xds.detailed_category_28" isNull="true"/>
                    <property name="xds.detailed_category_29" isNull="true"/>
                    <property name="xds.detailed_category_3" isNull="true"/>
                    <property name="xds.detailed_category_30" isNull="true"/>
                    <property name="xds.detailed_category_31" isNull="true"/>
                    <property name="xds.detailed_category_4" isNull="true"/>
                    <property name="xds.detailed_category_5" isNull="true"/>
                    <property name="xds.detailed_category_6" isNull="true"/>
                    <property name="xds.detailed_category_7" isNull="true"/>
                    <property name="xds.detailed_category_8" isNull="true"/>
                    <property name="xds.detailed_category_9" isNull="true"/>
                    <property name="xds.enable-xds-removal">FALSE</property>
                    <property name="xds.fantasy-violence">FALSE</property>
                    <property name="xds.insert-cgms-a">FALSE</property>
                    <property name="xds.insert-network-name">FALSE</property>
                    <property name="xds.insert-program-description">FALSE</property>
                    <property name="xds.insert-program-name">FALSE</property>
                    <property name="xds.insert-program-type">FALSE</property>
                    <property name="xds.insert-station-id-native-channel">FALSE</property>
                    <property name="xds.insert-tsid">FALSE</property>
                    <property name="xds.insert-v-chip-info">FALSE</property>
                    <property name="xds.native-channel" isNull="true"/>
                    <property name="xds.network-name" isNull="true"/>
                    <property name="xds.network-name-repetition-rate">2.50</property>
                    <property name="xds.number_of_basic_categories">1</property>
                    <property name="xds.number_of_detailed_categories">0</property>
                    <property name="xds.program-description" isNull="true"/>
                    <property name="xds.program-description-repetition-rate">20.0</property>
                    <property name="xds.program-name" isNull="true"/>
                    <property name="xds.program-name-repetition-rate">2.50</property>
                    <property name="xds.program-type-repetition-rate">20.0</property>
                    <property name="xds.rating" isNull="true"/>
                    <property name="xds.sexual-situations">FALSE</property>
                    <property name="xds.sexually-suggestive-dialog">FALSE</property>
                    <property name="xds.station-id" isNull="true"/>
                    <property name="xds.station-id-native-channel-repetition-rate">2.50</property>
                    <property name="xds.system">0</property>
                    <property name="xds.tsid" isNull="true"/>
                    <property name="xds.tsid-repetition-rate">2.50</property>
                    <property name="xds.v-chip-repetition-rate">2.50</property>
                    <property name="xds.violence">FALSE</property>
                    <componentName>AVC Video Encoder 960x540 1500 kbps</componentName>
                    <componentDefinitionName>AVC Video Encoder</componentDefinitionName>
                    <componentDefinitionGuid>43e72a55-1e8f-4827-a6e3-3217b07ba7e9</componentDefinitionGuid>
                    <componentOwningPluginName>DRAVCEncoder</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.DRAVCEncoder</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_IO"/>
                    <pin name="tc" type="INPUT_IO"/>
                    <pin name="cc608" type="INPUT_IO"/>
                    <pin name="cc708" type="INPUT_IO"/>
                    <pin name="afd" type="INPUT_IO"/>
                    <pin name="ud" type="INPUT_IO"/>
                    <pin name="out" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="AVCMuxMode">avc1</property>
                    <property name="AlternateAudioTracks">true</property>
                    <property name="AlternateSubtitleTracks">true</property>
                    <property name="ChunkDuration">1000</property>
                    <property name="ChunkMode">GOP count or duration</property>
                    <property name="FragmentDuration">3000</property>
                    <property name="Fragmentation">false</property>
                    <property name="NbGopsPerChunk">1</property>
                    <property name="ProgressiveDownload">false</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1491.260009765625,1037.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">mp4</property>
                    <property name="drc_iso_file_format">MPEG4</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">false</property>
                    <property name="metadata" isNull="true"/>
                    <property name="onFormatChange">Update Metadata</property>
                    <componentName>ISO MPEG-4 Multiplexer 6</componentName>
                    <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                    <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                    <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                    <childComponents/>
                    <pin name="mp4" type="OUTPUT_IO"/>
                    <pin name="Track 1" type="INPUT_IO">
                        <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Track 2" type="INPUT_IO">
                        <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1053.8123779296875,1034.0000305175781</property>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="adv.constraint_set_flag_mode">0</property>
                    <property name="adv.custom_frame_rate_timescale">240000</property>
                    <property name="adv.direct_mode">0</property>
                    <property name="adv.enable_custom_frame_rate_timescale">false</property>
                    <property name="adv.intra_precision">2</property>
                    <property name="adv.mbaff_scan_type">1</property>
                    <property name="adv.ps3_mode">0</property>
                    <property name="adv.qt_mode">0</property>
                    <property name="adv.transform_8x8">0</property>
                    <property name="adv.weighted_pred_flag">1</property>
                    <property name="afd.active_format">8</property>
                    <property name="afd.afd_source">NONE</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="gen.enable_3d_encoding">false</property>
                    <property name="gen.level_idc">0</property>
                    <property name="gen.mbaff_mode">false</property>
                    <property name="gen.profile_idc">77</property>
                    <property name="gen.speed">8</property>
                    <property name="gen.sym_mode">1</property>
                    <property name="gop.adaptive_bframes">true</property>
                    <property name="gop.bframes">2</property>
                    <property name="gop.duration">2</property>
                    <property name="gop.idr_control">1</property>
                    <property name="gop.idr_period">1</property>
                    <property name="gop.min_intra_period">4</property>
                    <property name="gop.mode">0</property>
                    <property name="gop.sps_period">0</property>
                    <property name="load_config_file" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="me.max_refs">1</property>
                    <property name="me.search_range">-1</property>
                    <property name="me.subdiv">7</property>
                    <property name="mt.max_pict_tasks">-1</property>
                    <property name="mt.mode">1</property>
                    <property name="mt.num_threads">8</property>
                    <property name="output.bitdepth" isNull="true"/>
                    <property name="output.video_format.color_space" isNull="true"/>
                    <property name="output.video_format.dar" isNull="true"/>
                    <property name="output.video_format.deinterleave_fields" isNull="true"/>
                    <property name="output.video_format.frame_rate" isNull="true"/>
                    <property name="output.video_format.height">360</property>
                    <property name="output.video_format.scan_type" isNull="true"/>
                    <property name="output.video_format.width">640</property>
                    <property name="preference.threads.bit_depth_down_converter">1</property>
                    <property name="preference.threads.bit_depth_up_converter">1</property>
                    <property name="preference.threads.chroma_resample">1</property>
                    <property name="preference.threads.color_space_convert">1</property>
                    <property name="preference.threads.data_layout_converter">1</property>
                    <property name="preference.threads.de_interlacing">1</property>
                    <property name="preference.threads.progressive_to_interlaced">1</property>
                    <property name="preference.threads.scaling">1</property>
                    <property name="preference.threads.temporal_noise_reduction">1</property>
                    <property name="rc.auto_qp">true</property>
                    <property name="rc.initial_cpb_removal_delay">-1</property>
                    <property name="rc.kbps">1000</property>
                    <property name="rc.max_inter_frame_bytes">0</property>
                    <property name="rc.max_intra_frame_bytes">0</property>
                    <property name="rc.max_kbps">0</property>
                    <property name="rc.min_intra_frame_bytes">0</property>
                    <property name="rc.qp_delta_b">4</property>
                    <property name="rc.qp_delta_p">2</property>
                    <property name="rc.qp_intra">30</property>
                    <property name="rc.qp_max">51</property>
                    <property name="rc.qp_min">8</property>
                    <property name="rc.scene_detect">35</property>
                    <property name="rc.type">2</property>
                    <property name="rc.unrestricted_iframepicsize">0</property>
                    <property name="rc.vbv_length">1000</property>
                    <property name="save_config_file" isNull="true"/>
                    <property name="save_post_validation_config_file" isNull="true"/>
                    <property name="save_pre_validation_config_file" isNull="true"/>
                    <property name="sei.afd_repetition">0</property>
                    <property name="sei.pic_timing_flag">1</property>
                    <property name="sei.split_sei_payload">0</property>
                    <property name="slice.deblock">1</property>
                    <property name="slice.mode">0</property>
                    <property name="slice.param">1</property>
                    <property name="suppress_validation">false</property>
                    <property name="tc.enable_timecode_frame_rate_above_30fps_retimer">FALSE</property>
                    <property name="tc.input">none</property>
                    <property name="tc.retimer_behavior">Data is Missing</property>
                    <property name="tc.start_timecode">00:00:00:00/30</property>
                    <property name="tc.video_format">Same as Input(1tc/frame)</property>
                    <property name="tc.video_format_retimer" isNull="true"/>
                    <property name="ud.field_dominance">INPUT_PIN</property>
                    <property name="ud.target_format">ATSC53</property>
                    <property name="ud.target_stream">AVC</property>
                    <property name="vui.bitstream_restriction_flag">false</property>
                    <property name="vui.colour_description_present_flag">false</property>
                    <property name="vui.colour_primaries">2</property>
                    <property name="vui.extended_sar" isNull="true"/>
                    <property name="vui.matrix_coeffients">2</property>
                    <property name="vui.max_bytes_per_pic_denom">false</property>
                    <property name="vui.nal_hrd_parameters_present_flag">false</property>
                    <property name="vui.pic_struct_present_flag">true</property>
                    <property name="vui.sar">0</property>
                    <property name="vui.timing_info_present_flag">false</property>
                    <property name="vui.transfer_characteristics">2</property>
                    <property name="vui.vcl_hrd_parameters_present_flag">false</property>
                    <property name="vui.video_format">2</property>
                    <property name="vui.video_signal_type_present_flag">true</property>
                    <property name="xds.adult-language">FALSE</property>
                    <property name="xds.analog-source-bit">0</property>
                    <property name="xds.aps-bit">0</property>
                    <property name="xds.basic_category" isNull="true"/>
                    <property name="xds.basic_category_1" isNull="true"/>
                    <property name="xds.basic_category_2" isNull="true"/>
                    <property name="xds.basic_category_3" isNull="true"/>
                    <property name="xds.basic_category_4" isNull="true"/>
                    <property name="xds.basic_category_5" isNull="true"/>
                    <property name="xds.basic_category_6" isNull="true"/>
                    <property name="xds.cgms-a-bit">0</property>
                    <property name="xds.cgms-a-repetition-rate">2.50</property>
                    <property name="xds.detailed_category" isNull="true"/>
                    <property name="xds.detailed_category_1" isNull="true"/>
                    <property name="xds.detailed_category_10" isNull="true"/>
                    <property name="xds.detailed_category_11" isNull="true"/>
                    <property name="xds.detailed_category_12" isNull="true"/>
                    <property name="xds.detailed_category_13" isNull="true"/>
                    <property name="xds.detailed_category_14" isNull="true"/>
                    <property name="xds.detailed_category_15" isNull="true"/>
                    <property name="xds.detailed_category_16" isNull="true"/>
                    <property name="xds.detailed_category_17" isNull="true"/>
                    <property name="xds.detailed_category_18" isNull="true"/>
                    <property name="xds.detailed_category_19" isNull="true"/>
                    <property name="xds.detailed_category_2" isNull="true"/>
                    <property name="xds.detailed_category_20" isNull="true"/>
                    <property name="xds.detailed_category_21" isNull="true"/>
                    <property name="xds.detailed_category_22" isNull="true"/>
                    <property name="xds.detailed_category_23" isNull="true"/>
                    <property name="xds.detailed_category_24" isNull="true"/>
                    <property name="xds.detailed_category_25" isNull="true"/>
                    <property name="xds.detailed_category_26" isNull="true"/>
                    <property name="xds.detailed_category_27" isNull="true"/>
                    <property name="xds.detailed_category_28" isNull="true"/>
                    <property name="xds.detailed_category_29" isNull="true"/>
                    <property name="xds.detailed_category_3" isNull="true"/>
                    <property name="xds.detailed_category_30" isNull="true"/>
                    <property name="xds.detailed_category_31" isNull="true"/>
                    <property name="xds.detailed_category_4" isNull="true"/>
                    <property name="xds.detailed_category_5" isNull="true"/>
                    <property name="xds.detailed_category_6" isNull="true"/>
                    <property name="xds.detailed_category_7" isNull="true"/>
                    <property name="xds.detailed_category_8" isNull="true"/>
                    <property name="xds.detailed_category_9" isNull="true"/>
                    <property name="xds.enable-xds-removal">FALSE</property>
                    <property name="xds.fantasy-violence">FALSE</property>
                    <property name="xds.insert-cgms-a">FALSE</property>
                    <property name="xds.insert-network-name">FALSE</property>
                    <property name="xds.insert-program-description">FALSE</property>
                    <property name="xds.insert-program-name">FALSE</property>
                    <property name="xds.insert-program-type">FALSE</property>
                    <property name="xds.insert-station-id-native-channel">FALSE</property>
                    <property name="xds.insert-tsid">FALSE</property>
                    <property name="xds.insert-v-chip-info">FALSE</property>
                    <property name="xds.native-channel" isNull="true"/>
                    <property name="xds.network-name" isNull="true"/>
                    <property name="xds.network-name-repetition-rate">2.50</property>
                    <property name="xds.number_of_basic_categories">1</property>
                    <property name="xds.number_of_detailed_categories">0</property>
                    <property name="xds.program-description" isNull="true"/>
                    <property name="xds.program-description-repetition-rate">20.0</property>
                    <property name="xds.program-name" isNull="true"/>
                    <property name="xds.program-name-repetition-rate">2.50</property>
                    <property name="xds.program-type-repetition-rate">20.0</property>
                    <property name="xds.rating" isNull="true"/>
                    <property name="xds.sexual-situations">FALSE</property>
                    <property name="xds.sexually-suggestive-dialog">FALSE</property>
                    <property name="xds.station-id" isNull="true"/>
                    <property name="xds.station-id-native-channel-repetition-rate">2.50</property>
                    <property name="xds.system">0</property>
                    <property name="xds.tsid" isNull="true"/>
                    <property name="xds.tsid-repetition-rate">2.50</property>
                    <property name="xds.v-chip-repetition-rate">2.50</property>
                    <property name="xds.violence">FALSE</property>
                    <componentName>AVC Video Encoder 640x360 1000 kbps</componentName>
                    <componentDefinitionName>AVC Video Encoder</componentDefinitionName>
                    <componentDefinitionGuid>43e72a55-1e8f-4827-a6e3-3217b07ba7e9</componentDefinitionGuid>
                    <componentOwningPluginName>DRAVCEncoder</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.DRAVCEncoder</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_IO"/>
                    <pin name="tc" type="INPUT_IO"/>
                    <pin name="cc608" type="INPUT_IO"/>
                    <pin name="cc708" type="INPUT_IO"/>
                    <pin name="afd" type="INPUT_IO"/>
                    <pin name="ud" type="INPUT_IO"/>
                    <pin name="out" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="AVCMuxMode">avc1</property>
                    <property name="AlternateAudioTracks">true</property>
                    <property name="AlternateSubtitleTracks">true</property>
                    <property name="ChunkDuration">1000</property>
                    <property name="ChunkMode">GOP count or duration</property>
                    <property name="FragmentDuration">3000</property>
                    <property name="Fragmentation">false</property>
                    <property name="NbGopsPerChunk">1</property>
                    <property name="ProgressiveDownload">false</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1485.260009765625,1254.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">mp4</property>
                    <property name="drc_iso_file_format">MPEG4</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">false</property>
                    <property name="metadata" isNull="true"/>
                    <property name="onFormatChange">Update Metadata</property>
                    <componentName>ISO MPEG-4 Multiplexer 7</componentName>
                    <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                    <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                    <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                    <childComponents/>
                    <pin name="mp4" type="OUTPUT_IO"/>
                    <pin name="Track 1" type="INPUT_IO">
                        <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Track 2" type="INPUT_IO">
                        <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1045.8123779296875,1231.0000305175781</property>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="adv.constraint_set_flag_mode">0</property>
                    <property name="adv.custom_frame_rate_timescale">240000</property>
                    <property name="adv.direct_mode">0</property>
                    <property name="adv.enable_custom_frame_rate_timescale">false</property>
                    <property name="adv.intra_precision">2</property>
                    <property name="adv.mbaff_scan_type">1</property>
                    <property name="adv.ps3_mode">0</property>
                    <property name="adv.qt_mode">0</property>
                    <property name="adv.transform_8x8">0</property>
                    <property name="adv.weighted_pred_flag">1</property>
                    <property name="afd.active_format">8</property>
                    <property name="afd.afd_source">NONE</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="gen.enable_3d_encoding">false</property>
                    <property name="gen.level_idc">0</property>
                    <property name="gen.mbaff_mode">false</property>
                    <property name="gen.profile_idc">77</property>
                    <property name="gen.speed">8</property>
                    <property name="gen.sym_mode">1</property>
                    <property name="gop.adaptive_bframes">true</property>
                    <property name="gop.bframes">2</property>
                    <property name="gop.duration">2</property>
                    <property name="gop.idr_control">1</property>
                    <property name="gop.idr_period">1</property>
                    <property name="gop.min_intra_period">4</property>
                    <property name="gop.mode">0</property>
                    <property name="gop.sps_period">0</property>
                    <property name="load_config_file" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="me.max_refs">1</property>
                    <property name="me.search_range">-1</property>
                    <property name="me.subdiv">7</property>
                    <property name="mt.max_pict_tasks">-1</property>
                    <property name="mt.mode">1</property>
                    <property name="mt.num_threads">8</property>
                    <property name="output.bitdepth" isNull="true"/>
                    <property name="output.video_format.color_space" isNull="true"/>
                    <property name="output.video_format.dar" isNull="true"/>
                    <property name="output.video_format.deinterleave_fields" isNull="true"/>
                    <property name="output.video_format.frame_rate" isNull="true"/>
                    <property name="output.video_format.height">360</property>
                    <property name="output.video_format.scan_type" isNull="true"/>
                    <property name="output.video_format.width">640</property>
                    <property name="preference.threads.bit_depth_down_converter">1</property>
                    <property name="preference.threads.bit_depth_up_converter">1</property>
                    <property name="preference.threads.chroma_resample">1</property>
                    <property name="preference.threads.color_space_convert">1</property>
                    <property name="preference.threads.data_layout_converter">1</property>
                    <property name="preference.threads.de_interlacing">1</property>
                    <property name="preference.threads.progressive_to_interlaced">1</property>
                    <property name="preference.threads.scaling">1</property>
                    <property name="preference.threads.temporal_noise_reduction">1</property>
                    <property name="rc.auto_qp">true</property>
                    <property name="rc.initial_cpb_removal_delay">-1</property>
                    <property name="rc.kbps">650</property>
                    <property name="rc.max_inter_frame_bytes">0</property>
                    <property name="rc.max_intra_frame_bytes">0</property>
                    <property name="rc.max_kbps">0</property>
                    <property name="rc.min_intra_frame_bytes">0</property>
                    <property name="rc.qp_delta_b">4</property>
                    <property name="rc.qp_delta_p">2</property>
                    <property name="rc.qp_intra">30</property>
                    <property name="rc.qp_max">51</property>
                    <property name="rc.qp_min">8</property>
                    <property name="rc.scene_detect">35</property>
                    <property name="rc.type">2</property>
                    <property name="rc.unrestricted_iframepicsize">0</property>
                    <property name="rc.vbv_length">1000</property>
                    <property name="save_config_file" isNull="true"/>
                    <property name="save_post_validation_config_file" isNull="true"/>
                    <property name="save_pre_validation_config_file" isNull="true"/>
                    <property name="sei.afd_repetition">0</property>
                    <property name="sei.pic_timing_flag">1</property>
                    <property name="sei.split_sei_payload">0</property>
                    <property name="slice.deblock">1</property>
                    <property name="slice.mode">0</property>
                    <property name="slice.param">1</property>
                    <property name="suppress_validation">false</property>
                    <property name="tc.enable_timecode_frame_rate_above_30fps_retimer">FALSE</property>
                    <property name="tc.input">none</property>
                    <property name="tc.retimer_behavior">Data is Missing</property>
                    <property name="tc.start_timecode">00:00:00:00/30</property>
                    <property name="tc.video_format">Same as Input(1tc/frame)</property>
                    <property name="tc.video_format_retimer" isNull="true"/>
                    <property name="ud.field_dominance">INPUT_PIN</property>
                    <property name="ud.target_format">ATSC53</property>
                    <property name="ud.target_stream">AVC</property>
                    <property name="vui.bitstream_restriction_flag">false</property>
                    <property name="vui.colour_description_present_flag">false</property>
                    <property name="vui.colour_primaries">2</property>
                    <property name="vui.extended_sar" isNull="true"/>
                    <property name="vui.matrix_coeffients">2</property>
                    <property name="vui.max_bytes_per_pic_denom">false</property>
                    <property name="vui.nal_hrd_parameters_present_flag">false</property>
                    <property name="vui.pic_struct_present_flag">true</property>
                    <property name="vui.sar">0</property>
                    <property name="vui.timing_info_present_flag">false</property>
                    <property name="vui.transfer_characteristics">2</property>
                    <property name="vui.vcl_hrd_parameters_present_flag">false</property>
                    <property name="vui.video_format">2</property>
                    <property name="vui.video_signal_type_present_flag">true</property>
                    <property name="xds.adult-language">FALSE</property>
                    <property name="xds.analog-source-bit">0</property>
                    <property name="xds.aps-bit">0</property>
                    <property name="xds.basic_category" isNull="true"/>
                    <property name="xds.basic_category_1" isNull="true"/>
                    <property name="xds.basic_category_2" isNull="true"/>
                    <property name="xds.basic_category_3" isNull="true"/>
                    <property name="xds.basic_category_4" isNull="true"/>
                    <property name="xds.basic_category_5" isNull="true"/>
                    <property name="xds.basic_category_6" isNull="true"/>
                    <property name="xds.cgms-a-bit">0</property>
                    <property name="xds.cgms-a-repetition-rate">2.50</property>
                    <property name="xds.detailed_category" isNull="true"/>
                    <property name="xds.detailed_category_1" isNull="true"/>
                    <property name="xds.detailed_category_10" isNull="true"/>
                    <property name="xds.detailed_category_11" isNull="true"/>
                    <property name="xds.detailed_category_12" isNull="true"/>
                    <property name="xds.detailed_category_13" isNull="true"/>
                    <property name="xds.detailed_category_14" isNull="true"/>
                    <property name="xds.detailed_category_15" isNull="true"/>
                    <property name="xds.detailed_category_16" isNull="true"/>
                    <property name="xds.detailed_category_17" isNull="true"/>
                    <property name="xds.detailed_category_18" isNull="true"/>
                    <property name="xds.detailed_category_19" isNull="true"/>
                    <property name="xds.detailed_category_2" isNull="true"/>
                    <property name="xds.detailed_category_20" isNull="true"/>
                    <property name="xds.detailed_category_21" isNull="true"/>
                    <property name="xds.detailed_category_22" isNull="true"/>
                    <property name="xds.detailed_category_23" isNull="true"/>
                    <property name="xds.detailed_category_24" isNull="true"/>
                    <property name="xds.detailed_category_25" isNull="true"/>
                    <property name="xds.detailed_category_26" isNull="true"/>
                    <property name="xds.detailed_category_27" isNull="true"/>
                    <property name="xds.detailed_category_28" isNull="true"/>
                    <property name="xds.detailed_category_29" isNull="true"/>
                    <property name="xds.detailed_category_3" isNull="true"/>
                    <property name="xds.detailed_category_30" isNull="true"/>
                    <property name="xds.detailed_category_31" isNull="true"/>
                    <property name="xds.detailed_category_4" isNull="true"/>
                    <property name="xds.detailed_category_5" isNull="true"/>
                    <property name="xds.detailed_category_6" isNull="true"/>
                    <property name="xds.detailed_category_7" isNull="true"/>
                    <property name="xds.detailed_category_8" isNull="true"/>
                    <property name="xds.detailed_category_9" isNull="true"/>
                    <property name="xds.enable-xds-removal">FALSE</property>
                    <property name="xds.fantasy-violence">FALSE</property>
                    <property name="xds.insert-cgms-a">FALSE</property>
                    <property name="xds.insert-network-name">FALSE</property>
                    <property name="xds.insert-program-description">FALSE</property>
                    <property name="xds.insert-program-name">FALSE</property>
                    <property name="xds.insert-program-type">FALSE</property>
                    <property name="xds.insert-station-id-native-channel">FALSE</property>
                    <property name="xds.insert-tsid">FALSE</property>
                    <property name="xds.insert-v-chip-info">FALSE</property>
                    <property name="xds.native-channel" isNull="true"/>
                    <property name="xds.network-name" isNull="true"/>
                    <property name="xds.network-name-repetition-rate">2.50</property>
                    <property name="xds.number_of_basic_categories">1</property>
                    <property name="xds.number_of_detailed_categories">0</property>
                    <property name="xds.program-description" isNull="true"/>
                    <property name="xds.program-description-repetition-rate">20.0</property>
                    <property name="xds.program-name" isNull="true"/>
                    <property name="xds.program-name-repetition-rate">2.50</property>
                    <property name="xds.program-type-repetition-rate">20.0</property>
                    <property name="xds.rating" isNull="true"/>
                    <property name="xds.sexual-situations">FALSE</property>
                    <property name="xds.sexually-suggestive-dialog">FALSE</property>
                    <property name="xds.station-id" isNull="true"/>
                    <property name="xds.station-id-native-channel-repetition-rate">2.50</property>
                    <property name="xds.system">0</property>
                    <property name="xds.tsid" isNull="true"/>
                    <property name="xds.tsid-repetition-rate">2.50</property>
                    <property name="xds.v-chip-repetition-rate">2.50</property>
                    <property name="xds.violence">FALSE</property>
                    <componentName>AVC Video Encoder 640x360 650 kbps</componentName>
                    <componentDefinitionName>AVC Video Encoder</componentDefinitionName>
                    <componentDefinitionGuid>43e72a55-1e8f-4827-a6e3-3217b07ba7e9</componentDefinitionGuid>
                    <componentOwningPluginName>DRAVCEncoder</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.DRAVCEncoder</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_IO"/>
                    <pin name="tc" type="INPUT_IO"/>
                    <pin name="cc608" type="INPUT_IO"/>
                    <pin name="cc708" type="INPUT_IO"/>
                    <pin name="afd" type="INPUT_IO"/>
                    <pin name="ud" type="INPUT_IO"/>
                    <pin name="out" type="OUTPUT_IO"/>
                </component>
                <component>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1835.260009765625,25.0</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="exclusiveMode">false</property>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_1920x1080_6000.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_1920x1080_6000.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output - Video 1</componentName>
                    <componentDefinitionName>File Output</componentDefinitionName>
                    <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                    <childComponents/>
                    <pin name="write" type="INPUT_IO"/>
                    <pin name="filename" type="INPUT_IO"/>
                    <pin name="writeComplete" type="OUTPUT_IO"/>
                </component>
                <component>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1837.260009765625,237.0</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="exclusiveMode">false</property>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_1920x1080_4700.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_1920x1080_4700.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output - Video 2</componentName>
                    <componentDefinitionName>File Output</componentDefinitionName>
                    <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                    <childComponents/>
                    <pin name="write" type="INPUT_IO"/>
                    <pin name="filename" type="INPUT_IO"/>
                    <pin name="writeComplete" type="OUTPUT_IO"/>
                </component>
                <component>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1837.260009765625,993.0</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="exclusiveMode">false</property>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_640x360_1000.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_640x360_1000.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output - Video 6</componentName>
                    <componentDefinitionName>File Output</componentDefinitionName>
                    <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                    <childComponents/>
                    <pin name="write" type="INPUT_IO"/>
                    <pin name="filename" type="INPUT_IO"/>
                    <pin name="writeComplete" type="OUTPUT_IO"/>
                </component>
                <component>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1842.260009765625,799.0</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="exclusiveMode">false</property>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_960x540_1500.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_960x540_1500.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output - Video 5</componentName>
                    <componentDefinitionName>File Output</componentDefinitionName>
                    <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                    <childComponents/>
                    <pin name="write" type="INPUT_IO"/>
                    <pin name="filename" type="INPUT_IO"/>
                    <pin name="writeComplete" type="OUTPUT_IO"/>
                </component>
                <component>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1843.260009765625,591.0</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="exclusiveMode">false</property>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_960x540_2250.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_960x540_2250.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output - Video 4</componentName>
                    <componentDefinitionName>File Output</componentDefinitionName>
                    <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                    <childComponents/>
                    <pin name="write" type="INPUT_IO"/>
                    <pin name="filename" type="INPUT_IO"/>
                    <pin name="writeComplete" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="AVCMuxMode">avc1</property>
                    <property name="AlternateAudioTracks">true</property>
                    <property name="AlternateSubtitleTracks">true</property>
                    <property name="ChunkDuration">1000</property>
                    <property name="ChunkMode">GOP count or duration</property>
                    <property name="FragmentDuration">3000</property>
                    <property name="Fragmentation">false</property>
                    <property name="NbGopsPerChunk">1</property>
                    <property name="ProgressiveDownload">false</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1489.260009765625,1439.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">mp4</property>
                    <property name="drc_iso_file_format">MPEG4</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">false</property>
                    <property name="metadata" isNull="true"/>
                    <property name="onFormatChange">Update Metadata</property>
                    <componentName>ISO MPEG-4 Multiplexer 8</componentName>
                    <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                    <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                    <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                    <childComponents/>
                    <pin name="mp4" type="OUTPUT_IO"/>
                    <pin name="Track 1" type="INPUT_IO">
                        <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Track 2" type="INPUT_IO">
                        <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1045.8123779296875,1428.0000305175781</property>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="adv.constraint_set_flag_mode">0</property>
                    <property name="adv.custom_frame_rate_timescale">240000</property>
                    <property name="adv.direct_mode">0</property>
                    <property name="adv.enable_custom_frame_rate_timescale">false</property>
                    <property name="adv.intra_precision">2</property>
                    <property name="adv.mbaff_scan_type">1</property>
                    <property name="adv.ps3_mode">0</property>
                    <property name="adv.qt_mode">0</property>
                    <property name="adv.transform_8x8">0</property>
                    <property name="adv.weighted_pred_flag">1</property>
                    <property name="afd.active_format">8</property>
                    <property name="afd.afd_source">NONE</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="gen.enable_3d_encoding">false</property>
                    <property name="gen.level_idc">0</property>
                    <property name="gen.mbaff_mode">false</property>
                    <property name="gen.profile_idc">66</property>
                    <property name="gen.speed">8</property>
                    <property name="gen.sym_mode">1</property>
                    <property name="gop.adaptive_bframes">true</property>
                    <property name="gop.bframes">0</property>
                    <property name="gop.duration">2</property>
                    <property name="gop.idr_control">1</property>
                    <property name="gop.idr_period">1</property>
                    <property name="gop.min_intra_period">4</property>
                    <property name="gop.mode">0</property>
                    <property name="gop.sps_period">0</property>
                    <property name="load_config_file" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="me.max_refs">1</property>
                    <property name="me.search_range">-1</property>
                    <property name="me.subdiv">7</property>
                    <property name="mt.max_pict_tasks">-1</property>
                    <property name="mt.mode">1</property>
                    <property name="mt.num_threads">8</property>
                    <property name="output.bitdepth" isNull="true"/>
                    <property name="output.video_format.color_space" isNull="true"/>
                    <property name="output.video_format.dar" isNull="true"/>
                    <property name="output.video_format.deinterleave_fields" isNull="true"/>
                    <property name="output.video_format.frame_rate" isNull="true"/>
                    <property name="output.video_format.height">180</property>
                    <property name="output.video_format.scan_type" isNull="true"/>
                    <property name="output.video_format.width">320</property>
                    <property name="preference.threads.bit_depth_down_converter">1</property>
                    <property name="preference.threads.bit_depth_up_converter">1</property>
                    <property name="preference.threads.chroma_resample">1</property>
                    <property name="preference.threads.color_space_convert">1</property>
                    <property name="preference.threads.data_layout_converter">1</property>
                    <property name="preference.threads.de_interlacing">1</property>
                    <property name="preference.threads.progressive_to_interlaced">1</property>
                    <property name="preference.threads.scaling">1</property>
                    <property name="preference.threads.temporal_noise_reduction">1</property>
                    <property name="rc.auto_qp">true</property>
                    <property name="rc.initial_cpb_removal_delay">-1</property>
                    <property name="rc.kbps">400</property>
                    <property name="rc.max_inter_frame_bytes">0</property>
                    <property name="rc.max_intra_frame_bytes">0</property>
                    <property name="rc.max_kbps">0</property>
                    <property name="rc.min_intra_frame_bytes">0</property>
                    <property name="rc.qp_delta_b">4</property>
                    <property name="rc.qp_delta_p">2</property>
                    <property name="rc.qp_intra">30</property>
                    <property name="rc.qp_max">51</property>
                    <property name="rc.qp_min">8</property>
                    <property name="rc.scene_detect">35</property>
                    <property name="rc.type">2</property>
                    <property name="rc.unrestricted_iframepicsize">0</property>
                    <property name="rc.vbv_length">1000</property>
                    <property name="save_config_file" isNull="true"/>
                    <property name="save_post_validation_config_file" isNull="true"/>
                    <property name="save_pre_validation_config_file" isNull="true"/>
                    <property name="sei.afd_repetition">0</property>
                    <property name="sei.pic_timing_flag">1</property>
                    <property name="sei.split_sei_payload">0</property>
                    <property name="slice.deblock">1</property>
                    <property name="slice.mode">0</property>
                    <property name="slice.param">1</property>
                    <property name="suppress_validation">false</property>
                    <property name="tc.enable_timecode_frame_rate_above_30fps_retimer">FALSE</property>
                    <property name="tc.input">none</property>
                    <property name="tc.retimer_behavior">Data is Missing</property>
                    <property name="tc.start_timecode">00:00:00:00/30</property>
                    <property name="tc.video_format">Same as Input(1tc/frame)</property>
                    <property name="tc.video_format_retimer" isNull="true"/>
                    <property name="ud.field_dominance">INPUT_PIN</property>
                    <property name="ud.target_format">ATSC53</property>
                    <property name="ud.target_stream">AVC</property>
                    <property name="vui.bitstream_restriction_flag">false</property>
                    <property name="vui.colour_description_present_flag">false</property>
                    <property name="vui.colour_primaries">2</property>
                    <property name="vui.extended_sar" isNull="true"/>
                    <property name="vui.matrix_coeffients">2</property>
                    <property name="vui.max_bytes_per_pic_denom">false</property>
                    <property name="vui.nal_hrd_parameters_present_flag">false</property>
                    <property name="vui.pic_struct_present_flag">true</property>
                    <property name="vui.sar">0</property>
                    <property name="vui.timing_info_present_flag">false</property>
                    <property name="vui.transfer_characteristics">2</property>
                    <property name="vui.vcl_hrd_parameters_present_flag">false</property>
                    <property name="vui.video_format">2</property>
                    <property name="vui.video_signal_type_present_flag">true</property>
                    <property name="xds.adult-language">FALSE</property>
                    <property name="xds.analog-source-bit">0</property>
                    <property name="xds.aps-bit">0</property>
                    <property name="xds.basic_category" isNull="true"/>
                    <property name="xds.basic_category_1" isNull="true"/>
                    <property name="xds.basic_category_2" isNull="true"/>
                    <property name="xds.basic_category_3" isNull="true"/>
                    <property name="xds.basic_category_4" isNull="true"/>
                    <property name="xds.basic_category_5" isNull="true"/>
                    <property name="xds.basic_category_6" isNull="true"/>
                    <property name="xds.cgms-a-bit">0</property>
                    <property name="xds.cgms-a-repetition-rate">2.50</property>
                    <property name="xds.detailed_category" isNull="true"/>
                    <property name="xds.detailed_category_1" isNull="true"/>
                    <property name="xds.detailed_category_10" isNull="true"/>
                    <property name="xds.detailed_category_11" isNull="true"/>
                    <property name="xds.detailed_category_12" isNull="true"/>
                    <property name="xds.detailed_category_13" isNull="true"/>
                    <property name="xds.detailed_category_14" isNull="true"/>
                    <property name="xds.detailed_category_15" isNull="true"/>
                    <property name="xds.detailed_category_16" isNull="true"/>
                    <property name="xds.detailed_category_17" isNull="true"/>
                    <property name="xds.detailed_category_18" isNull="true"/>
                    <property name="xds.detailed_category_19" isNull="true"/>
                    <property name="xds.detailed_category_2" isNull="true"/>
                    <property name="xds.detailed_category_20" isNull="true"/>
                    <property name="xds.detailed_category_21" isNull="true"/>
                    <property name="xds.detailed_category_22" isNull="true"/>
                    <property name="xds.detailed_category_23" isNull="true"/>
                    <property name="xds.detailed_category_24" isNull="true"/>
                    <property name="xds.detailed_category_25" isNull="true"/>
                    <property name="xds.detailed_category_26" isNull="true"/>
                    <property name="xds.detailed_category_27" isNull="true"/>
                    <property name="xds.detailed_category_28" isNull="true"/>
                    <property name="xds.detailed_category_29" isNull="true"/>
                    <property name="xds.detailed_category_3" isNull="true"/>
                    <property name="xds.detailed_category_30" isNull="true"/>
                    <property name="xds.detailed_category_31" isNull="true"/>
                    <property name="xds.detailed_category_4" isNull="true"/>
                    <property name="xds.detailed_category_5" isNull="true"/>
                    <property name="xds.detailed_category_6" isNull="true"/>
                    <property name="xds.detailed_category_7" isNull="true"/>
                    <property name="xds.detailed_category_8" isNull="true"/>
                    <property name="xds.detailed_category_9" isNull="true"/>
                    <property name="xds.enable-xds-removal">FALSE</property>
                    <property name="xds.fantasy-violence">FALSE</property>
                    <property name="xds.insert-cgms-a">FALSE</property>
                    <property name="xds.insert-network-name">FALSE</property>
                    <property name="xds.insert-program-description">FALSE</property>
                    <property name="xds.insert-program-name">FALSE</property>
                    <property name="xds.insert-program-type">FALSE</property>
                    <property name="xds.insert-station-id-native-channel">FALSE</property>
                    <property name="xds.insert-tsid">FALSE</property>
                    <property name="xds.insert-v-chip-info">FALSE</property>
                    <property name="xds.native-channel" isNull="true"/>
                    <property name="xds.network-name" isNull="true"/>
                    <property name="xds.network-name-repetition-rate">2.50</property>
                    <property name="xds.number_of_basic_categories">1</property>
                    <property name="xds.number_of_detailed_categories">0</property>
                    <property name="xds.program-description" isNull="true"/>
                    <property name="xds.program-description-repetition-rate">20.0</property>
                    <property name="xds.program-name" isNull="true"/>
                    <property name="xds.program-name-repetition-rate">2.50</property>
                    <property name="xds.program-type-repetition-rate">20.0</property>
                    <property name="xds.rating" isNull="true"/>
                    <property name="xds.sexual-situations">FALSE</property>
                    <property name="xds.sexually-suggestive-dialog">FALSE</property>
                    <property name="xds.station-id" isNull="true"/>
                    <property name="xds.station-id-native-channel-repetition-rate">2.50</property>
                    <property name="xds.system">0</property>
                    <property name="xds.tsid" isNull="true"/>
                    <property name="xds.tsid-repetition-rate">2.50</property>
                    <property name="xds.v-chip-repetition-rate">2.50</property>
                    <property name="xds.violence">FALSE</property>
                    <componentName>AVC Video Encoder 320x180 400 kbps</componentName>
                    <componentDefinitionName>AVC Video Encoder</componentDefinitionName>
                    <componentDefinitionGuid>43e72a55-1e8f-4827-a6e3-3217b07ba7e9</componentDefinitionGuid>
                    <componentOwningPluginName>DRAVCEncoder</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.DRAVCEncoder</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_IO"/>
                    <pin name="tc" type="INPUT_IO"/>
                    <pin name="cc608" type="INPUT_IO"/>
                    <pin name="cc708" type="INPUT_IO"/>
                    <pin name="afd" type="INPUT_IO"/>
                    <pin name="ud" type="INPUT_IO"/>
                    <pin name="out" type="OUTPUT_IO"/>
                </component>
                <component>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1851.260009765625,396.0</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="exclusiveMode">false</property>
                    <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../sourceFileBaseName"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_1280x720_3700.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\\\${ROOT_sourceFileBaseName}_1280x720_3700.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output - Video 3</componentName>
                    <componentDefinitionName>File Output</componentDefinitionName>
                    <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                    <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                    <childComponents/>
                    <pin name="write" type="INPUT_IO"/>
                    <pin name="filename" type="INPUT_IO"/>
                    <pin name="writeComplete" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">314.0000305175781,2023.273681640625</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 3</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">311.0000305175781,2111.2738037109375</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 4</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">307.0000305175781,2194.2738037109375</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 5</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">311.0000305175781,2276.2738037109375</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 6</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">307.0000305175781,2359.2738037109375</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 7</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">304.0000305175781,2447.27392578125</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 8</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">300.0000305175781,2530.27392578125</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 9</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">308.0000305175781,2616.1187744140625</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 10</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">304.0000305175781,2699.1187744140625</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 11</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">301.0000305175781,2787.118896484375</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 12</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">297.0000305175781,2870.118896484375</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 13</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">307.0000305175781,2958.3187255859375</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 14</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">303.0000305175781,3041.3187255859375</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 15</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">300.0000305175781,3129.31884765625</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 16</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">296.0000305175781,3212.31884765625</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 17</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">298.0000305175781,3299.18408203125</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_parentIgnoresOurErrors" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="graphThreadAffinityMask" isNull="true"/>
                    <property name="graphThreadAffinityMode" isNull="true"/>
                    <property name="graphThreadAffinityNuma" isNull="true"/>
                    <property name="ignoreChildComponentErrors">true</property>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>Audio 18</componentName>
                    <componentDefinitionName>Kayak Graph</componentDefinitionName>
                    <componentDefinitionGuid>abc785f2-427e-4522-ba00-f3cb6acd1220</componentDefinitionGuid>
                    <componentOwningPluginName>KayakCore</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.KayakCore</componentOwningPluginId>
                    <childComponents>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="Decode_audio">true</property>
                            <property name="Decode_captions_AFD">true</property>
                            <property name="EndTime" isNull="true"/>
                            <property name="EndTimecode" isNull="true"/>
                            <property name="StartTime" isNull="true"/>
                            <property name="StartTimecode" isNull="true"/>
                            <property name="TargetFrameRate" isNull="true"/>
                            <property name="TrimmingMode">Timestamp</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">0.0,0.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="always_use_directshow">false</property>
                            <property name="blackThreshold">0.10</property>
                            <property name="black_border_detection">false</property>
                            <property name="captions_conform">true</property>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="enable_directshow">false</property>
                            <property name="filename">empty</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="inspection_max_megabytes" isNull="true"/>
                            <property name="inspection_max_seconds" isNull="true"/>
                            <property name="inspection_mode" isNull="true"/>
                            <property name="logFile" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="max_drift" isNull="true"/>
                            <property name="max_latency">8008/30000</property>
                            <property name="noiseThreshold">0.10</property>
                            <property name="probeDuration">60.0</property>
                            <property name="probeRate">0.10</property>
                            <property name="probeTimeInterval">1.0</property>
                            <property name="truncation">true</property>
                            <property name="validation">true</property>
                            <componentName>Media File Input</componentName>
                            <componentDefinitionName>Media File Input</componentDefinitionName>
                            <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                            <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                            <childComponents/>
                            <pin name="filename" type="PROPERTY">
                                <property name="_pinProperty">filename</property>
                            </pin>
                            <pin name="CompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="CompressedAudio" displayName="Uncompressed Audio (WAVE)" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="UncompressedAudio" type="OUTPUT_IO">
                                <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">347.0,16.0</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="language_code">fr</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="override_language_code">true</property>
                            <componentName>Language</componentName>
                            <componentDefinitionName>Language Code Updater</componentDefinitionName>
                            <componentDefinitionGuid>563232cc-20ba-453f-8f69-43284cea7abc</componentDefinitionGuid>
                            <componentOwningPluginName>CommonLanguage</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonLanguage</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH"/>
                            <pin name="out" type="OUTPUT_PUSH"/>
                        </component>
                        <component>
                            <property name="5_1_to_stereo" isNull="true"/>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">558.0,22.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="assign_missing_speaker_positions">true</property>
                            <property name="attenuation3db">false</property>
                            <property name="bitstreamformat">ADTSMP4</property>
                            <property name="center_gain_db">-3.0</property>
                            <property name="channel_position_preset">L_R</property>
                            <property name="copyright">true</property>
                            <property name="datarate">128000</property>
                            <property name="defaultInputPin">audio</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="dialoguenormalization">-27</property>
                            <property name="downmix_to_mono">false</property>
                            <property name="downmixstyle">Disabled</property>
                            <property name="drclinemode">COMPPROF_FILMSTD</property>
                            <property name="drcrfmode">COMPPROF_FILMSTD</property>
                            <property name="dsurrmode">DSM_NOTINDICATED</property>
                            <property name="encodemode">DP_AAC</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="left_gain_db">0.0</property>
                            <property name="lfe_gain_db">-12.0</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="lorocmix">-3 dB</property>
                            <property name="lorosmix">-3 dB</property>
                            <property name="lowpassfilterlfe">true</property>
                            <property name="ltrtcmix">-3 dB</property>
                            <property name="ltrtsmix">-3 dB</property>
                            <property name="originalbitstream">true</property>
                            <property name="output_bits_per_sample">16</property>
                            <property name="output_sample_rate" isNull="true"/>
                            <property name="phaseshift90">true</property>
                            <property name="prefsterdmix">Lt/Rt downmix preferred</property>
                            <property name="removepce">false</property>
                            <property name="right_gain_db">0.0</property>
                            <property name="signallingmode">SBR_BC</property>
                            <property name="surround_gain_db">-3.0</property>
                            <property name="usePSv2">false</property>
                            <property name="use_metadata">true</property>
                            <componentName>AAC Encoder (Dolby)</componentName>
                            <componentDefinitionName>AAC Encoder - Dolby Pulse</componentDefinitionName>
                            <componentDefinitionGuid>D0933A55-4818-4ADC-9301-8BE7687AC9E3</componentDefinitionGuid>
                            <componentOwningPluginName>DolbyPulseEncoder</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.DolbyPulseEncoder</componentOwningPluginId>
                            <childComponents/>
                            <pin name="audio" type="INPUT_IO"/>
                            <pin name="out" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                            <property name="AVCMuxMode">avc1</property>
                            <property name="AlternateAudioTracks">true</property>
                            <property name="AlternateSubtitleTracks">true</property>
                            <property name="ChunkDuration">1000</property>
                            <property name="ChunkMode">GOP count or duration</property>
                            <property name="FragmentDuration">3000</property>
                            <property name="Fragmentation">false</property>
                            <property name="NbGopsPerChunk">1</property>
                            <property name="ProgressiveDownload">false</property>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">964.0,23.0</property>
                            <property name="_graphMinDisplaySize" isNull="true"/>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin">mp4</property>
                            <property name="drc_iso_file_format">MPEG4</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">false</property>
                            <property name="metadata" isNull="true"/>
                            <property name="onFormatChange">Update Metadata</property>
                            <componentName>ISO MPEG-4 Audio Multiplexer</componentName>
                            <componentDefinitionName>Advanced ISO MPEG4 Multiplexer</componentDefinitionName>
                            <componentDefinitionGuid>E25468C3-A65C-4f1a-8172-E72CE4B63A70</componentDefinitionGuid>
                            <componentOwningPluginName>MPEG4Muxer</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.MPEG4Muxer</componentOwningPluginId>
                            <childComponents/>
                            <pin name="mp4" type="OUTPUT_IO"/>
                            <pin name="Track 1" type="INPUT_IO">
                                <pinDefinition name="Track 1" type="INPUT_IO" dynamic="true"/>
                            </pin>
                            <pin name="Track 2" type="INPUT_IO">
                                <pinDefinition name="Track 2" type="INPUT_IO" dynamic="true"/>
                            </pin>
                        </component>
                        <component>
                            <property name="_graphDisplayContents">false</property>
                            <property name="_graphDisplayLocation">1313.0,13.0</property>
                            <property name="_graphMinDisplaySize">500.0,400.0</property>
                            <property name="_logDebugInfoOnError" isNull="true"/>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="acquireChildLicenses" isNull="true"/>
                            <property name="defaultInputPin" isNull="true"/>
                            <property name="defaultOutputPin" isNull="true"/>
                            <property name="exclusiveMode">false</property>
                            <property name="filename" marshallerKey="Evaluation">Expression
&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;expressionInfo&gt;
    &lt;propertyBinding variable="ROOT_outputWriteDirectory" path="../../../outputWriteDirectory"/&gt;
    &lt;propertyBinding variable="ROOT_sourceFileBaseName" path="../../../sourceFileBaseName"/&gt;
    &lt;propertyBinding variable="language_code" path="../../Language/language_code"/&gt;
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_${language_code}_128.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                            <property name="graphThreadAffinityMask" isNull="true"/>
                            <property name="graphThreadAffinityMode" isNull="true"/>
                            <property name="graphThreadAffinityNuma" isNull="true"/>
                            <property name="ignoreChildComponentErrors" isNull="true"/>
                            <property name="ignoreParentGraphState" isNull="true"/>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="manual_input">true</property>
                            <componentName>File Output - Audio</componentName>
                            <componentDefinitionName>File Output</componentDefinitionName>
                            <componentDefinitionGuid>9b376163-de8d-4e09-8bed-353725b6b6d6</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="write" type="INPUT_IO"/>
                            <pin name="filename" type="INPUT_IO"/>
                            <pin name="writeComplete" type="OUTPUT_IO"/>
                        </component>
                        <component>
                            <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                            <property name="_graphDisplayContents" isNull="true"/>
                            <property name="_graphDisplayLocation">373.60235595703125,88.228759765625</property>
                            <property name="_parentIgnoresOurErrors" isNull="true"/>
                            <property name="_timeBase_local" isNull="true"/>
                            <property name="commandScript" isNull="true"/>
                            <property name="defaultInputPin">in</property>
                            <property name="defaultOutputPin">out</property>
                            <property name="lifeCycleScript">import java.lang.String;
import ca.digitalrapids.kayak.data.impl.DefaultDataContainer;
import ca.digitalrapids.kayak.data.*;
import ca.digitalrapids.kayak.data.impl.*;
import ca.digitalrapids.kayak.util.*;
import ca.digitalrapids.kayak.api.*;
import ca.digitalrapids.kayak.graph.*;
import ca.digitalrapids.kayak.graph.impl.*;

if (graphState == 'initial') 
	{	
	node.log("===============");
		
	node.log("determining if audio required");
	def audioInput = false;
	KayakNode parentNode = node.getParentNode();

	nodeWalker = new KayakNodeWalker(parentNode);
	def kayakNodeIterator = nodeWalker.iterator();
	while ( kayakNodeIterator.hasNext() )
		{
		KayakNode nextNode = kayakNodeIterator.next();
		if ( nextNode instanceof KayakComponent )
			{
			if ( nextNode.getNodeName() == "Media File Input" )
				{
				def textstring = nextNode.getPropertyAsString( "filename", null );
		
				if(textstring == null || textstring == "empty")
					{						
					def outPin = nextNode.getOutputPin("UncompressedAudio");					
					outPin.disconnectAllPinConnections();
					node.log("disconnected the output pin for MFI");

					def outPin2 = parentNode.getOutputPin("writeComplete");	
					outPin2.disconnectAllPinConnections();
					}						
				}					
			}
		}
	}</property>
                            <property name="logsMaxEntries" isNull="true"/>
                            <property name="processInputScript" isNull="true"/>
                            <property name="realizeScript" isNull="true"/>
                            <componentName>Script - Make Audio Optional</componentName>
                            <componentDefinitionName>Scripted Component</componentDefinitionName>
                            <componentDefinitionGuid>2c5d7c09-9db8-4bb5-9dab-b2682268e2be</componentDefinitionGuid>
                            <componentOwningPluginName>CommonComponents</componentOwningPluginName>
                            <componentOwningPluginId>ca.digitalrapids.CommonComponents</componentOwningPluginId>
                            <childComponents/>
                            <pin name="in" type="INPUT_PUSH">
                                <pinDefinition name="in" type="INPUT_PUSH" dynamic="true"/>
                            </pin>
                            <pin name="out" type="OUTPUT_PUSH">
                                <pinDefinition name="out" type="OUTPUT_PUSH" dynamic="true"/>
                            </pin>
                        </component>
                    </childComponents>
                    <pin name="writeComplete" type="OUTPUT_IO">
                        <propertyDefinition transient="false" hidden="true" name="_userDeleteable" group="System" dynamic="true"/>
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                        <property name="_graphDisplayLocation">847.0,747.0</property>
                        <property name="_userDeleteable">true</property>
                        <pinDefinition name="writeComplete" displayName="Write Complete" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
            </childComponents>
            <pin name="primarySourceFile" type="PROPERTY">
                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                <property name="_graphDisplayLocation">1.6775016784667969,37.93499755859375</property>
                <property name="_pinProperty">primarySourceFile</property>
            </pin>
            <pin name="clipListXml" type="PROPERTY">
                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                <property name="_graphDisplayLocation">1.3549995422363281,15.902501106262207</property>
                <property name="_pinProperty">clipListXml</property>
            </pin>
            <pin name="outputAssetsCommand" type="COMMAND">
                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                <property name="_graphDisplayLocation">2747.9007568359375,1670.6909713745117</property>
            </pin>
        </component>
    </components>
    <pinConnections>
        <connection>
            <sourcePath>AMS Manifest Writer/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - En128/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 1/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 7/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 2/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 1/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 2/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 3/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 4/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 5/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 6/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 8/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 3/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 4/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 5/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 6/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 7/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 8/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 9/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 10/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 11/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 12/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 13/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 14/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 15/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 16/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 17/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 18/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input Video/UncompressedVideo</sourcePath>
            <destinationPath>Color Space Standard Updater/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input Video/UncompressedAudio</sourcePath>
            <destinationPath>Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Language/out</sourcePath>
            <destinationPath>AAC Encoder (Dolby) En128/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>AAC Encoder (Dolby) En128/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer Eb128/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer Eb128/mp4</sourcePath>
            <destinationPath>File Output - En128/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - En128/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 9</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 1/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 2/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 2</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 3/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 3</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 4/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 4</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 5/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 5</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 6/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 6</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 7/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 7</destinationPath>
        </connection>
        <connection>
            <sourcePath>File Output - Video 8/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 8</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 1/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 10</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 2/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 11</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 3/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 12</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 4/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 13</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 5/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 14</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 6/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 15</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 7/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 16</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 8/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 17</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 9/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 18</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 10/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 19</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 11/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 20</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 12/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 21</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 13/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 22</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 14/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 23</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 15/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 24</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 16/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 25</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 17/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 26</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 18/writeComplete</sourcePath>
            <destinationPath>AMS Manifest Writer/Asset 27</destinationPath>
        </connection>
        <connection>
            <sourcePath>Color Space Standard Updater/out</sourcePath>
            <destinationPath>Video Format Converter/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video Format Converter/out</sourcePath>
            <destinationPath>AVC Video Encoder 1920x1080 6000 kbps/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video Format Converter/out</sourcePath>
            <destinationPath>AVC Video Encoder 1920x1080 4700 kbps/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video Format Converter/out</sourcePath>
            <destinationPath>AVC Video Encoder 1280x720 3400 kbps/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video Format Converter/out</sourcePath>
            <destinationPath>AVC Video Encoder 960x540 2250 kbps/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video Format Converter/out</sourcePath>
            <destinationPath>AVC Video Encoder 960x540 1500 kbps/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video Format Converter/out</sourcePath>
            <destinationPath>AVC Video Encoder 640x360 1000 kbps/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video Format Converter/out</sourcePath>
            <destinationPath>AVC Video Encoder 640x360 650 kbps/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video Format Converter/out</sourcePath>
            <destinationPath>AVC Video Encoder 320x180 400 kbps/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 1/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 1/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 1/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 1/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 1/Language/out</sourcePath>
            <destinationPath>Audio 1/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 1/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 1/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 1/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 1/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 2/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 2/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 2/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 2/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 2/Language/out</sourcePath>
            <destinationPath>Audio 2/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 2/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 2/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 2/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 2/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer 7/mp4</sourcePath>
            <destinationPath>File Output - Video 7/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer 8/mp4</sourcePath>
            <destinationPath>File Output - Video 8/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>AVC Video Encoder 1920x1080 6000 kbps/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer 1/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer 1/mp4</sourcePath>
            <destinationPath>File Output - Video 1/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer 2/mp4</sourcePath>
            <destinationPath>File Output - Video 2/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>AVC Video Encoder 1920x1080 4700 kbps/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer 2/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer 3/mp4</sourcePath>
            <destinationPath>File Output - Video 3/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>AVC Video Encoder 1280x720 3400 kbps/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer 3/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer 4/mp4</sourcePath>
            <destinationPath>File Output - Video 4/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>AVC Video Encoder 960x540 2250 kbps/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer 4/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer 5/mp4</sourcePath>
            <destinationPath>File Output - Video 5/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>AVC Video Encoder 960x540 1500 kbps/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer 5/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer 6/mp4</sourcePath>
            <destinationPath>File Output - Video 6/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>AVC Video Encoder 640x360 1000 kbps/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer 6/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>AVC Video Encoder 640x360 650 kbps/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer 7/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>AVC Video Encoder 320x180 400 kbps/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer 8/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 3/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 3/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 3/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 3/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 3/Language/out</sourcePath>
            <destinationPath>Audio 3/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 3/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 3/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 3/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 3/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 4/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 4/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 4/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 4/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 4/Language/out</sourcePath>
            <destinationPath>Audio 4/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 4/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 4/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 4/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 4/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 5/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 5/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 5/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 5/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 5/Language/out</sourcePath>
            <destinationPath>Audio 5/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 5/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 5/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 5/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 5/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 6/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 6/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 6/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 6/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 6/Language/out</sourcePath>
            <destinationPath>Audio 6/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 6/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 6/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 6/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 6/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 7/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 7/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 7/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 7/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 7/Language/out</sourcePath>
            <destinationPath>Audio 7/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 7/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 7/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 7/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 7/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 8/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 8/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 8/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 8/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 8/Language/out</sourcePath>
            <destinationPath>Audio 8/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 8/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 8/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 8/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 8/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 9/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 9/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 9/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 9/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 9/Language/out</sourcePath>
            <destinationPath>Audio 9/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 9/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 9/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 9/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 9/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 10/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 10/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 10/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 10/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 10/Language/out</sourcePath>
            <destinationPath>Audio 10/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 10/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 10/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 10/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 10/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 11/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 11/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 11/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 11/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 11/Language/out</sourcePath>
            <destinationPath>Audio 11/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 11/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 11/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 11/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 11/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 12/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 12/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 12/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 12/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 12/Language/out</sourcePath>
            <destinationPath>Audio 12/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 12/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 12/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 12/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 12/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 13/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 13/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 13/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 13/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 13/Language/out</sourcePath>
            <destinationPath>Audio 13/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 13/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 13/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 13/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 13/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 14/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 14/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 14/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 14/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 14/Language/out</sourcePath>
            <destinationPath>Audio 14/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 14/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 14/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 14/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 14/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 15/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 15/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 15/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 15/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 15/Language/out</sourcePath>
            <destinationPath>Audio 15/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 15/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 15/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 15/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 15/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 16/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 16/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 16/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 16/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 16/Language/out</sourcePath>
            <destinationPath>Audio 16/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 16/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 16/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 16/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 16/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 17/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 17/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 17/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 17/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 17/Language/out</sourcePath>
            <destinationPath>Audio 17/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 17/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 17/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 17/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 17/File Output - Audio/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 18/File Output - Audio/writeComplete</sourcePath>
            <destinationPath>Audio 18/writeComplete</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 18/Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>Audio 18/Language/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 18/Language/out</sourcePath>
            <destinationPath>Audio 18/AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 18/AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>Audio 18/ISO MPEG-4 Audio Multiplexer/Track 1</destinationPath>
        </connection>
        <connection>
            <sourcePath>Audio 18/ISO MPEG-4 Audio Multiplexer/mp4</sourcePath>
            <destinationPath>Audio 18/File Output - Audio/write</destinationPath>
        </connection>
    </pinConnections>
    <authoringInfo>
        <kayakFrameworkVersion>1.7.2.0</kayakFrameworkVersion>
        <userName>xpouyat</userName>
        <userLanguage>en</userLanguage>
        <platform>Windows</platform>
        <osName>Windows 10</osName>
        <osArch>amd64</osArch>
        <osVersion>10.0</osVersion>
        <authoredDate>2016-11-04T17:30:13.071+01:00</authoredDate>
        <plugins>
            <plugin pluginId="ca.digitalrapids.AACSourceController" name="AAC Source Controller">
                <pluginVersion>1.5.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-18 15:29:11-0400</buildDate>
                    <checksum>6d5819c59c8e19266879d4c78ace54e6</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AC3SourceController" name="AC3 Source Controller">
                <pluginVersion>1.6.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-18 15:30:45-0400</buildDate>
                    <checksum>a4c8dfd276522195c847cc5ac7e82cb6</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AES3AudioProcessor" name="AES3AudioProcessor">
                <pluginVersion>1.8.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:53:48-0400</buildDate>
                    <checksum>7b6bc5fc85e0a6c802c0f6019841da39</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AVCSourceController" name="AVC Source Controller">
                <pluginVersion>1.7.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-18 11:26:21-0400</buildDate>
                    <checksum>4206fafdf37fe1dfcf1dc4ce6756267d</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.ActiveImageCrop" name="ActiveImageCrop">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:56:35-0400</buildDate>
                    <checksum>49139b1d4ad4c384310a989c676cee18</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AlphaChannelUtilities" name="AlphaChannelUtilities">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:58:08-0400</buildDate>
                    <checksum>7163a1856d7f18f99d4f6c9382914778</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.Assets" name="Assets">
                <pluginVersion>1.0.5.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-11-16 22:31:10-0500</buildDate>
                    <checksum>02bb76d11531ceace39cd274be7d9c27</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AudioFormatConverter" name="AudioFormatConverter">
                <pluginVersion>1.1.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 09:17:41-0400</buildDate>
                    <checksum>8cb7ac531decf5d5219fd96e41dcdd94</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AudioFormatUtilities" name="AudioFormatUtilities">
                <pluginVersion>1.13.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 08:49:16-0400</buildDate>
                    <checksum>58548f3c1a16fffa53fa5d2b94b7f691</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.ChromaResampler" name="ChromaResampler">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:58:56-0400</buildDate>
                    <checksum>5f27921f5b276d2ee47d318c25aa419a</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.ClosedCaptionsUtilities" name="ClosedCaptionsUtilities">
                <pluginVersion>1.7.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 08:56:17-0400</buildDate>
                    <checksum>b7fb4d38a7fcdbecee5a3048651f4b3a</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonAAC" name="CommonAAC">
                <pluginVersion>1.1.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-09-30 11:17:59-0400</buildDate>
                    <checksum>188aa71e2fafc87c1feb63b98b5933a4</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonAC3" name="CommonAC3">
                <pluginVersion>1.3.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-08-06 12:30:52-0400</buildDate>
                    <checksum>5ce148e5f5783ec69336b72d12d9b4a8</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonAES3" name="CommonAES3">
                <pluginVersion>1.2.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-02-05 12:16:28-0500</buildDate>
                    <checksum>450ea9b38fded4b37deb9cbc4f0f28f6</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonAVC" name="CommonAVC">
                <pluginVersion>1.5.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-04-26 10:21:55-0400</buildDate>
                    <checksum>e1a52abfd6b41c6ea9b6c20cc0ae7446</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonAVCEncoder" name="CommonAVCEncoder">
                <pluginVersion>1.1.6.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-04 10:54:04-0400</buildDate>
                    <checksum>789a83cff68dfcfbdec4af91aa24d6a8</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonComponents" name="CommonComponents">
                <pluginVersion>1.18.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-26 14:33:17-0400</buildDate>
                    <checksum>4ac8ae85ac32b4b823bae05db931296f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonCuePoint" name="CommonCuePoint">
                <pluginVersion>1.1.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-12 14:17:50-0400</buildDate>
                    <checksum>3a73f972da948562b3b030c881ba19b4</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonDTS" name="CommonDTS">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-02-05 16:29:30-0500</buildDate>
                    <checksum>b47ecd82ba53dcca736784efd7d2d6a1</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonDV" name="CommonDV">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-02-05 16:29:44-0500</buildDate>
                    <checksum>581bda38d92daf60a96174a1760a8c76</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonDolbyE" name="CommonDolbyE">
                <pluginVersion>1.2.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-06-05 17:12:45-0400</buildDate>
                    <checksum>8bb859ada3dd0b3c84da638f503dedc5</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonEAC3" name="CommonEAC3">
                <pluginVersion>1.2.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-08-06 12:31:10-0400</buildDate>
                    <checksum>fe3828204f7ee515fe56d8f8736f80db</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonFont" name="CommonFont">
                <pluginVersion>1.0.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-02-21 19:27:37-0500</buildDate>
                    <checksum>385df375fa90cde4af26294936172be7</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonImageFormats" name="CommonImageFormats">
                <pluginVersion>1.0.24.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-09-22 07:51:55-0400</buildDate>
                    <checksum>3a87c5d7f9c7d2bbd001eb237a0d5e2f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonIntelIPP" name="CommonIntelIPP">
                <pluginVersion>1.2.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:50:18-0400</buildDate>
                    <checksum>b59816bd5cbb49a3ab81df74eb00ab0e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonJ2KVideo" name="CommonJ2KVideo">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-12-10 13:21:47-0500</buildDate>
                    <checksum>7efb0bd2e81cf37c79c8119fbc1304c9</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonLanguage" name="CommonLanguage">
                <pluginVersion>1.1.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-18 16:31:32-0400</buildDate>
                    <checksum>8e0cbfdf03361886ac7d8c0ff4d6a2a9</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMPEG" name="CommonMPEG">
                <pluginVersion>1.0.5.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-11-16 22:26:20-0500</buildDate>
                    <checksum>768e789a483bd66793ceb1fbe1319b0a</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMPEG1" name="CommonMPEG1">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-02-05 12:17:22-0500</buildDate>
                    <checksum>27565032c30a0de4541b77262ca34a95</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMPEG2" name="CommonMPEG2">
                <pluginVersion>1.6.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-18 18:22:19-0400</buildDate>
                    <checksum>f3efbcef0a66f4b442b49f8bd9ef02ea</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMPEG4" name="CommonMPEG4">
                <pluginVersion>1.0.12.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-11-16 22:23:54-0500</buildDate>
                    <checksum>51aed3ea316ed781e6d86db70267c6c6</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMXF" name="CommonMXF">
                <pluginVersion>1.0.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-11-16 22:48:52-0500</buildDate>
                    <checksum>d6323db57110b4b505f4cf6afe1547ce</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMedia" name="CommonMedia">
                <pluginVersion>1.25.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-18 15:25:05-0400</buildDate>
                    <checksum>49e35ad34e1b4b1e5d511857b0b9f001</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMediaEncryption" name="CommonMediaEncryption">
                <pluginVersion>1.0.7.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-06-27 17:24:40-0400</buildDate>
                    <checksum>e8137cd8314ceb3134bd2569645d0cd2</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMetadata" name="CommonMetadata">
                <pluginVersion>1.0.6.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-11-16 22:30:54-0500</buildDate>
                    <checksum>5f854f13c4617c090c1d121b227d9d88</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonPlayReadyEncryption" name="CommonPlayReadyEncryption">
                <pluginVersion>1.0.6.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-11-16 22:49:53-0500</buildDate>
                    <checksum>9e777b913c3bf72ce11bdbe43d2295b1</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonQuickTime" name="CommonQuickTime">
                <pluginVersion>1.0.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-11-16 22:50:40-0500</buildDate>
                    <checksum>80363bbce7317baa24ab8f538f8b8f86</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonSCC" name="CommonSCC">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-07-14 12:49:10-0400</buildDate>
                    <checksum>6a2f083148a9336012fa6ed2543a09ee</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonStereoVideo" name="CommonStereoVideo">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-02-05 16:30:34-0500</buildDate>
                    <checksum>f8764aaec77fe5604228de00578cff0b</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonSubtitles" name="CommonSubtitles">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-04-28 15:20:56-0400</buildDate>
                    <checksum>0caff5fcf2238a8660264ef63ddde63c</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonTimecode" name="CommonTimecode">
                <pluginVersion>1.2.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-05-04 16:57:22-0400</buildDate>
                    <checksum>998e14fd976cbb310fdb42cf6bf0b0d6</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonTimedText" name="CommonTimedText">
                <pluginVersion>1.0.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-11 19:16:43-0500</buildDate>
                    <checksum>bd7301c97546b6cd9e8a91df982358dd</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonUltraviolet" name="CommonUltraviolet">
                <pluginVersion>1.0.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-03-25 11:15:24-0400</buildDate>
                    <checksum>0fbaf01cd371c81f4fcb7cf66f41f6d9</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonVC3" name="CommonVC3">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-02-05 16:30:50-0500</buildDate>
                    <checksum>1fe7814f916437e8d32e470bc8c8ee7e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonVideoSystem" name="CommonVideoSystem">
                <pluginVersion>2.5.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-18 15:25:40-0400</buildDate>
                    <checksum>2d31c4fd9c9966ce8b9ecb772d79e309</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonWAVE" name="CommonWAVE">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-02-05 12:17:51-0500</buildDate>
                    <checksum>0355d46d1c92231032fed50b5947275b</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRAVCEncoder" name="DRAVCEncoder">
                <pluginVersion>1.5.8.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 09:38:15-0400</buildDate>
                    <checksum>b09049069afe1f62ca644af42e454dc7</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRColorspaceConverter" name="DRColorspaceConverter">
                <pluginVersion>1.3.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:51:30-0400</buildDate>
                    <checksum>1bf0186f6b6444075de60ca017229f43</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRDeinterlacer" name="DRDeinterlacer">
                <pluginVersion>1.6.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 08:58:38-0400</buildDate>
                    <checksum>2b9014286167e9047fdc776a8738162b</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRMediaProcessing" name="DRMediaProcessing">
                <pluginVersion>2.12.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:51:18-0400</buildDate>
                    <checksum>5bd71c5e16cc2b6022d1811ab38765fc</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRProgressiveToInterlaced" name="DRProgressiveToInterlaced">
                <pluginVersion>1.3.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 08:59:52-0400</buildDate>
                    <checksum>163456f30b830b0b16eadba3bf35ea87</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRScaler" name="DRScaler">
                <pluginVersion>1.4.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 09:01:05-0400</buildDate>
                    <checksum>2e69858c61c65bafa07f3ea46c2e4eef</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRTemporalNoiseReduction" name="DRTemporalNoiseReduction">
                <pluginVersion>1.3.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 11:02:05-0400</buildDate>
                    <checksum>71632997e4d80383ffa6c2c43178d3b1</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DTSSourceController" name="DTS Source Controller">
                <pluginVersion>1.2.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 11:02:48-0400</buildDate>
                    <checksum>1401f562cd33c266f818b6607b899472</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DirectShowFileSource" name="DirectShowFileSource">
                <pluginVersion>1.2.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:55:41-0400</buildDate>
                    <checksum>4bd06a0c10c1a61997f17b9d832babbf</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DolbyEDecoder" name="DolbyEDecoder">
                <pluginVersion>1.3.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:52:25-0400</buildDate>
                    <checksum>519e863e367415a6b8c9064f641adb3d</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DolbyESourceController" name="Dolby E Source Controller">
                <pluginVersion>1.3.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:52:38-0400</buildDate>
                    <checksum>4b3ff602af6460aa6a32971e56d86497</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DolbyPulseEncoder" name="DolbyPulseEncoder">
                <pluginVersion>1.2.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 09:34:36-0400</buildDate>
                    <checksum>7e691409edf2d2e157d22f41197843a1</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.EAC3SourceController" name="EAC3 Source Controller">
                <pluginVersion>1.5.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-18 15:32:01-0400</buildDate>
                    <checksum>83af8fc20d38d778cb9e7d295e086ae5</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.EIACaptionsRetimer" name="EIACaptionsRetimer">
                <pluginVersion>1.7.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 09:05:33-0400</buildDate>
                    <checksum>79c6e0195e02160b90826f549d66cea5</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.KayakCore" name="KayakCore">
                <pluginVersion>1.7.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-15 10:34:26-0400</buildDate>
                    <checksum>3a25fde0f48f6556e1638e2ad014c473</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.KayakDesigner" name="KayakDesigner">
                <pluginVersion>1.7.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-21 11:30:31-0400</buildDate>
                    <checksum>8075ed6297f64297d814b3fa602a19b1</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2AudioSourceController" name="MPEG2AudioSourceController">
                <pluginVersion>1.2.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:53:32-0400</buildDate>
                    <checksum>167d4f2aef7d91ef3d123789f3f31824</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2UDDemuxer" name="MPEG2UDDemuxer">
                <pluginVersion>1.6.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 16:28:31-0400</buildDate>
                    <checksum>d3700d7cc483a33f8fefc4a97b982590</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2UDMuxer" name="MPEG2UDMuxer">
                <pluginVersion>1.7.6.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 09:46:59-0400</buildDate>
                    <checksum>d980760b82f1bd9e85886d64bd736a57</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG4Muxer" name="MPEG4Muxer">
                <pluginVersion>1.11.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 10:00:58-0400</buildDate>
                    <checksum>debeda332ec0977aa536233a2af8dc00</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MXFDemuxer" name="MXFDemuxer">
                <pluginVersion>1.9.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 11:03:25-0400</buildDate>
                    <checksum>7478b8e1200d0f7de6e6287094305ff0</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MediaInspection" name="MediaInspection">
                <pluginVersion>1.10.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-21 11:08:48-0400</buildDate>
                    <checksum>7eb2619cd3539f28bc0a52880e0fede3</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MediaManager" name="Media Manager">
                <pluginVersion>1.0.58.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-02-04 16:36:07-0500</buildDate>
                    <checksum>c931da9a5e8f0dc43b83dd4c93d1ec25</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MediaManagerWSClient" name="Media Manager WS Client">
                <pluginVersion>1.0.10.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-06-02 13:58:30-0400</buildDate>
                    <checksum>8d95df55931735bcd0069c19b36732f2</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.RasterFlip" name="RasterFlip">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 11:05:50-0400</buildDate>
                    <checksum>5d36826401fe4f818aa4b2910fcfb3a7</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.SCCSourceController" name="SCCSourceController">
                <pluginVersion>1.9.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:59:51-0400</buildDate>
                    <checksum>917e30e9a890205c673183be4d5a690a</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.SMPTE291Demuxer" name="SMPTE291Demuxer">
                <pluginVersion>1.14.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 11:04:36-0400</buildDate>
                    <checksum>3d96bdb4e0c456a3a78d7d76c54c0bb4</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.StreamSynchronizers" name="StreamSynchronizers">
                <pluginVersion>1.9.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 09:51:07-0400</buildDate>
                    <checksum>541dc771547edcd3429234dfdc8bcb6a</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.TimecodeEncoder" name="TimecodeEncoder">
                <pluginVersion>1.4.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 10:25:30-0400</buildDate>
                    <checksum>394c5e1c224d69e97501de600ba89b3f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VC3Decoder" name="VC3Decoder">
                <pluginVersion>1.4.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 11:06:58-0400</buildDate>
                    <checksum>5d68a8654e05630bd92325a828c0722d</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VC3SourceController" name="VC3SourceController">
                <pluginVersion>1.1.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-11-17 00:08:23-0500</buildDate>
                    <checksum>905150bbe3155b95693646110e046220</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoBitDepthConverters" name="VideoBitDepthConverters">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 11:07:19-0400</buildDate>
                    <checksum>986609b2ae2596abb7cf4e8575b5a93d</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoBorder" name="VideoBorder">
                <pluginVersion>1.2.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 10:30:00-0400</buildDate>
                    <checksum>b9a1934d651afb1716e6cafc61f807e1</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoDataLayoutConverter" name="VideoDataLayoutConverter">
                <pluginVersion>1.3.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:51:45-0400</buildDate>
                    <checksum>2f391a190d99ca922320851eea22682b</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoDeinterlacers" name="VideoDeinterlacers">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:57:39-0400</buildDate>
                    <checksum>3ad9ecb616a3be18a3a4d01b2daaf9e5</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoFormatConverter" name="VideoFormatConverter">
                <pluginVersion>1.1.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 09:53:23-0400</buildDate>
                    <checksum>e9dd77b39e59bd8ef372fb5ff1d67301</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoFormatConverter2" name="VideoFormatConverter2">
                <pluginVersion>1.9.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-20 11:53:40-0400</buildDate>
                    <checksum>4204d607d30238923decf2ace973ad3d</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoFormatUtilities" name="VideoFormatUtilities">
                <pluginVersion>1.4.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 10:31:54-0400</buildDate>
                    <checksum>f83b75744449fbf71518d21ca551b11f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoFrameLayout" name="VideoFrameLayout">
                <pluginVersion>1.2.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:55:54-0400</buildDate>
                    <checksum>2fa0f73ca7a0a59c6b2e137c9607dc90</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoProcessor" name="VideoProcessor">
                <pluginVersion>1.1.7.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-19 10:33:15-0400</buildDate>
                    <checksum>e533cd0909a896212328c77f778bcf1e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoPulldownProcessor" name="VideoPulldownProcessor">
                <pluginVersion>1.3.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 11:08:16-0400</buildDate>
                    <checksum>92288c0f59fd87bcf5d9caeaea5f1f4c</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.WAVESourceController" name="WAVESourceController">
                <pluginVersion>1.2.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-05-11 16:58:56-0400</buildDate>
                    <checksum>6106b2582a46456d161c62fa27240935</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.AFDUtilities" name="AFDUtilities">
                <pluginVersion>1.4.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:54:10-0400</buildDate>
                    <checksum>5e21d9d7a0d48b09477de6aca530113c</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.AMSManifestWriter" name="AMSManifestWriter">
                <pluginVersion>1.1.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-20 17:26:44-0400</buildDate>
                    <checksum>b8d9902a5d87b4bbc274e8032a174d5a</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.CommonDolbyMetadata" name="CommonDolbyMetadata">
                <pluginVersion>1.0.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-10-02 09:54:15-0400</buildDate>
                    <checksum>ab33d049bb8a7480d62aedba69db0c15</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.CommonID3Metadata" name="CommonID3Metadata">
                <pluginVersion>1.0.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-09-30 11:17:43-0400</buildDate>
                    <checksum>55e43a54d3ea63901a8037d504599374</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.CommonMediaOrigin" name="CommonMediaOrigin">
                <pluginVersion>1.7.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:55:08-0400</buildDate>
                    <checksum>d18843513a64d7df40ce6d2f85a65010</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.CommonProRes" name="CommonProRes">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-24 17:36:19-0400</buildDate>
                    <checksum>df55b51242ef5bb3086a7a6470ab9d44</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.CommonSourceController" name="CommonSourceController">
                <pluginVersion>1.1.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-20 16:32:34-0400</buildDate>
                    <checksum>5f4a9884670c14887c3cb77cb7d14a41</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.CommonVideoEncoder" name="CommonVideoEncoder">
                <pluginVersion>1.2.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-04-13 17:00:26-0400</buildDate>
                    <checksum>3df16254d3ac256921afcf19fae3cda6</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.CuePointUtilities" name="CuePointUtilities">
                <pluginVersion>1.3.6.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-07-12 14:19:17-0400</buildDate>
                    <checksum>34096e1fe3ffcdcc4fadcf1e27aa45a3</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.EIACaptionsConformer" name="EIACaptionsConformer">
                <pluginVersion>1.3.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:55:24-0400</buildDate>
                    <checksum>b6bf5fbbd2e692bfbd32e8f355577466</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.StreamFillers" name="StreamFillers">
                <pluginVersion>1.8.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2016-06-17 10:51:56-0400</buildDate>
                    <checksum>9a26aea2ee78c53a076021667d7d4b40</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.TimecodeRetimer" name="TimecodeRetimer">
                <pluginVersion>1.2.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-08-06 15:28:41-0400</buildDate>
                    <checksum>7d974c1cb68ddef56d4b2d144ab93526</checksum>
                </buildInfo>
            </plugin>
        </plugins>
        <pluginIdentifiers>
            <plugin name="AAC Source Controller" buildDate="2016-07-18 15:29:11-0400" pluginId="ca.digitalrapids.AACSourceController" pluginVersion="1.5.0.0" platform="Windows" checksum="6d5819c59c8e19266879d4c78ace54e6"/>
            <plugin name="AC3 Source Controller" buildDate="2016-07-18 15:30:45-0400" pluginId="ca.digitalrapids.AC3SourceController" pluginVersion="1.6.0.0" platform="Windows" checksum="a4c8dfd276522195c847cc5ac7e82cb6"/>
            <plugin name="AES3AudioProcessor" buildDate="2016-06-17 10:53:48-0400" pluginId="ca.digitalrapids.AES3AudioProcessor" pluginVersion="1.8.0.0" platform="Windows" checksum="7b6bc5fc85e0a6c802c0f6019841da39"/>
            <plugin name="AVC Source Controller" buildDate="2016-07-18 11:26:21-0400" pluginId="ca.digitalrapids.AVCSourceController" pluginVersion="1.7.2.0" platform="Windows" checksum="4206fafdf37fe1dfcf1dc4ce6756267d"/>
            <plugin name="ActiveImageCrop" buildDate="2016-06-17 10:56:35-0400" pluginId="ca.digitalrapids.ActiveImageCrop" pluginVersion="1.1.1.0" platform="Windows" checksum="49139b1d4ad4c384310a989c676cee18"/>
            <plugin name="AlphaChannelUtilities" buildDate="2016-06-17 10:58:08-0400" pluginId="ca.digitalrapids.AlphaChannelUtilities" pluginVersion="1.1.1.0" platform="Windows" checksum="7163a1856d7f18f99d4f6c9382914778"/>
            <plugin name="Assets" buildDate="2015-11-16 22:31:10-0500" pluginId="ca.digitalrapids.Assets" pluginVersion="1.0.5.0" platform="Generic" checksum="02bb76d11531ceace39cd274be7d9c27"/>
            <plugin name="AudioFormatConverter" buildDate="2016-07-19 09:17:41-0400" pluginId="ca.digitalrapids.AudioFormatConverter" pluginVersion="1.1.4.0" platform="Generic" checksum="8cb7ac531decf5d5219fd96e41dcdd94"/>
            <plugin name="AudioFormatUtilities" buildDate="2016-07-19 08:49:16-0400" pluginId="ca.digitalrapids.AudioFormatUtilities" pluginVersion="1.13.1.0" platform="Windows" checksum="58548f3c1a16fffa53fa5d2b94b7f691"/>
            <plugin name="ChromaResampler" buildDate="2016-06-17 10:58:56-0400" pluginId="ca.digitalrapids.ChromaResampler" pluginVersion="1.1.1.0" platform="Windows" checksum="5f27921f5b276d2ee47d318c25aa419a"/>
            <plugin name="ClosedCaptionsUtilities" buildDate="2016-07-19 08:56:17-0400" pluginId="ca.digitalrapids.ClosedCaptionsUtilities" pluginVersion="1.7.1.0" platform="Windows" checksum="b7fb4d38a7fcdbecee5a3048651f4b3a"/>
            <plugin name="CommonAAC" buildDate="2015-09-30 11:17:59-0400" pluginId="ca.digitalrapids.CommonAAC" pluginVersion="1.1.3.0" platform="Generic" checksum="188aa71e2fafc87c1feb63b98b5933a4"/>
            <plugin name="CommonAC3" buildDate="2015-08-06 12:30:52-0400" pluginId="ca.digitalrapids.CommonAC3" pluginVersion="1.3.1.0" platform="Generic" checksum="5ce148e5f5783ec69336b72d12d9b4a8"/>
            <plugin name="CommonAES3" buildDate="2015-02-05 12:16:28-0500" pluginId="ca.digitalrapids.CommonAES3" pluginVersion="1.2.1.0" platform="Generic" checksum="450ea9b38fded4b37deb9cbc4f0f28f6"/>
            <plugin name="CommonAVC" buildDate="2016-04-26 10:21:55-0400" pluginId="ca.digitalrapids.CommonAVC" pluginVersion="1.5.1.0" platform="Generic" checksum="e1a52abfd6b41c6ea9b6c20cc0ae7446"/>
            <plugin name="CommonAVCEncoder" buildDate="2016-07-04 10:54:04-0400" pluginId="ca.digitalrapids.CommonAVCEncoder" pluginVersion="1.1.6.0" platform="Windows" checksum="789a83cff68dfcfbdec4af91aa24d6a8"/>
            <plugin name="CommonComponents" buildDate="2016-07-26 14:33:17-0400" pluginId="ca.digitalrapids.CommonComponents" pluginVersion="1.18.3.0" platform="Generic" checksum="4ac8ae85ac32b4b823bae05db931296f"/>
            <plugin name="CommonCuePoint" buildDate="2016-07-12 14:17:50-0400" pluginId="ca.digitalrapids.CommonCuePoint" pluginVersion="1.1.2.0" platform="Generic" checksum="3a73f972da948562b3b030c881ba19b4"/>
            <plugin name="CommonDTS" buildDate="2015-02-05 16:29:30-0500" pluginId="ca.digitalrapids.CommonDTS" pluginVersion="1.1.1.0" platform="Generic" checksum="b47ecd82ba53dcca736784efd7d2d6a1"/>
            <plugin name="CommonDV" buildDate="2015-02-05 16:29:44-0500" pluginId="ca.digitalrapids.CommonDV" pluginVersion="1.1.1.0" platform="Generic" checksum="581bda38d92daf60a96174a1760a8c76"/>
            <plugin name="CommonDolbyE" buildDate="2015-06-05 17:12:45-0400" pluginId="ca.digitalrapids.CommonDolbyE" pluginVersion="1.2.1.0" platform="Generic" checksum="8bb859ada3dd0b3c84da638f503dedc5"/>
            <plugin name="CommonEAC3" buildDate="2015-08-06 12:31:10-0400" pluginId="ca.digitalrapids.CommonEAC3" pluginVersion="1.2.1.0" platform="Generic" checksum="fe3828204f7ee515fe56d8f8736f80db"/>
            <plugin name="CommonFont" buildDate="2014-02-21 19:27:37-0500" pluginId="ca.digitalrapids.CommonFont" pluginVersion="1.0.1.0" platform="Generic" checksum="385df375fa90cde4af26294936172be7"/>
            <plugin name="CommonImageFormats" buildDate="2015-09-22 07:51:55-0400" pluginId="ca.digitalrapids.CommonImageFormats" pluginVersion="1.0.24.0" platform="Generic" checksum="3a87c5d7f9c7d2bbd001eb237a0d5e2f"/>
            <plugin name="CommonIntelIPP" buildDate="2016-06-17 10:50:18-0400" pluginId="ca.digitalrapids.CommonIntelIPP" pluginVersion="1.2.0.0" platform="Windows" checksum="b59816bd5cbb49a3ab81df74eb00ab0e"/>
            <plugin name="CommonJ2KVideo" buildDate="2015-12-10 13:21:47-0500" pluginId="ca.digitalrapids.CommonJ2KVideo" pluginVersion="1.1.1.0" platform="Generic" checksum="7efb0bd2e81cf37c79c8119fbc1304c9"/>
            <plugin name="CommonLanguage" buildDate="2016-07-18 16:31:32-0400" pluginId="ca.digitalrapids.CommonLanguage" pluginVersion="1.1.4.0" platform="Windows" checksum="8e0cbfdf03361886ac7d8c0ff4d6a2a9"/>
            <plugin name="CommonMPEG" buildDate="2015-11-16 22:26:20-0500" pluginId="ca.digitalrapids.CommonMPEG" pluginVersion="1.0.5.0" platform="Generic" checksum="768e789a483bd66793ceb1fbe1319b0a"/>
            <plugin name="CommonMPEG1" buildDate="2015-02-05 12:17:22-0500" pluginId="ca.digitalrapids.CommonMPEG1" pluginVersion="1.1.1.0" platform="Generic" checksum="27565032c30a0de4541b77262ca34a95"/>
            <plugin name="CommonMPEG2" buildDate="2016-07-18 18:22:19-0400" pluginId="ca.digitalrapids.CommonMPEG2" pluginVersion="1.6.0.0" platform="Generic" checksum="f3efbcef0a66f4b442b49f8bd9ef02ea"/>
            <plugin name="CommonMPEG4" buildDate="2015-11-16 22:23:54-0500" pluginId="ca.digitalrapids.CommonMPEG4" pluginVersion="1.0.12.0" platform="Generic" checksum="51aed3ea316ed781e6d86db70267c6c6"/>
            <plugin name="CommonMXF" buildDate="2015-11-16 22:48:52-0500" pluginId="ca.digitalrapids.CommonMXF" pluginVersion="1.0.4.0" platform="Generic" checksum="d6323db57110b4b505f4cf6afe1547ce"/>
            <plugin name="CommonMedia" buildDate="2016-07-18 15:25:05-0400" pluginId="ca.digitalrapids.CommonMedia" pluginVersion="1.25.0.0" platform="Windows" checksum="49e35ad34e1b4b1e5d511857b0b9f001"/>
            <plugin name="CommonMediaEncryption" buildDate="2014-06-27 17:24:40-0400" pluginId="ca.digitalrapids.CommonMediaEncryption" pluginVersion="1.0.7.0" platform="Generic" checksum="e8137cd8314ceb3134bd2569645d0cd2"/>
            <plugin name="CommonMetadata" buildDate="2015-11-16 22:30:54-0500" pluginId="ca.digitalrapids.CommonMetadata" pluginVersion="1.0.6.0" platform="Generic" checksum="5f854f13c4617c090c1d121b227d9d88"/>
            <plugin name="CommonPlayReadyEncryption" buildDate="2015-11-16 22:49:53-0500" pluginId="ca.digitalrapids.CommonPlayReadyEncryption" pluginVersion="1.0.6.0" platform="Generic" checksum="9e777b913c3bf72ce11bdbe43d2295b1"/>
            <plugin name="CommonQuickTime" buildDate="2015-11-16 22:50:40-0500" pluginId="ca.digitalrapids.CommonQuickTime" pluginVersion="1.0.4.0" platform="Generic" checksum="80363bbce7317baa24ab8f538f8b8f86"/>
            <plugin name="CommonSCC" buildDate="2015-07-14 12:49:10-0400" pluginId="ca.digitalrapids.CommonSCC" pluginVersion="1.1.1.0" platform="Generic" checksum="6a2f083148a9336012fa6ed2543a09ee"/>
            <plugin name="CommonStereoVideo" buildDate="2015-02-05 16:30:34-0500" pluginId="ca.digitalrapids.CommonStereoVideo" pluginVersion="1.1.1.0" platform="Generic" checksum="f8764aaec77fe5604228de00578cff0b"/>
            <plugin name="CommonSubtitles" buildDate="2016-04-28 15:20:56-0400" pluginId="ca.digitalrapids.CommonSubtitles" pluginVersion="1.1.1.0" platform="Generic" checksum="0caff5fcf2238a8660264ef63ddde63c"/>
            <plugin name="CommonTimecode" buildDate="2015-05-04 16:57:22-0400" pluginId="ca.digitalrapids.CommonTimecode" pluginVersion="1.2.3.0" platform="Generic" checksum="998e14fd976cbb310fdb42cf6bf0b0d6"/>
            <plugin name="CommonTimedText" buildDate="2014-11-11 19:16:43-0500" pluginId="ca.digitalrapids.CommonTimedText" pluginVersion="1.0.2.0" platform="Generic" checksum="bd7301c97546b6cd9e8a91df982358dd"/>
            <plugin name="CommonUltraviolet" buildDate="2014-03-25 11:15:24-0400" pluginId="ca.digitalrapids.CommonUltraviolet" pluginVersion="1.0.3.0" platform="Generic" checksum="0fbaf01cd371c81f4fcb7cf66f41f6d9"/>
            <plugin name="CommonVC3" buildDate="2015-02-05 16:30:50-0500" pluginId="ca.digitalrapids.CommonVC3" pluginVersion="1.1.1.0" platform="Generic" checksum="1fe7814f916437e8d32e470bc8c8ee7e"/>
            <plugin name="CommonVideoSystem" buildDate="2016-07-18 15:25:40-0400" pluginId="ca.digitalrapids.CommonVideoSystem" pluginVersion="2.5.0.0" platform="Windows" checksum="2d31c4fd9c9966ce8b9ecb772d79e309"/>
            <plugin name="CommonWAVE" buildDate="2015-02-05 12:17:51-0500" pluginId="ca.digitalrapids.CommonWAVE" pluginVersion="1.1.1.0" platform="Generic" checksum="0355d46d1c92231032fed50b5947275b"/>
            <plugin name="DRAVCEncoder" buildDate="2016-07-19 09:38:15-0400" pluginId="ca.digitalrapids.DRAVCEncoder" pluginVersion="1.5.8.0" platform="Windows" checksum="b09049069afe1f62ca644af42e454dc7"/>
            <plugin name="DRColorspaceConverter" buildDate="2016-06-17 10:51:30-0400" pluginId="ca.digitalrapids.DRColorspaceConverter" pluginVersion="1.3.1.0" platform="Windows" checksum="1bf0186f6b6444075de60ca017229f43"/>
            <plugin name="DRDeinterlacer" buildDate="2016-07-19 08:58:38-0400" pluginId="ca.digitalrapids.DRDeinterlacer" pluginVersion="1.6.1.0" platform="Windows" checksum="2b9014286167e9047fdc776a8738162b"/>
            <plugin name="DRMediaProcessing" buildDate="2016-06-17 10:51:18-0400" pluginId="ca.digitalrapids.DRMediaProcessing" pluginVersion="2.12.0.0" platform="Windows" checksum="5bd71c5e16cc2b6022d1811ab38765fc"/>
            <plugin name="DRProgressiveToInterlaced" buildDate="2016-07-19 08:59:52-0400" pluginId="ca.digitalrapids.DRProgressiveToInterlaced" pluginVersion="1.3.1.0" platform="Windows" checksum="163456f30b830b0b16eadba3bf35ea87"/>
            <plugin name="DRScaler" buildDate="2016-07-19 09:01:05-0400" pluginId="ca.digitalrapids.DRScaler" pluginVersion="1.4.1.0" platform="Windows" checksum="2e69858c61c65bafa07f3ea46c2e4eef"/>
            <plugin name="DRTemporalNoiseReduction" buildDate="2016-06-17 11:02:05-0400" pluginId="ca.digitalrapids.DRTemporalNoiseReduction" pluginVersion="1.3.1.0" platform="Windows" checksum="71632997e4d80383ffa6c2c43178d3b1"/>
            <plugin name="DTS Source Controller" buildDate="2016-06-17 11:02:48-0400" pluginId="ca.digitalrapids.DTSSourceController" pluginVersion="1.2.0.0" platform="Windows" checksum="1401f562cd33c266f818b6607b899472"/>
            <plugin name="DirectShowFileSource" buildDate="2016-06-17 10:55:41-0400" pluginId="ca.digitalrapids.DirectShowFileSource" pluginVersion="1.2.0.0" platform="Windows" checksum="4bd06a0c10c1a61997f17b9d832babbf"/>
            <plugin name="DolbyEDecoder" buildDate="2016-06-17 10:52:25-0400" pluginId="ca.digitalrapids.DolbyEDecoder" pluginVersion="1.3.0.0" platform="Windows" checksum="519e863e367415a6b8c9064f641adb3d"/>
            <plugin name="Dolby E Source Controller" buildDate="2016-06-17 10:52:38-0400" pluginId="ca.digitalrapids.DolbyESourceController" pluginVersion="1.3.0.0" platform="Windows" checksum="4b3ff602af6460aa6a32971e56d86497"/>
            <plugin name="DolbyPulseEncoder" buildDate="2016-07-19 09:34:36-0400" pluginId="ca.digitalrapids.DolbyPulseEncoder" pluginVersion="1.2.3.0" platform="Windows" checksum="7e691409edf2d2e157d22f41197843a1"/>
            <plugin name="EAC3 Source Controller" buildDate="2016-07-18 15:32:01-0400" pluginId="ca.digitalrapids.EAC3SourceController" pluginVersion="1.5.0.0" platform="Windows" checksum="83af8fc20d38d778cb9e7d295e086ae5"/>
            <plugin name="EIACaptionsRetimer" buildDate="2016-07-19 09:05:33-0400" pluginId="ca.digitalrapids.EIACaptionsRetimer" pluginVersion="1.7.1.0" platform="Windows" checksum="79c6e0195e02160b90826f549d66cea5"/>
            <plugin name="KayakCore" buildDate="2016-06-15 10:34:26-0400" pluginId="ca.digitalrapids.KayakCore" pluginVersion="1.7.2.0" platform="Windows" checksum="3a25fde0f48f6556e1638e2ad014c473"/>
            <plugin name="KayakDesigner" buildDate="2016-07-21 11:30:31-0400" pluginId="ca.digitalrapids.KayakDesigner" pluginVersion="1.7.2.0" platform="Generic" checksum="8075ed6297f64297d814b3fa602a19b1"/>
            <plugin name="MPEG2AudioSourceController" buildDate="2016-06-17 10:53:32-0400" pluginId="ca.digitalrapids.MPEG2AudioSourceController" pluginVersion="1.2.1.0" platform="Windows" checksum="167d4f2aef7d91ef3d123789f3f31824"/>
            <plugin name="MPEG2UDDemuxer" buildDate="2016-06-17 16:28:31-0400" pluginId="ca.digitalrapids.MPEG2UDDemuxer" pluginVersion="1.6.3.0" platform="Windows" checksum="d3700d7cc483a33f8fefc4a97b982590"/>
            <plugin name="MPEG2UDMuxer" buildDate="2016-07-19 09:46:59-0400" pluginId="ca.digitalrapids.MPEG2UDMuxer" pluginVersion="1.7.6.0" platform="Windows" checksum="d980760b82f1bd9e85886d64bd736a57"/>
            <plugin name="MPEG4Muxer" buildDate="2016-07-19 10:00:58-0400" pluginId="ca.digitalrapids.MPEG4Muxer" pluginVersion="1.11.4.0" platform="Windows" checksum="debeda332ec0977aa536233a2af8dc00"/>
            <plugin name="MXFDemuxer" buildDate="2016-06-17 11:03:25-0400" pluginId="ca.digitalrapids.MXFDemuxer" pluginVersion="1.9.1.0" platform="Windows" checksum="7478b8e1200d0f7de6e6287094305ff0"/>
            <plugin name="MediaInspection" buildDate="2016-07-21 11:08:48-0400" pluginId="ca.digitalrapids.MediaInspection" pluginVersion="1.10.2.0" platform="Generic" checksum="7eb2619cd3539f28bc0a52880e0fede3"/>
            <plugin name="Media Manager" buildDate="2016-02-04 16:36:07-0500" pluginId="ca.digitalrapids.MediaManager" pluginVersion="1.0.58.0" platform="Generic" checksum="c931da9a5e8f0dc43b83dd4c93d1ec25"/>
            <plugin name="Media Manager WS Client" buildDate="2015-06-02 13:58:30-0400" pluginId="ca.digitalrapids.MediaManagerWSClient" pluginVersion="1.0.10.0" platform="Generic" checksum="8d95df55931735bcd0069c19b36732f2"/>
            <plugin name="RasterFlip" buildDate="2016-06-17 11:05:50-0400" pluginId="ca.digitalrapids.RasterFlip" pluginVersion="1.1.1.0" platform="Windows" checksum="5d36826401fe4f818aa4b2910fcfb3a7"/>
            <plugin name="SCCSourceController" buildDate="2016-06-17 10:59:51-0400" pluginId="ca.digitalrapids.SCCSourceController" pluginVersion="1.9.0.0" platform="Windows" checksum="917e30e9a890205c673183be4d5a690a"/>
            <plugin name="SMPTE291Demuxer" buildDate="2016-06-17 11:04:36-0400" pluginId="ca.digitalrapids.SMPTE291Demuxer" pluginVersion="1.14.1.0" platform="Windows" checksum="3d96bdb4e0c456a3a78d7d76c54c0bb4"/>
            <plugin name="StreamSynchronizers" buildDate="2016-07-19 09:51:07-0400" pluginId="ca.digitalrapids.StreamSynchronizers" pluginVersion="1.9.1.0" platform="Windows" checksum="541dc771547edcd3429234dfdc8bcb6a"/>
            <plugin name="TimecodeEncoder" buildDate="2016-07-19 10:25:30-0400" pluginId="ca.digitalrapids.TimecodeEncoder" pluginVersion="1.4.1.0" platform="Windows" checksum="394c5e1c224d69e97501de600ba89b3f"/>
            <plugin name="VC3Decoder" buildDate="2016-06-17 11:06:58-0400" pluginId="ca.digitalrapids.VC3Decoder" pluginVersion="1.4.1.0" platform="Windows" checksum="5d68a8654e05630bd92325a828c0722d"/>
            <plugin name="VC3SourceController" buildDate="2015-11-17 00:08:23-0500" pluginId="ca.digitalrapids.VC3SourceController" pluginVersion="1.1.3.0" platform="Windows" checksum="905150bbe3155b95693646110e046220"/>
            <plugin name="VideoBitDepthConverters" buildDate="2016-06-17 11:07:19-0400" pluginId="ca.digitalrapids.VideoBitDepthConverters" pluginVersion="1.1.1.0" platform="Windows" checksum="986609b2ae2596abb7cf4e8575b5a93d"/>
            <plugin name="VideoBorder" buildDate="2016-07-19 10:30:00-0400" pluginId="ca.digitalrapids.VideoBorder" pluginVersion="1.2.2.0" platform="Windows" checksum="b9a1934d651afb1716e6cafc61f807e1"/>
            <plugin name="VideoDataLayoutConverter" buildDate="2016-06-17 10:51:45-0400" pluginId="ca.digitalrapids.VideoDataLayoutConverter" pluginVersion="1.3.1.0" platform="Windows" checksum="2f391a190d99ca922320851eea22682b"/>
            <plugin name="VideoDeinterlacers" buildDate="2016-06-17 10:57:39-0400" pluginId="ca.digitalrapids.VideoDeinterlacers" pluginVersion="1.1.1.0" platform="Windows" checksum="3ad9ecb616a3be18a3a4d01b2daaf9e5"/>
            <plugin name="VideoFormatConverter" buildDate="2016-07-19 09:53:23-0400" pluginId="ca.digitalrapids.VideoFormatConverter" pluginVersion="1.1.3.0" platform="Generic" checksum="e9dd77b39e59bd8ef372fb5ff1d67301"/>
            <plugin name="VideoFormatConverter2" buildDate="2016-06-20 11:53:40-0400" pluginId="ca.digitalrapids.VideoFormatConverter2" pluginVersion="1.9.2.0" platform="Windows" checksum="4204d607d30238923decf2ace973ad3d"/>
            <plugin name="VideoFormatUtilities" buildDate="2016-07-19 10:31:54-0400" pluginId="ca.digitalrapids.VideoFormatUtilities" pluginVersion="1.4.3.0" platform="Windows" checksum="f83b75744449fbf71518d21ca551b11f"/>
            <plugin name="VideoFrameLayout" buildDate="2016-06-17 10:55:54-0400" pluginId="ca.digitalrapids.VideoFrameLayout" pluginVersion="1.2.1.0" platform="Windows" checksum="2fa0f73ca7a0a59c6b2e137c9607dc90"/>
            <plugin name="VideoProcessor" buildDate="2016-07-19 10:33:15-0400" pluginId="ca.digitalrapids.VideoProcessor" pluginVersion="1.1.7.0" platform="Windows" checksum="e533cd0909a896212328c77f778bcf1e"/>
            <plugin name="VideoPulldownProcessor" buildDate="2016-06-17 11:08:16-0400" pluginId="ca.digitalrapids.VideoPulldownProcessor" pluginVersion="1.3.1.0" platform="Windows" checksum="92288c0f59fd87bcf5d9caeaea5f1f4c"/>
            <plugin name="WAVESourceController" buildDate="2016-05-11 16:58:56-0400" pluginId="ca.digitalrapids.WAVESourceController" pluginVersion="1.2.2.0" platform="Generic" checksum="6106b2582a46456d161c62fa27240935"/>
            <plugin name="AFDUtilities" buildDate="2016-06-17 10:54:10-0400" pluginId="com.imaginecommunications.AFDUtilities" pluginVersion="1.4.1.0" platform="Windows" checksum="5e21d9d7a0d48b09477de6aca530113c"/>
            <plugin name="AMSManifestWriter" buildDate="2016-07-20 17:26:44-0400" pluginId="com.imaginecommunications.AMSManifestWriter" pluginVersion="1.1.3.0" platform="Generic" checksum="b8d9902a5d87b4bbc274e8032a174d5a"/>
            <plugin name="CommonDolbyMetadata" buildDate="2015-10-02 09:54:15-0400" pluginId="com.imaginecommunications.CommonDolbyMetadata" pluginVersion="1.0.0.0" platform="Generic" checksum="ab33d049bb8a7480d62aedba69db0c15"/>
            <plugin name="CommonID3Metadata" buildDate="2015-09-30 11:17:43-0400" pluginId="com.imaginecommunications.CommonID3Metadata" pluginVersion="1.0.1.0" platform="Generic" checksum="55e43a54d3ea63901a8037d504599374"/>
            <plugin name="CommonMediaOrigin" buildDate="2016-06-17 10:55:08-0400" pluginId="com.imaginecommunications.CommonMediaOrigin" pluginVersion="1.7.0.0" platform="Windows" checksum="d18843513a64d7df40ce6d2f85a65010"/>
            <plugin name="CommonProRes" buildDate="2016-06-24 17:36:19-0400" pluginId="com.imaginecommunications.CommonProRes" pluginVersion="1.1.1.0" platform="Generic" checksum="df55b51242ef5bb3086a7a6470ab9d44"/>
            <plugin name="CommonSourceController" buildDate="2016-06-20 16:32:34-0400" pluginId="com.imaginecommunications.CommonSourceController" pluginVersion="1.1.2.0" platform="Windows" checksum="5f4a9884670c14887c3cb77cb7d14a41"/>
            <plugin name="CommonVideoEncoder" buildDate="2016-04-13 17:00:26-0400" pluginId="com.imaginecommunications.CommonVideoEncoder" pluginVersion="1.2.2.0" platform="Windows" checksum="3df16254d3ac256921afcf19fae3cda6"/>
            <plugin name="CuePointUtilities" buildDate="2016-07-12 14:19:17-0400" pluginId="com.imaginecommunications.CuePointUtilities" pluginVersion="1.3.6.0" platform="Windows" checksum="34096e1fe3ffcdcc4fadcf1e27aa45a3"/>
            <plugin name="EIACaptionsConformer" buildDate="2016-06-17 10:55:24-0400" pluginId="com.imaginecommunications.EIACaptionsConformer" pluginVersion="1.3.1.0" platform="Windows" checksum="b6bf5fbbd2e692bfbd32e8f355577466"/>
            <plugin name="StreamFillers" buildDate="2016-06-17 10:51:56-0400" pluginId="com.imaginecommunications.StreamFillers" pluginVersion="1.8.1.0" platform="Windows" checksum="9a26aea2ee78c53a076021667d7d4b40"/>
            <plugin name="TimecodeRetimer" buildDate="2015-08-06 15:28:41-0400" pluginId="com.imaginecommunications.TimecodeRetimer" pluginVersion="1.2.0.0" platform="Generic" checksum="7d974c1cb68ddef56d4b2d144ab93526"/>
        </pluginIdentifiers>
    </authoringInfo>
    <dependencyInfo>
        <plugins>
            <plugin pluginId="ca.digitalrapids.AACSourceController" name="AAC Source Controller"/>
            <plugin pluginId="ca.digitalrapids.AC3SourceController" name="AC3 Source Controller"/>
            <plugin pluginId="ca.digitalrapids.AES3AudioProcessor" name="AES3AudioProcessor"/>
            <plugin pluginId="ca.digitalrapids.AVCSourceController" name="AVC Source Controller"/>
            <plugin pluginId="ca.digitalrapids.ActiveImageCrop" name="ActiveImageCrop"/>
            <plugin pluginId="ca.digitalrapids.AlphaChannelUtilities" name="AlphaChannelUtilities"/>
            <plugin pluginId="ca.digitalrapids.Assets" name="Assets"/>
            <plugin pluginId="ca.digitalrapids.AudioFormatConverter" name="AudioFormatConverter"/>
            <plugin pluginId="ca.digitalrapids.AudioFormatUtilities" name="AudioFormatUtilities"/>
            <plugin pluginId="ca.digitalrapids.ChromaResampler" name="ChromaResampler"/>
            <plugin pluginId="ca.digitalrapids.ClosedCaptionsUtilities" name="ClosedCaptionsUtilities"/>
            <plugin pluginId="ca.digitalrapids.CommonAAC" name="CommonAAC"/>
            <plugin pluginId="ca.digitalrapids.CommonAC3" name="CommonAC3"/>
            <plugin pluginId="ca.digitalrapids.CommonAES3" name="CommonAES3"/>
            <plugin pluginId="ca.digitalrapids.CommonAVC" name="CommonAVC"/>
            <plugin pluginId="ca.digitalrapids.CommonAVCEncoder" name="CommonAVCEncoder"/>
            <plugin pluginId="ca.digitalrapids.CommonComponents" name="CommonComponents"/>
            <plugin pluginId="ca.digitalrapids.CommonCuePoint" name="CommonCuePoint"/>
            <plugin pluginId="ca.digitalrapids.CommonDTS" name="CommonDTS"/>
            <plugin pluginId="ca.digitalrapids.CommonDV" name="CommonDV"/>
            <plugin pluginId="ca.digitalrapids.CommonDolbyE" name="CommonDolbyE"/>
            <plugin pluginId="ca.digitalrapids.CommonEAC3" name="CommonEAC3"/>
            <plugin pluginId="ca.digitalrapids.CommonFont" name="CommonFont"/>
            <plugin pluginId="ca.digitalrapids.CommonImageFormats" name="CommonImageFormats"/>
            <plugin pluginId="ca.digitalrapids.CommonIntelIPP" name="CommonIntelIPP"/>
            <plugin pluginId="ca.digitalrapids.CommonJ2KVideo" name="CommonJ2KVideo"/>
            <plugin pluginId="ca.digitalrapids.CommonLanguage" name="CommonLanguage"/>
            <plugin pluginId="ca.digitalrapids.CommonMPEG" name="CommonMPEG"/>
            <plugin pluginId="ca.digitalrapids.CommonMPEG1" name="CommonMPEG1"/>
            <plugin pluginId="ca.digitalrapids.CommonMPEG2" name="CommonMPEG2"/>
            <plugin pluginId="ca.digitalrapids.CommonMPEG4" name="CommonMPEG4"/>
            <plugin pluginId="ca.digitalrapids.CommonMXF" name="CommonMXF"/>
            <plugin pluginId="ca.digitalrapids.CommonMedia" name="CommonMedia"/>
            <plugin pluginId="ca.digitalrapids.CommonMediaEncryption" name="CommonMediaEncryption"/>
            <plugin pluginId="ca.digitalrapids.CommonMetadata" name="CommonMetadata"/>
            <plugin pluginId="ca.digitalrapids.CommonPlayReadyEncryption" name="CommonPlayReadyEncryption"/>
            <plugin pluginId="ca.digitalrapids.CommonQuickTime" name="CommonQuickTime"/>
            <plugin pluginId="ca.digitalrapids.CommonSCC" name="CommonSCC"/>
            <plugin pluginId="ca.digitalrapids.CommonStereoVideo" name="CommonStereoVideo"/>
            <plugin pluginId="ca.digitalrapids.CommonSubtitles" name="CommonSubtitles"/>
            <plugin pluginId="ca.digitalrapids.CommonTimecode" name="CommonTimecode"/>
            <plugin pluginId="ca.digitalrapids.CommonTimedText" name="CommonTimedText"/>
            <plugin pluginId="ca.digitalrapids.CommonUltraviolet" name="CommonUltraviolet"/>
            <plugin pluginId="ca.digitalrapids.CommonVC3" name="CommonVC3"/>
            <plugin pluginId="ca.digitalrapids.CommonVideoSystem" name="CommonVideoSystem"/>
            <plugin pluginId="ca.digitalrapids.CommonWAVE" name="CommonWAVE"/>
            <plugin pluginId="ca.digitalrapids.DRAVCEncoder" name="DRAVCEncoder"/>
            <plugin pluginId="ca.digitalrapids.DRColorspaceConverter" name="DRColorspaceConverter"/>
            <plugin pluginId="ca.digitalrapids.DRDeinterlacer" name="DRDeinterlacer"/>
            <plugin pluginId="ca.digitalrapids.DRMediaProcessing" name="DRMediaProcessing"/>
            <plugin pluginId="ca.digitalrapids.DRProgressiveToInterlaced" name="DRProgressiveToInterlaced"/>
            <plugin pluginId="ca.digitalrapids.DRScaler" name="DRScaler"/>
            <plugin pluginId="ca.digitalrapids.DRTemporalNoiseReduction" name="DRTemporalNoiseReduction"/>
            <plugin pluginId="ca.digitalrapids.DTSSourceController" name="DTS Source Controller"/>
            <plugin pluginId="ca.digitalrapids.DirectShowFileSource" name="DirectShowFileSource"/>
            <plugin pluginId="ca.digitalrapids.DolbyEDecoder" name="DolbyEDecoder"/>
            <plugin pluginId="ca.digitalrapids.DolbyESourceController" name="Dolby E Source Controller"/>
            <plugin pluginId="ca.digitalrapids.DolbyPulseEncoder" name="DolbyPulseEncoder"/>
            <plugin pluginId="ca.digitalrapids.EAC3SourceController" name="EAC3 Source Controller"/>
            <plugin pluginId="ca.digitalrapids.EIACaptionsRetimer" name="EIACaptionsRetimer"/>
            <plugin pluginId="ca.digitalrapids.KayakCore" name="KayakCore"/>
            <plugin pluginId="ca.digitalrapids.KayakDesigner" name="KayakDesigner"/>
            <plugin pluginId="ca.digitalrapids.MPEG2AudioSourceController" name="MPEG2AudioSourceController"/>
            <plugin pluginId="ca.digitalrapids.MPEG2UDDemuxer" name="MPEG2UDDemuxer"/>
            <plugin pluginId="ca.digitalrapids.MPEG2UDMuxer" name="MPEG2UDMuxer"/>
            <plugin pluginId="ca.digitalrapids.MPEG4Muxer" name="MPEG4Muxer"/>
            <plugin pluginId="ca.digitalrapids.MXFDemuxer" name="MXFDemuxer"/>
            <plugin pluginId="ca.digitalrapids.MediaInspection" name="MediaInspection"/>
            <plugin pluginId="ca.digitalrapids.MediaManager" name="Media Manager"/>
            <plugin pluginId="ca.digitalrapids.MediaManagerWSClient" name="Media Manager WS Client"/>
            <plugin pluginId="ca.digitalrapids.RasterFlip" name="RasterFlip"/>
            <plugin pluginId="ca.digitalrapids.SCCSourceController" name="SCCSourceController"/>
            <plugin pluginId="ca.digitalrapids.SMPTE291Demuxer" name="SMPTE291Demuxer"/>
            <plugin pluginId="ca.digitalrapids.StreamSynchronizers" name="StreamSynchronizers"/>
            <plugin pluginId="ca.digitalrapids.TimecodeEncoder" name="TimecodeEncoder"/>
            <plugin pluginId="ca.digitalrapids.VC3Decoder" name="VC3Decoder"/>
            <plugin pluginId="ca.digitalrapids.VC3SourceController" name="VC3SourceController"/>
            <plugin pluginId="ca.digitalrapids.VideoBitDepthConverters" name="VideoBitDepthConverters"/>
            <plugin pluginId="ca.digitalrapids.VideoBorder" name="VideoBorder"/>
            <plugin pluginId="ca.digitalrapids.VideoDataLayoutConverter" name="VideoDataLayoutConverter"/>
            <plugin pluginId="ca.digitalrapids.VideoDeinterlacers" name="VideoDeinterlacers"/>
            <plugin pluginId="ca.digitalrapids.VideoFormatConverter" name="VideoFormatConverter"/>
            <plugin pluginId="ca.digitalrapids.VideoFormatConverter2" name="VideoFormatConverter2"/>
            <plugin pluginId="ca.digitalrapids.VideoFormatUtilities" name="VideoFormatUtilities"/>
            <plugin pluginId="ca.digitalrapids.VideoFrameLayout" name="VideoFrameLayout"/>
            <plugin pluginId="ca.digitalrapids.VideoProcessor" name="VideoProcessor"/>
            <plugin pluginId="ca.digitalrapids.VideoPulldownProcessor" name="VideoPulldownProcessor"/>
            <plugin pluginId="ca.digitalrapids.WAVESourceController" name="WAVESourceController"/>
            <plugin pluginId="com.imaginecommunications.AFDUtilities" name="AFDUtilities"/>
            <plugin pluginId="com.imaginecommunications.AMSManifestWriter" name="AMSManifestWriter"/>
            <plugin pluginId="com.imaginecommunications.CommonDolbyMetadata" name="CommonDolbyMetadata"/>
            <plugin pluginId="com.imaginecommunications.CommonID3Metadata" name="CommonID3Metadata"/>
            <plugin pluginId="com.imaginecommunications.CommonMediaOrigin" name="CommonMediaOrigin"/>
            <plugin pluginId="com.imaginecommunications.CommonProRes" name="CommonProRes"/>
            <plugin pluginId="com.imaginecommunications.CommonSourceController" name="CommonSourceController"/>
            <plugin pluginId="com.imaginecommunications.CommonVideoEncoder" name="CommonVideoEncoder"/>
            <plugin pluginId="com.imaginecommunications.CuePointUtilities" name="CuePointUtilities"/>
            <plugin pluginId="com.imaginecommunications.EIACaptionsConformer" name="EIACaptionsConformer"/>
            <plugin pluginId="com.imaginecommunications.StreamFillers" name="StreamFillers"/>
            <plugin pluginId="com.imaginecommunications.TimecodeRetimer" name="TimecodeRetimer"/>
        </plugins>
        <components>
            <component pluginId="ca.digitalrapids.AACSourceController" name="AAC ADTS to Raw Converter" guid="eed0ba59-346f-47b0-ba9a-2ea14be6fa53"/>
            <component pluginId="ca.digitalrapids.AACSourceController" name="AAC Controller" guid="784ee2cc-8a15-41c9-b84b-1a79ced4a646"/>
            <component pluginId="ca.digitalrapids.DolbyPulseEncoder" name="AAC Encoder - Dolby Pulse" displayName="AAC Encoder (Dolby)" guid="D0933A55-4818-4ADC-9301-8BE7687AC9E3"/>
            <component pluginId="ca.digitalrapids.DolbyPulseEncoder" name="Atomic AAC Encoder - Dolby Pulse" displayName="AAC Encoder Core (Dolby)" guid="8916cfea-3397-4310-b5bb-402e27fb0baf"/>
            <component pluginId="ca.digitalrapids.ActiveImageCrop" name="Active Image Crop" guid="695BC53B-922F-4af1-88D1-6B33A3D05599"/>
            <component pluginId="ca.digitalrapids.AlphaChannelUtilities" name="Alpha Channel Adder" guid="22444100-318E-4379-9CC1-6DD8A74135A2"/>
            <component pluginId="ca.digitalrapids.AlphaChannelUtilities" name="Alpha Channel Remover" guid="92E5AC2E-9CC7-422F-B947-DC39E077D9CB"/>
            <component pluginId="com.imaginecommunications.AMSManifestWriter" name="AMSManifestWriter" displayName="AMS Manifest Writer" guid="3780304E-D2B1-4AA6-B109-893B4866DE5E"/>
            <component pluginId="ca.digitalrapids.SMPTE291Demuxer" name="SMPTE291 Demultiplexer" displayName="Ancillary Data Demultiplexer" guid="DF2C29B3-52A5-4786-8C1D-3AE207D252D4"/>
            <component pluginId="ca.digitalrapids.AES3AudioProcessor" name="Audio Container Data Type Merger" guid="3AED3909-CEC8-4413-BF58-33FA08514D0C"/>
            <component pluginId="ca.digitalrapids.AudioFormatConverter" name="Audio Format Converter" displayName="Audio Format Converter (Deprecated)" guid="F2A4515C-ABD5-49f9-B0D5-DB462E4BB674"/>
            <component pluginId="ca.digitalrapids.AVCSourceController" name="AVC Part10 to Part15 Converter" guid="b2eC0208-f841-4272-8a16-4b88e80d86a5"/>
            <component pluginId="ca.digitalrapids.DRAVCEncoder" name="AVC Video Encoder" guid="43e72a55-1e8f-4827-a6e3-3217b07ba7e9"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Bit Depth Converter" guid="7DF81BC0-6DFD-44fd-BDAA-2E568F65CFF6"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Channel Mapper" displayName="Channel Remapper" guid="771ACEB1-E611-4803-A356-21F221E3753D"/>
            <component pluginId="ca.digitalrapids.ChromaResampler" name="Chroma Resampler" guid="96051CC1-65A8-4574-944E-8427D0598427"/>
            <component pluginId="ca.digitalrapids.VideoFormatUtilities" name="Color Space Standard Updater" guid="28825610-F32A-4b0b-B353-A368AA05B393"/>
            <component pluginId="ca.digitalrapids.DRColorspaceConverter" name="DRColorspaceConverter" displayName="Colorspace Converter" guid="A1F022EA-A1BF-446b-B1E2-C1DFEA43F29E"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Container Data Type Merger" guid="b6eac4c1-3c04-4f8d-9654-96da605b9e90"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="Data Type Merger" guid="4971c1a4-07ab-4c9a-93a6-947526a1005d"/>
            <component pluginId="ca.digitalrapids.DRDeinterlacer" name="DRDeinterlacer" displayName="Deinterlacer" guid="750D51F3-FC19-410f-89AB-B7F3E8CAFEDC"/>
            <component pluginId="ca.digitalrapids.VC3SourceController" name="VC3 Controller" displayName="DNxHD Controller" guid="8C6E027F-C1F2-4965-89D5-952E3672BA79"/>
            <component pluginId="ca.digitalrapids.VC3Decoder" name="VC3 OOP Decoder" displayName="DNxHD Decoder" guid="CA8185FD-EDAF-44B7-B4AF-2EC30FBAC347"/>
            <component pluginId="ca.digitalrapids.VC3Decoder" name="VC3 Decoder" displayName="DNxHD Decoder Core (x86)" guid="852562D8-71CC-4121-A3F6-FD0DE1B56D3F"/>
            <component pluginId="ca.digitalrapids.VC3SourceController" name="VC3 Media Inspector" displayName="DNxHD Media Inspector" guid="0EF180D6-468A-4255-A122-25BCD2CC1CDB"/>
            <component pluginId="ca.digitalrapids.ClosedCaptionsUtilities" name="EIA-708 to 608 Converter" displayName="EIA-708 to 608 De-Embedder" guid="57CA8716-84CF-4C7F-B59F-DF34AFE2E73E"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="EndOfStreamNotification" displayName="End of Stream Notification" guid="285BF6A1-3FEA-4c2a-9D2D-4DB4B965C3EA"/>
            <component pluginId="ca.digitalrapids.CommonMedia" name="Endian Converter" guid="D076A34F-6E7D-46BD-875A-4C590B5538BF"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="File Output" guid="9b376163-de8d-4e09-8bed-353725b6b6d6"/>
            <component pluginId="ca.digitalrapids.VideoFormatUtilities" name="Frame Rate Converter" guid="3a36bec1-8b92-442b-92c4-8a8f908a6cd5"/>
            <component pluginId="ca.digitalrapids.VideoBorder" name="Image Border Core 2" displayName="Image Border Core" guid="165205D3-C849-4651-82BF-97EF31CA0827"/>
            <component pluginId="ca.digitalrapids.VideoBorder" name="Image Crop Core 2" displayName="Image Crop Core" guid="32DA642E-168F-4db7-A33C-CB4D395ED2B8"/>
            <component pluginId="ca.digitalrapids.MPEG4Muxer" name="Advanced ISO MPEG4 Multiplexer" displayName="ISO MPEG-4 Multiplexer" guid="E25468C3-A65C-4f1a-8172-E72CE4B63A70"/>
            <component pluginId="ca.digitalrapids.MPEG4Muxer" name="ISO MPEG4 Multiplexer" displayName="ISO MPEG-4 Multiplexer Core" guid="3CC47644-DC6D-4f2b-AB3B-580D305F47CC"/>
            <component pluginId="ca.digitalrapids.CommonLanguage" name="Language Code Updater" guid="563232cc-20ba-453f-8f69-43284cea7abc"/>
            <component pluginId="ca.digitalrapids.CommonMedia" name="Media Data Type Auto Updater" guid="9dc80c38-b4ff-4b3e-8324-2f29abeb461e"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Media File Input" guid="7cec6ecd-a477-4834-bc6f-97e34aa58bb5"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Media Inpection Data Type Merger" guid="A025A4BD-A59D-42e4-B00C-66F67BCB147C"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Media Inspector" guid="3ada68f0-f492-4133-87e2-cdb55ae9f7fc"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Multi-Program Audio Splitter" displayName="Multi-Program Audio Splitter" guid="6436a63f-1fa4-40e6-ba86-95138130d456"/>
            <component pluginId="ca.digitalrapids.MXFDemuxer" name="MXF Demultiplexer" guid="1A6B42BD-8131-41c3-8E51-361AB75A08B5"/>
            <component pluginId="ca.digitalrapids.MXFDemuxer" name="MXF Media Inspector" guid="C1261FE2-7506-4e7d-A6D7-5F576F723B2A"/>
            <component pluginId="ca.digitalrapids.DRProgressiveToInterlaced" name="DRProgressiveToInterlaced" displayName="Progressive To Interlaced" guid="05EE19E6-624C-4a64-8540-2AB31682B357"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="Random Access File" guid="ef0bd6fd-7564-4efb-bb78-a184bce33a29"/>
            <component pluginId="ca.digitalrapids.RasterFlip" name="Raster Orientation Inverter" guid="8d5132cd-c826-412e-9d57-51d0cc5d8166"/>
            <component pluginId="ca.digitalrapids.AES3AudioProcessor" name="Raw Audio Data Type Merger" guid="08D76F09-6818-4214-B4CF-0E7591556ADE"/>
            <component pluginId="ca.digitalrapids.AES3AudioProcessor" name="Raw Audio Media Inspector" guid="F16BE80D-2AAB-4126-8820-1E05F64FB99D"/>
            <component pluginId="ca.digitalrapids.AES3AudioProcessor" name="Raw Audio Processor" guid="9C0D7AA4-45A5-4561-B6EB-BCA2E0D4856F"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Sample Rate Converter" guid="0DAC861A-FDD8-4e0c-97BB-3341C4E46999"/>
            <component pluginId="ca.digitalrapids.DRScaler" name="DRScaler" displayName="Scaler" guid="2EA57BB6-D100-4eaf-8DE0-1739BD64B833"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="Scripted Component" guid="2c5d7c09-9db8-4bb5-9dab-b2682268e2be"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Speaker Position Assigner" guid="AB851938-A3DA-4062-9F4A-FB8AF260D887"/>
            <component pluginId="ca.digitalrapids.StreamSynchronizers" name="Stream Trimmer" guid="4EDCEFA6-93DE-463f-8C6B-543ED2CFCA77"/>
            <component pluginId="ca.digitalrapids.StreamSynchronizers" name="Stream Truncator" guid="3B8118A9-72E6-42b7-910A-014D9E8C1575"/>
            <component pluginId="ca.digitalrapids.DRTemporalNoiseReduction" name="DRTemporalNoiseReduction" displayName="Temporal Noise Reduction" guid="2568D4B4-6B99-424e-A249-9CCFD59C62DA"/>
            <component pluginId="ca.digitalrapids.MediaManager" name="Transcode Task Graph" displayName="Transcode Blueprint" guid="cc2f8f8a-85a3-4522-85a5-b0b26b12f4cd"/>
            <component pluginId="ca.digitalrapids.StreamSynchronizers" name="Trimming Validator" guid="6D3E7814-6954-4e57-BF9C-AC843726A621"/>
            <component pluginId="ca.digitalrapids.VideoFormatConverter2" name="VFCDurationRegulator" displayName="VFC Duration Regulator" guid="C54DD038-5161-4503-B1EA-D3CEB4F5914E"/>
            <component pluginId="ca.digitalrapids.VideoBitDepthConverters" name="Video Bit Depth Down Converter" guid="C5993C0E-C1C2-4765-9AD7-F87F832E3D3D"/>
            <component pluginId="ca.digitalrapids.VideoBitDepthConverters" name="Video Bit Depth Up Converter" guid="0581F7F4-149F-4241-B633-A634C49D0661"/>
            <component pluginId="ca.digitalrapids.VideoDataLayoutConverter" name="Video Data Layout Converter" guid="37B6A99C-7EBE-4b7a-99C0-6B872532802D"/>
            <component pluginId="ca.digitalrapids.VideoFormatConverter2" name="VideoFormatConverter2" displayName="Video Format Converter" guid="044A97C7-A980-433e-83FF-FC15067F627F"/>
            <component pluginId="ca.digitalrapids.VideoPulldownProcessor" name="Video Pulldown Processor" guid="B0BBD4A8-2E48-42F7-BF58-AB2660B27D01"/>
            <component pluginId="ca.digitalrapids.WAVESourceController" name="WAVE Controller" guid="8c0acacd-ac08-4a85-842f-04e62dab8c6f"/>
            <component pluginId="ca.digitalrapids.WAVESourceController" name="WAVE Media Inspector" guid="821df281-ee31-4ee2-bd32-536ee0d6e034"/>
            <component pluginId="ca.digitalrapids.KayakCore" name="Kayak Graph" displayName="Zenium Graph" guid="abc785f2-427e-4522-ba00-f3cb6acd1220"/>
        </components>
    </dependencyInfo>
</kayakDocument>
