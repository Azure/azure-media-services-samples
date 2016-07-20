<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<kayakDocument version="1.2" xml:space="preserve">
    <components>
        <component>
            <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
            <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
            <property name="_graphDisplayContents" isNull="true"/>
            <property name="_graphMinDisplaySize" isNull="true"/>
            <property name="_logDebugInfoOnError" isNull="true"/>
            <property name="_timeBase_local" isNull="true"/>
            <property name="acquireChildLicenses" isNull="true"/>
            <property name="assetPieceNoMetadata" isNull="true"/>
            <property name="clipListXml">&lt;?xml version="1.0" encoding="UTF-8" standalone="yes"?&gt;
&lt;clipList&gt;
    &lt;clip&gt;
        &lt;videoSource&gt;
            &lt;mediaFile&gt;
                &lt;file&gt;C:\Users\xpouyat\Videos\Microsoft_HoloLens_Possibilities_816p24.mp4&lt;/file&gt;
            &lt;/mediaFile&gt;
        &lt;/videoSource&gt;
        &lt;audioSource&gt;
            &lt;mediaFile&gt;
                &lt;file&gt;C:\Users\xpouyat\Videos\Microsoft_HoloLens_Possibilities_816p24.mp4&lt;/file&gt;
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
            <property name="ignoreChildComponentErrors" isNull="true"/>
            <property name="ignoreParentGraphState" isNull="true"/>
            <property name="inactiveTimeout">60</property>
            <property name="logsMaxEntries" isNull="true"/>
            <property name="monitorProgress">true</property>
            <property name="outputWriteDirectory">C:\Users\xpouyat\Videos\test</property>
            <property name="primarySourceFile">C:\Users\xpouyat\Videos\Microsoft_HoloLens_Possibilities_816p24.mp4</property>
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
                    <propertyDefinition name="MPEG4 Demultiplexer.H.264 format" description="Specifies the output format for H.264 tracks" dynamic="true">
                        <valueType type="STRING">
                            <valueRestriction strictEnum="true">
                                <enumerationValue val="RAW" displayName="Raw"></enumerationValue>
                                <enumerationValue val="CANONICAL" displayName="Canonical"></enumerationValue>
                            </valueRestriction>
                        </valueType>
                    </propertyDefinition>
                    <propertyDefinition name="MPEG4 Demultiplexer.AAC format" description="Specifies the output format for AAC tracks" dynamic="true">
                        <valueType type="STRING">
                            <valueRestriction strictEnum="true">
                                <enumerationValue val="RAW" displayName="Raw"></enumerationValue>
                                <enumerationValue val="MPEG2 ADTS HEADER" displayName="MPEG2 ADTS Header"></enumerationValue>
                                <enumerationValue val="MPEG4 ADTS HEADER" displayName="MPEG4 ADTS Header"></enumerationValue>
                            </valueRestriction>
                        </valueType>
                    </propertyDefinition>
                    <propertyDefinition advanced="true" name="H.264 (AVC)Decoder.mt.mode" description="Enable/Disable multithreading.  In Enabled (Auto) mode, the number of threads will be set to the number of CPUs." dynamic="true">
                        <initialValue>2</initialValue>
                        <valueType type="INTEGER">
                            <valueRestriction strictEnum="true">
                                <enumerationValue val="0" displayName="Disabled"></enumerationValue>
                                <enumerationValue val="1" displayName="Enabled"></enumerationValue>
                                <enumerationValue val="2" displayName="Enabled (Auto)"></enumerationValue>
                            </valueRestriction>
                        </valueType>
                    </propertyDefinition>
                    <propertyDefinition advanced="true" name="H.264 (AVC)Decoder.mt.num_threads" description="Number of worker threads to run." dynamic="true">
                        <initialValue>8</initialValue>
                        <valueType type="INTEGER">
                            <valueRestriction minValue="1" maxValue="64"/>
                        </valueType>
                    </propertyDefinition>
                    <propertyDefinition advanced="true" name="H.264 (AVC)Decoder.mt.num_nal_units" description="Maximum number of nal units in decoder core buffer" dynamic="true">
                        <initialValue>0</initialValue>
                        <valueType type="INTEGER"/>
                    </propertyDefinition>
                    <propertyDefinition advanced="true" name="H.264 (AVC)Decoder.mt.max_picts_in_parallel" description="Maximum number of pictures to decode in parallel" dynamic="true">
                        <initialValue>0</initialValue>
                        <valueType type="INTEGER"/>
                    </propertyDefinition>
                    <property name="Decode_audio">true</property>
                    <property name="Decode_captions_AFD">true</property>
                    <property name="EndTime" isNull="true"/>
                    <property name="EndTimecode" isNull="true"/>
                    <property name="H.264 (AVC)Decoder.mt.max_picts_in_parallel">0</property>
                    <property name="H.264 (AVC)Decoder.mt.mode">2</property>
                    <property name="H.264 (AVC)Decoder.mt.num_nal_units">0</property>
                    <property name="H.264 (AVC)Decoder.mt.num_threads">8</property>
                    <property name="MPEG4 Demultiplexer.AAC format">MPEG4 ADTS HEADER</property>
                    <property name="MPEG4 Demultiplexer.H.264 format">RAW</property>
                    <property name="StartTime" isNull="true"/>
                    <property name="StartTimecode" isNull="true"/>
                    <property name="TargetFrameRate" isNull="true"/>
                    <property name="TrimmingMode">Timestamp</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">161.99998474121094,21.999998092651367</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="always_use_directshow">false</property>
                    <property name="blackThreshold">0.10</property>
                    <property name="black_border_detection">false</property>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="enable_directshow">false</property>
                    <property name="filename">C:\Users\xpouyat\Videos\Microsoft_HoloLens_Possibilities_816p24.mp4</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="inspection_max_megabytes" isNull="true"/>
                    <property name="inspection_max_seconds" isNull="true"/>
                    <property name="inspection_mode" isNull="true"/>
                    <property name="logFile" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="noiseThreshold">0.10</property>
                    <property name="probeDuration">60.0</property>
                    <property name="probeRate">0.10</property>
                    <property name="probeTimeInterval">1.0</property>
                    <property name="truncation">true</property>
                    <componentName>Media File Input</componentName>
                    <componentDefinitionName>Media File Input</componentDefinitionName>
                    <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                    <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                    <childComponents/>
                    <pin name="filename" type="PROPERTY">
                        <property name="_pinProperty">filename</property>
                    </pin>
                    <pin name="UncompressedAudio" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAAYdAAGRW5kaWFudAAOUmF3QXVkaW9TYW1wbGV0
AA5SYXdBdWRpb1N0cmVhbXQAC01lZGlhVGltaW5ndAAQTWVkaWFSYXRlQ29udHJvbHQAEVVuY29t
cHJlc3NlZEF1ZGlvdAAUQ2hhbm5lbENvbmZpZ3VyYXRpb250ABFTYW1wbGVJbmZvcm1hdGlvbnQA
EkRhdGFJc01hbnVmYWN0dXJlZHQAFkF1ZGlvU2FtcGxlSW5mb3JtYXRpb250AAxTYW1wbGVGb3Jt
YXR0AAtBdWRpb1N0cmVhbXQAC01lZGlhU3RyZWFtdAALS2F5YWtCdWZmZXJ0AAhMYW5ndWFnZXQA
CFRlbXBvcmFsdAAXVW5jb21wcmVzc2VkQXVkaW9TYW1wbGV0AAhSYXdBdWRpb3QACkJ5dGVTdHJl
YW10AAtSYXRlU2FtcGxlZHQAC0F1ZGlvU2FtcGxldAAGU3RyZWFtdAAFQXVkaW90ABdVbmNvbXBy
ZXNzZWRBdWRpb1N0cmVhbXhzcQB+AAh3DAAAACA/QAAAAAAAFnNyAE9jYS5kaWdpdGFscmFwaWRz
LmtheWFrLmRhdGF0eXBlcy5kZWZpbml0aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlvbiRTaW1w
bGVUeXBlAAAAAAAAAAECAAJMABFlbnVtZXJhdGlvblZhbHVlc3QAEExqYXZhL3V0aWwvTGlzdDtM
AAR0eXBldABDTGNhL2RpZ2l0YWxyYXBpZHMva2F5YWsvZGF0YXR5cGVzL2RlZmluaXRpb24vbW9k
ZWwvU2ltcGxlVHlwZXNFbnVtO3hyAFJjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5k
ZWZpbml0aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlvbiRLZXlEZWZpbml0aW9uAAAAAAAAAAEC
AARMAAdjb21tZW50cQB+AAVMAAtkaXNwbGF5TmFtZXEAfgAFTAALbXVsdGlWYWx1ZWR0ABNMamF2
YS9sYW5nL0Jvb2xlYW47TAAEbmFtZXEAfgAFeHB0ABxJbmRpY2F0ZXMgdGhlIG1lZGlhIGR1cmF0
aW9ucHB0AA5tZWRpYV9kdXJhdGlvbnB+cgBBY2EuZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhdHlw
ZXMuZGVmaW5pdGlvbi5tb2RlbC5TaW1wbGVUeXBlc0VudW0AAAAAAAAAABIAAHhyAA5qYXZhLmxh
bmcuRW51bQAAAAAAAAAAEgAAeHB0AARUSU1Fc3EAfgAjdAAmSW5kaWNhdGVzIHRoZSBudW1iZXIg
b2YgYXVkaW8gY2hhbm5lbHNwcHQADG51bV9jaGFubmVsc3B+cQB+ACt0AAdJTlRFR0VSc3EAfgAj
dABJVGhlIHRpbWUgcGVydGFpbmluZyB0byB0aGUgZW5kIG9mIHRoZSBkYXRhICh0aW1lICsgZHVy
YXRpb24gb2YgdGhpcyBkYXRhKXBwdAAHdGltZUVuZHBxAH4ALXNxAH4AI3QAQWRvdWJsZVtdIC0g
VlUgTWV0ZXIgZGF0YSAob25lIGVudHJ5IHBlciBjaGFubmVsKSBub3JtYWxpemVkIFswLDFdcHB0
AA9hdWRpb192dV9tZXRlcnNwfnEAfgArdAAGT0JKRUNUc3EAfgAjdAAxSW5kaWNhdGVzIHRoZSBh
dmVyYWdlIGJpdCByYXRlIGluIGJpdHMgcGVyIHNlY29uZHBwdAAQYXZlcmFnZV9iaXRfcmF0ZXBx
AH4AMnNxAH4AI3QAQVRydWUgb24gdGhlIGxhc3QgZGF0YSBwYWNrZXQgb2YgdGhlIFN0cmVhbSAo
d2l0aCBvciB3aXRob3V0IGRhdGEpcHB0AAtlbmRPZlN0cmVhbXB+cQB+ACt0AAdCT09MRUFOc3EA
fgAjdABGTGV2ZWwgb2YgcHJlY2lzaW9uIC0gY2FuIGJlIGxvd2VyIHRoYW4gdGhlIGFjdHVhbCBu
dW1iZXIgb2YgdmFsaWQgYml0c3BwdAAYYWNjdXJhY3lfYml0c19wZXJfc2FtcGxlcHEAfgAyc3EA
fgAjdAAxVG90YWwgbnVtYmVyIG9mIHZhbGlkIGFuZCBpbnZhbGlkIGJpdHMgcGVyIHNhbXBsZXBw
dAAXc3RvcmFnZV9iaXRzX3Blcl9zYW1wbGVwcQB+ADJzcQB+ACN0ABdJbmRpY2F0ZXMgYnl0ZSBv
cmRlcmluZ3BwdAAGZW5kaWFuc3IAE2phdmEudXRpbC5BcnJheUxpc3R4gdIdmcdhnQMAAUkABHNp
emV4cAAAAAJ3BAAAAAJzcgA3Y2EuZGlnaXRhbHJhcGlkcy5rYXlhay5wbHVnaW4ueG1sLktheWFr
RW51bWVyYXRpb25WYWx1ZQAAAAAAAAABAgAFTAALZGVzY3JpcHRpb25xAH4ABUwAC2Rpc3BsYXlO
YW1lcQB+AAVMAAZoaWRkZW5xAH4AJ0wADnZhbHVlQXR0cmlidXRlcQB+AAVMAA12YWx1ZUVtYmVk
ZGVkcQB+AAV4cHBwcHB0AANiaWdzcQB+AE9wcHBwdAAGbGl0dGxleH5xAH4AK3QABlNUUklOR3Nx
AH4AI3QAF0Nhbm9uaWNhbCBMYW5ndWFnZSBDb2RlcHB0AA1sYW5ndWFnZV9jb2Rlc3EAfgBNAAAA
uncEAAAAunNxAH4AT3B0AAlVbmRlZmluZWRwdAADdW5kdAAAc3EAfgBPcHQADk5vdCBBcHBsaWNh
YmxlcHQAA3p4eHEAfgBdc3EAfgBPcHQAEUFia2hhemlhbiwgQWJraGF6cHQAAmFicQB+AF1zcQB+
AE9wdAAEQWZhcnB0AAJhYXEAfgBdc3EAfgBPcHQACUFmcmlrYWFuc3B0AAJhZnEAfgBdc3EAfgBP
cHQABEFrYW5wdAACYWtxAH4AXXNxAH4AT3B0AAhBbGJhbmlhbnB0AAJzcXEAfgBdc3EAfgBPcHQA
B0FtaGFyaWNwdAACYW1xAH4AXXNxAH4AT3B0AAZBcmFiaWNwdAACYXJxAH4AXXNxAH4AT3B0AAlB
cmFnb25lc2VwdAACYW5xAH4AXXNxAH4AT3B0AAhBcm1lbmlhbnB0AAJoeXEAfgBdc3EAfgBPcHQA
CEFzc2FtZXNlcHQAAmFzcQB+AF1zcQB+AE9wdAAGQXZhcmljcHQAAmF2cQB+AF1zcQB+AE9wdAAH
QXZlc3RhbnB0AAJhZXEAfgBdc3EAfgBPcHQABkF5bWFyYXB0AAJheXEAfgBdc3EAfgBPcHQAC0F6
ZXJiYWlqYW5pcHQAAmF6cQB+AF1zcQB+AE9wdAAHQmFtYmFyYXB0AAJibXEAfgBdc3EAfgBPcHQA
B0Jhc2hraXJwdAACYmFxAH4AXXNxAH4AT3B0AAZCYXNxdWVwdAACZXVxAH4AXXNxAH4AT3B0AApC
ZWxhcnVzaWFucHQAAmJlcQB+AF1zcQB+AE9wdAAHQmVuZ2FsaXB0AAJibnEAfgBdc3EAfgBPcHQA
EEJpaGFyaSBMYW5ndWFnZXNwdAACYmhxAH4AXXNxAH4AT3B0AAdCaXNsYW1hcHQAAmJpcQB+AF1z
cQB+AE9wdAAHQm9zbmlhbnB0AAJic3EAfgBdc3EAfgBPcHQABkJyZXRvbnB0AAJicnEAfgBdc3EA
fgBPcHQACUJ1bGdhcmlhbnB0AAJiZ3EAfgBdc3EAfgBPcHQAB0J1cm1lc2VwdAACbXlxAH4AXXNx
AH4AT3B0ABJDYXRhbGFuLCBWYWxlbmNpYW5wdAACY2FxAH4AXXNxAH4AT3B0AAhDaGFtb3Jyb3B0
AAJjaHEAfgBdc3EAfgBPcHQAB0NoZWNoZW5wdAACY2VxAH4AXXNxAH4AT3B0ABdDaGljaGV3YSwg
Q2hld2EsIE55YW5qYXB0AAJueXEAfgBdc3EAfgBPcHQAB0NoaW5lc2VwdAACemhxAH4AXXNxAH4A
T3B0ABxDaHVyY2ggU2xhdmljLCBPbGQgQnVsZ2FyaWFucHQAAmN1cQB+AF1zcQB+AE9wdAAHQ2h1
dmFzaHB0AAJjdnEAfgBdc3EAfgBPcHQAB0Nvcm5pc2hwdAACa3dxAH4AXXNxAH4AT3B0AAhDb3Jz
aWNhbnB0AAJjb3EAfgBdc3EAfgBPcHQABENyZWVwdAACY3JxAH4AXXNxAH4AT3B0AAhDcm9hdGlh
bnB0AAJocnEAfgBdc3EAfgBPcHQABUN6ZWNocHQAAmNzcQB+AF1zcQB+AE9wdAAGRGFuaXNocHQA
AmRhcQB+AF1zcQB+AE9wdAAaRGl2ZWhpLCBEaGl2ZWhpLCBNYWxkaXZpYW5wdAACZHZxAH4AXXNx
AH4AT3B0AA5EdXRjaCwgRmxlbWlzaHB0AAJubHEAfgBdc3EAfgBPcHQACER6b25na2hhcHQAAmR6
cQB+AF1zcQB+AE9wdAAHRW5nbGlzaHB0AAJlbnEAfgBdc3EAfgBPcHQACUVzcGVyYW50b3B0AAJl
b3EAfgBdc3EAfgBPcHQACEVzdG9uaWFucHQAAmV0cQB+AF1zcQB+AE9wdAADRXdlcHQAAmVlcQB+
AF1zcQB+AE9wdAAHRmFyb2VzZXB0AAJmb3EAfgBdc3EAfgBPcHQABkZpamlhbnB0AAJmanEAfgBd
c3EAfgBPcHQAB0Zpbm5pc2hwdAACZmlxAH4AXXNxAH4AT3B0AAZGcmVuY2hwdAACZnJxAH4AXXNx
AH4AT3B0AAVGdWxhaHB0AAJmZnEAfgBdc3EAfgBPcHQACEdhbGljaWFucHQAAmdscQB+AF1zcQB+
AE9wdAAFR2FuZGFwdAACbGdxAH4AXXNxAH4AT3B0AAhHZW9yZ2lhbnB0AAJrYXEAfgBdc3EAfgBP
cHQABkdlcm1hbnB0AAJkZXEAfgBdc3EAfgBPcHQAB0d1YXJhbmlwdAACZ25xAH4AXXNxAH4AT3B0
AAhHdWphcmF0aXB0AAJndXEAfgBdc3EAfgBPcHQAD0hhaXRpYW4sIENyZW9sZXB0AAJodHEAfgBd
c3EAfgBPcHQABUhhdXNhcHQAAmhhcQB+AF1zcQB+AE9wdAAGSGVicmV3cHQAAmhlcQB+AF1zcQB+
AE9wdAAGSGVyZXJvcHQAAmh6cQB+AF1zcQB+AE9wdAAFSGluZGlwdAACaGlxAH4AXXNxAH4AT3B0
AAlIaXJpIE1vdHVwdAACaG9xAH4AXXNxAH4AT3B0AAlIdW5nYXJpYW5wdAACaHVxAH4AXXNxAH4A
T3B0AAlJY2VsYW5kaWNwdAACaXNxAH4AXXNxAH4AT3B0AANJZG9wdAACaW9xAH4AXXNxAH4AT3B0
AARJZ2JvcHQAAmlncQB+AF1zcQB+AE9wdAAKSW5kb25lc2lhbnB0AAJpZHEAfgBdc3EAfgBPcHQA
C0ludGVybGluZ3VhcHQAAmlhcQB+AF1zcQB+AE9wdAAXSW50ZXJsaW5ndWUsIE9jY2lkZW50YWxw
dAACaWVxAH4AXXNxAH4AT3B0AAlJbnVrdGl0dXRwdAACaXVxAH4AXXNxAH4AT3B0AAdJbnVwaWFx
cHQAAmlrcQB+AF1zcQB+AE9wdAAFSXJpc2hwdAACZ2FxAH4AXXNxAH4AT3B0AAdJdGFsaWFucHQA
Aml0cQB+AF1zcQB+AE9wdAAISmFwYW5lc2VwdAACamFxAH4AXXNxAH4AT3B0AAhKYXZhbmVzZXB0
AAJqdnEAfgBdc3EAfgBPcHQAGEthbGFhbGxpc3V0LCBHcmVlbmxhbmRpY3B0AAJrbHEAfgBdc3EA
fgBPcHQAB0thbm5hZGFwdAACa25xAH4AXXNxAH4AT3B0AAZLYW51cmlwdAACa3JxAH4AXXNxAH4A
T3B0AAhLYXNobWlyaXB0AAJrc3EAfgBdc3EAfgBPcHQABkthemFraHB0AAJra3EAfgBdc3EAfgBP
cHQABUtobWVycHQAAmttcQB+AF1zcQB+AE9wdAAOS2lrdXl1LCBHaWt1eXVwdAACa2lxAH4AXXNx
AH4AT3B0AAtLaW55YXJ3YW5kYXB0AAJyd3EAfgBdc3EAfgBPcHQAD0tpcmdoaXosIEt5cmd5enB0
AAJreXEAfgBdc3EAfgBPcHQAB0tpcnVuZGlwdAACcm5xAH4AXXNxAH4AT3B0AARLb21pcHQAAmt2
cQB+AF1zcQB+AE9wdAAFS29uZ29wdAACa2dxAH4AXXNxAH4AT3B0AAZLb3JlYW5wdAACa29xAH4A
XXNxAH4AT3B0ABJLdWFueWFtYSwgS3dhbnlhbWFwdAACa2pxAH4AXXNxAH4AT3B0AAdLdXJkaXNo
cHQAAmt1cQB+AF1zcQB+AE9wdAADTGFvcHQAAmxvcQB+AF1zcQB+AE9wdAAFTGF0aW5wdAACbGFx
AH4AXXNxAH4AT3B0AAdMYXR2aWFucHQAAmx2cQB+AF1zcQB+AE9wdAAgTGltYnVyZ2FuLCBMaW1i
dXJnZXIsIExpbWJ1cmdpc2hwdAACbGlxAH4AXXNxAH4AT3B0AAdMaW5nYWxhcHQAAmxucQB+AF1z
cQB+AE9wdAAKTGl0aHVhbmlhbnB0AAJsdHEAfgBdc3EAfgBPcHQADEx1YmEtS2F0YW5nYXB0AAJs
dXEAfgBdc3EAfgBPcHQAHEx1eGVtYm91cmdpc2gsIExldHplYnVyZ2VzY2hwdAACbGJxAH4AXXNx
AH4AT3B0AApNYWNlZG9uaWFucHQAAm1rcQB+AF1zcQB+AE9wdAAITWFsYWdhc3lwdAACbWdxAH4A
XXNxAH4AT3B0AAVNYWxheXB0AAJtc3EAfgBdc3EAfgBPcHQACU1hbGF5YWxhbXB0AAJtbHEAfgBd
c3EAfgBPcHQAB01hbHRlc2VwdAACbXRxAH4AXXNxAH4AT3B0AARNYW54cHQAAmd2cQB+AF1zcQB+
AE9wdAAFTWFvcmlwdAACbWlxAH4AXXNxAH4AT3B0AAdNYXJhdGhpcHQAAm1ycQB+AF1zcQB+AE9w
dAALTWFyc2hhbGxlc2VwdAACbWhxAH4AXXNxAH4AT3B0AAxNb2Rlcm4gR3JlZWtwdAACZWxxAH4A
XXNxAH4AT3B0AAlNb25nb2xpYW5wdAACbW5xAH4AXXNxAH4AT3B0AAVOYXVydXB0AAJuYXEAfgBd
c3EAfgBPcHQADk5hdmFqbywgTmF2YWhvcHQAAm52cQB+AF1zcQB+AE9wdAAGTmRvbmdhcHQAAm5n
cQB+AF1zcQB+AE9wdAAGTmVwYWxpcHQAAm5lcQB+AF1zcQB+AE9wdAANTm9ydGggTmRlYmVsZXB0
AAJuZHEAfgBdc3EAfgBPcHQADU5vcnRoZXJuIFNhbWlwdAACc2VxAH4AXXNxAH4AT3B0AAlOb3J3
ZWdpYW5wdAACbm9xAH4AXXNxAH4AT3B0ABFOb3J3ZWdpYW4gQm9rbcOlbHB0AAJuYnEAfgBdc3EA
fgBPcHQAEU5vcndlZ2lhbiBOeW5vcnNrcHQAAm5ucQB+AF1zcQB+AE9wdAATT2NjaXRhbiAocG9z
dCAxNTAwKXB0AAJvY3EAfgBdc3EAfgBPcHQABk9qaWJ3YXB0AAJvanEAfgBdc3EAfgBPcHQABU9y
aXlhcHQAAm9ycQB+AF1zcQB+AE9wdAAFT3JvbW9wdAACb21xAH4AXXNxAH4AT3B0ABFPc3NldGlh
biwgT3NzZXRpY3B0AAJvc3EAfgBdc3EAfgBPcHQABFBhbGlwdAACcGlxAH4AXXNxAH4AT3B0ABBQ
YW5qYWJpLCBQdW5qYWJpcHQAAnBhcQB+AF1zcQB+AE9wdAAHUGVyc2lhbnB0AAJmYXEAfgBdc3EA
fgBPcHQABlBvbGlzaHB0AAJwbHEAfgBdc3EAfgBPcHQAClBvcnR1Z3Vlc2VwdAACcHRxAH4AXXNx
AH4AT3B0AA5QdXNodG8sIFBhc2h0b3B0AAJwc3EAfgBdc3EAfgBPcHQAB1F1ZWNodWFwdAACcXVx
AH4AXXNxAH4AT3B0AB1Sb21hbmlhbiwgTW9sZGF2aWFuLCBNb2xkb3ZhbnB0AAJyb3EAfgBdc3EA
fgBPcHQAB1JvbWFuc2hwdAACcm1xAH4AXXNxAH4AT3B0AAdSdXNzaWFucHQAAnJ1cQB+AF1zcQB+
AE9wdAAGU2Ftb2FucHQAAnNtcQB+AF1zcQB+AE9wdAAFU2FuZ29wdAACc2dxAH4AXXNxAH4AT3B0
AAhTYW5za3JpdHB0AAJzYXEAfgBdc3EAfgBPcHQACVNhcmRpbmlhbnB0AAJzY3EAfgBdc3EAfgBP
cHQAD1Njb3R0aXNoIEdhZWxpY3B0AAJnZHEAfgBdc3EAfgBPcHQAB1NlcmJpYW5wdAACc3JxAH4A
XXNxAH4AT3B0AAVTaG9uYXB0AAJzbnEAfgBdc3EAfgBPcHQAEVNpY2h1YW4gWWksIE51b3N1cHQA
AmlpcQB+AF1zcQB+AE9wdAAGU2luZGhpcHQAAnNkcQB+AF1zcQB+AE9wdAASU2luaGFsYSwgU2lu
aGFsZXNlcHQAAnNpcQB+AF1zcQB+AE9wdAAGU2xvdmFrcHQAAnNrcQB+AF1zcQB+AE9wdAAJU2xv
dmVuaWFucHQAAnNscQB+AF1zcQB+AE9wdAAGU29tYWxpcHQAAnNvcQB+AF1zcQB+AE9wdAANU291
dGggTmRlYmVsZXB0AAJucnEAfgBdc3EAfgBPcHQADlNvdXRoZXJuIFNvdGhvcHQAAnN0cQB+AF1z
cQB+AE9wdAASU3BhbmlzaCwgQ2FzdGlsaWFucHQAAmVzcQB+AF1zcQB+AE9wdAAJU3VuZGFuZXNl
cHQAAnN1cQB+AF1zcQB+AE9wdAAHU3dhaGlsaXB0AAJzd3EAfgBdc3EAfgBPcHQABVN3YXRpcHQA
AnNzcQB+AF1zcQB+AE9wdAAHU3dlZGlzaHB0AAJzdnEAfgBdc3EAfgBPcHQAB1RhZ2Fsb2dwdAAC
dGxxAH4AXXNxAH4AT3B0AAhUYWhpdGlhbnB0AAJ0eXEAfgBdc3EAfgBPcHQABVRhamlrcHQAAnRn
cQB+AF1zcQB+AE9wdAAFVGFtaWxwdAACdGFxAH4AXXNxAH4AT3B0AAVUYXRhcnB0AAJ0dHEAfgBd
c3EAfgBPcHQABlRlbHVndXB0AAJ0ZXEAfgBdc3EAfgBPcHQABFRoYWlwdAACdGhxAH4AXXNxAH4A
T3B0AAdUaWJldGFucHQAAmJvcQB+AF1zcQB+AE9wdAAIVGlncmlueWFwdAACdGlxAH4AXXNxAH4A
T3B0ABVUb25nYSAoVG9uZ2EgSXNsYW5kcylwdAACdG9xAH4AXXNxAH4AT3B0AAZUc29uZ2FwdAAC
dHNxAH4AXXNxAH4AT3B0AAZUc3dhbmFwdAACdG5xAH4AXXNxAH4AT3B0AAdUdXJraXNocHQAAnRy
cQB+AF1zcQB+AE9wdAAHVHVya21lbnB0AAJ0a3EAfgBdc3EAfgBPcHQAA1R3aXB0AAJ0d3EAfgBd
c3EAfgBPcHQADlVpZ2h1ciwgVXlnaHVycHQAAnVncQB+AF1zcQB+AE9wdAAJVWtyYWluaWFucHQA
AnVrcQB+AF1zcQB+AE9wdAAEVXJkdXB0AAJ1cnEAfgBdc3EAfgBPcHQABVV6YmVrcHQAAnV6cQB+
AF1zcQB+AE9wdAAFVmVuZGFwdAACdmVxAH4AXXNxAH4AT3B0AApWaWV0bmFtZXNlcHQAAnZpcQB+
AF1zcQB+AE9wdAAIVm9sYXDDvGtwdAACdm9xAH4AXXNxAH4AT3B0AAdXYWxsb29ucHQAAndhcQB+
AF1zcQB+AE9wdAAFV2Vsc2hwdAACY3lxAH4AXXNxAH4AT3B0AA9XZXN0ZXJuIEZyaXNpYW5wdAAC
ZnlxAH4AXXNxAH4AT3B0AAVXb2xvZnB0AAJ3b3EAfgBdc3EAfgBPcHQABVhob3NhcHQAAnhocQB+
AF1zcQB+AE9wdAAHWWlkZGlzaHB0AAJ5aXEAfgBdc3EAfgBPcHQABllvcnViYXB0AAJ5b3EAfgBd
c3EAfgBPcHQADlpodWFuZywgQ2h1YW5ncHQAAnphcQB+AF1zcQB+AE9wdAAEWnVsdXB0AAJ6dXEA
fgBdeHEAfgBUc3IAUGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24u
bW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJENvbXBsZXhUeXBlAAAAAAAAAAECAAJMAAhvcHRpb25h
bHEAfgAnTAAEdHlwZXEAfgAFeHEAfgAmdAApRGV0YWlsIGluZm9ybWF0aW9uIGZvciBlYWNoIGF1
ZGlvIGNoYW5uZWxwc3IAEWphdmEubGFuZy5Cb29sZWFuzSBygNWc+u4CAAFaAAV2YWx1ZXhwAXQA
FWF1ZGlvX2NoYW5uZWxfZGV0YWlsc3B0AAxBdWRpb0NoYW5uZWxzcQB+ACN0AC9JbmRpY2F0ZXMg
aWYgdGhlIHN0cmVhbSBoYXMgYSBjb25zdGFudCBiaXQgcmF0ZXBwdAARY29uc3RhbnRfYml0X3Jh
dGVwcQB+AEJzcQB+ACN0ADNQb3NpdGlvbiBvciBvZmZzZXQgZnJvbSB0aGUgYmVnaW5uaW5nIG9m
IHRoZSBzdHJlYW1wcHQAEHBvc2l0aW9uSW5TdHJlYW1wfnEAfgArdAAETE9OR3NxAH4AI3BwcHQA
C3NhbXBsZV9yYXRlcH5xAH4AK3QACFJBVElPTkFMc3EAfgAjdAA3SW5kaWNhdGVzIHRoZSBudW1i
ZXIgb2YgY2hhbm5lbHMgd2l0aCBhY3R1YWwgYXVkaW8gZGF0YXBwdAATbnVtX2FjdGl2ZV9jaGFu
bmVsc3BxAH4AMnNxAH4AI3QAKkludGVycHJldCB0aGUgc2FtcGxlIGFzIHNpZ25lZCBvciB1bnNp
Z25lZHBwdAANc2FtcGxlX3NpZ25lZHBxAH4AQnNxAH4AI3QAH051bWJlciBvZiB2YWxpZCBiaXRz
IHBlciBzYW1wbGVwcHQAD2JpdHNfcGVyX3NhbXBsZXBxAH4AMnNxAH4AI3QAMUluZGljYXRlcyB0
aGUgbWF4aW11bSBiaXQgcmF0ZSBpbiBiaXRzIHBlciBzZWNvbmRwcHQAEG1heGltdW1fYml0X3Jh
dGVwcQB+ADJzcQB+ACN0AC1UaGUgbW9zdCByZWxldmFudCB0aW1lIHBlcnRhaW5pbmcgdG8gdGhl
IGRhdGFwcHQABHRpbWVwcQB+AC1zcQB+ACNwcHB0ABRkYXRhX2lzX21hbnVmYWN0dXJlZHBxAH4A
QnNxAH4AI3QAI1RvdGFsIGxlbmd0aCBvZiB0aGUgc3RyZWFtIGlmIGtub3ducHB0AA5sZW5ndGhP
ZlN0cmVhbXBxAH4ClnNxAH4AI3QAMkluZGljYXRlcyB0aGUgc2l6ZSBvZiB0aGUgZGVjb2Rpbmcg
YnVmZmVyIGluIGJ5dGVzcHB0ABRkZWNvZGluZ19idWZmZXJfc2l6ZXBxAH4AMnhwc3IAEWphdmEu
dXRpbC5IYXNoTWFwBQfawcMWYNEDAAJGAApsb2FkRmFjdG9ySQAJdGhyZXNob2xkeHA/AAAAAAAA
IHcIAAAAQAAAAA5xAH4AMXNyABFqYXZhLmxhbmcuSW50ZWdlchLioKT3gYc4AgABSQAFdmFsdWV4
cgAQamF2YS5sYW5nLk51bWJlcoaslR0LlOCLAgAAeHAAAAACcQB+ACpzcgAkY2EuZGlnaXRhbHJh
cGlkcy5rYXlhay50aW1lLlRpbWVJbXBsAAAAAAAAAAECAAJMAAhyYXRpb25hbHQAMUxjYS9kaWdp
dGFscmFwaWRzL2theWFrL2RhdGEvaW1wbC9SYXRpb25hbE51bWJlcjtMAAh0aW1lQmFzZXQAJkxj
YS9kaWdpdGFscmFwaWRzL2theWFrL3RpbWUvVGltZUJhc2U7eHBzcgAvY2EuZGlnaXRhbHJhcGlk
cy5rYXlhay5kYXRhLmltcGwuUmF0aW9uYWxOdW1iZXIAAAAAAAAAAQIABEoAC2Rlbm9taW5hdG9y
WgAJaXNSZWR1Y2VkWgATbmVlZHNCaWdGb3JNdWx0aXBseUoACW51bWVyYXRvcnhxAH4CtgAAAAAA
ALuAAAAAAAAAAIf4AHNyAChjYS5kaWdpdGFscmFwaWRzLmtheWFrLnRpbWUuVGltZUJhc2VJbXBs
AAAAAAAAAAECAAFMAA5vZmZzZXRSYXRpb25hbHEAfgK5eHBzcQB+ArwAAAAAO5rKAAAAAAAAAAAA
AABxAH4APnNxAH4CtQAXcABxAH4ATHEAfgBTcQB+Ao5zcgAmamF2YS51dGlsLkNvbGxlY3Rpb25z
JFVubW9kaWZpYWJsZUxpc3T8DyUxteyOEAIAAUwABGxpc3RxAH4AJHhyACxqYXZhLnV0aWwuQ29s
bGVjdGlvbnMkVW5tb2RpZmlhYmxlQ29sbGVjdGlvbhlCAIDLXvceAgABTAABY3QAFkxqYXZhL3V0
aWwvQ29sbGVjdGlvbjt4cHNxAH4ATQAAAAJ3BAAAAAJzcQB+AAAAc3EAfgAEdAAqR2VuZXJhbCBp
bmZvcm1hdGlvbiBhYm91dCBhbiBBdWRpbyBDaGFubmVsc3IAJWphdmEudXRpbC5Db2xsZWN0aW9u
cyRVbm1vZGlmaWFibGVTZXSAHZLRj5uAVQIAAHhxAH4Cw3NxAH4ACHcMAAAAED9AAAAAAAABdAAI
TGFuZ3VhZ2V4c3EAfgLKc3EAfgAIdwwAAAAQP0AAAAAAAARxAH4AVnNxAH4CiXBwcHQAF2NoYW5u
ZWxfZm9ybWF0X292ZXJyaWRlcQB+Ao10AAZTdHJlYW1zcQB+ACN0ABNTcGVha2VyIGRlc2lnbmF0
aW9ucHB0ABhjaGFubmVsX3NwZWFrZXJfcG9zaXRpb25zcQB+AE0AAAAedwQAAAAec3EAfgBPcHQA
BE1vbm9wdAAETU9OT3EAfgBdc3EAfgBPcHQABkNlbnRlcnB0AAhDX0NFTlRFUnEAfgBdc3EAfgBP
cHQABExlZnRwdAAGTF9MRUZUcQB+AF1zcQB+AE9wdAAFUmlnaHRwdAAHUl9SSUdIVHEAfgBdc3EA
fgBPcHQAB0x0IExlZnRwdAAHTHRfTEVGVHEAfgBdc3EAfgBPcHQACFJ0IFJpZ2h0cHQACFJ0X1JJ
R0hUcQB+AF1zcQB+AE9wdAAPQ2VudGVyIHN1cnJvdW5kcHQAEkNzX0NFTlRFUl9TVVJST1VORHEA
fgBdc3EAfgBPcHQADUxlZnQgc3Vycm91bmRwdAAQTHNfTEVGVF9TVVJST1VORHEAfgBdc3EAfgBP
cHQADlJpZ2h0IHN1cnJvdW5kcHQAEVJzX1JJR0hUX1NVUlJPVU5EcQB+AF1zcQB+AE9wdAANTG93
IGZyZXF1ZW5jeXB0ABFMRkVfTE9XX0ZSRVFVRU5DWXEAfgBdc3EAfgBPcHQAElJlYXIgbGVmdCBz
dXJyb3VuZHB0ABZSbHNfUkVBUl9MRUZUX1NVUlJPVU5EcQB+AF1zcQB+AE9wdAATUmVhciByaWdo
dCBzdXJyb3VuZHB0ABdScnNfUkVBUl9SSUdIVF9TVVJST1VORHEAfgBdc3EAfgBPcHQAElNpZGUg
bGVmdCBzdXJyb3VuZHB0ABZTbHNfU0lERV9MRUZUX1NVUlJPVU5EcQB+AF1zcQB+AE9wdAATU2lk
ZSByaWdodCBzdXJyb3VuZHB0ABdTcnNfU0lERV9SSUdIVF9TVVJST1VORHEAfgBdc3EAfgBPcHQA
C0xlZnQgY2VudGVycHQADkxjX0xFRlRfQ0VOVEVScQB+AF1zcQB+AE9wdAAMUmlnaHQgY2VudGVy
cHQAD1JjX1JJR0hUX0NFTlRFUnEAfgBdc3EAfgBPcHQACUxlZnQgd2lkZXB0AAxMd19MRUZUX1dJ
REVxAH4AXXNxAH4AT3B0AApSaWdodCB3aWRlcHQADVJ3X1JJR0hUX1dJREVxAH4AXXNxAH4AT3B0
ABlMZWZ0IHN1cnJvdW5kIGRpcmVjdGlvbmFscHQAHUxzZF9MRUZUX1NVUlJPVU5EX0RJUkVDVElP
TkFMcQB+AF1zcQB+AE9wdAAaUmlnaHQgc3Vycm91bmQgZGlyZWN0aW9uYWxwdAAeUnNkX1JJR0hU
X1NVUlJPVU5EX0RJUkVDVElPTkFMcQB+AF1zcQB+AE9wdAAPTG93IGZyZXF1ZW5jeSAycHQAEkxG
RTJfTE9XX0ZSRVFVRU5DWXEAfgBdc3EAfgBPcHQAE1RvcCBjZW50ZXIgc3Vycm91bmRwdAAWVHNf
VE9QX0NFTlRFUl9TVVJST1VORHEAfgBdc3EAfgBPcHQAFlZlcnRpY2FsIGhlaWdodCBjZW50ZXJw
dAAaVmhjX1ZFUlRJQ0FMX0hFSUdIVF9DRU5URVJxAH4AXXNxAH4AT3B0ABRWZXJ0aWNhbCBoZWln
aHQgbGVmdHB0ABhWaGxfVkVSVElDQUxfSEVJR0hUX0xFRlRxAH4AXXNxAH4AT3B0ABVWZXJ0aWNh
bCBoZWlnaHQgcmlnaHRwdAAZVmhyX1ZFUlRJQ0FMX0hFSUdIVF9SSUdIVHEAfgBdc3EAfgBPcHQA
GVZlcnRpY2FsIGhlaWdodCBzaWRlIGxlZnRwdAAeVmhzbF9WRVJUSUNBTF9IRUlHSFRfU0lERV9M
RUZUcQB+AF1zcQB+AE9wdAAaVmVydGljYWwgaGVpZ2h0IHNpZGUgcmlnaHRwdAAfVmhzcl9WRVJU
SUNBTF9IRUlHSFRfU0lERV9SSUdIVHEAfgBdc3EAfgBPcHQAG1ZlcnRpY2FsIGhlaWdodCByZWFy
IGNlbnRlcnB0ACBWaHJjX1ZFUlRJQ0FMX0hFSUdIVF9SRUFSX0NFTlRFUnEAfgBdc3EAfgBPcHQA
GVZlcnRpY2FsIGhlaWdodCByZWFyIGxlZnRwdAAeVmhybF9WRVJUSUNBTF9IRUlHSFRfUkVBUl9M
RUZUcQB+AF1zcQB+AE9wdAAaVmVydGljYWwgaGVpZ2h0IHJlYXIgcmlnaHRwdAAfVmhycl9WRVJU
SUNBTF9IRUlHSFRfUkVBUl9SSUdIVHEAfgBdeHEAfgBUc3EAfgAjcHBwdAANY2hhbm5lbF9ncm91
cHBxAH4AVHh0AAxBdWRpb0NoYW5uZWxzcQB+ArM/AAAAAAAAIHcIAAAAQAAAAAJxAH4AWHQAAmVu
cQB+AtV0AAZMX0xFRlR4c3EAfgAAAHEAfgLIc3EAfgKzPwAAAAAAACB3CAAAAEAAAAACcQB+AFh0
AAJlbnEAfgLVdAAHUl9SSUdIVHh4cQB+AsZxAH4AWHQAAmVucQB+Ap5xAH4Ct3EAfgKhcQB+Ao1x
AH4Cp3NxAH4CtQAXcABxAH4ARnNxAH4CtQAAABBxAH4ASXEAfgM9cQB+ApJxAH4CjXEAfgKZc3EA
fgK8AAAAAAAAAAEBAAAAAAAAALuAcQB+AqRxAH4DPXg=</property>
                        <pinDefinition name="UncompressedAudio" displayName="Uncompressed Audio" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="CompressedAudio" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAAVdAAGRW5kaWFudAALTWVkaWFUaW1pbmd0ABBN
ZWRpYVJhdGVDb250cm9sdAAOQUFDQXVkaW9TdHJlYW10ABRDaGFubmVsQ29uZmlndXJhdGlvbnQA
EVNhbXBsZUluZm9ybWF0aW9udAAWQXVkaW9TYW1wbGVJbmZvcm1hdGlvbnQADFNhbXBsZUZvcm1h
dHQAC0F1ZGlvU3RyZWFtdAAOQUFDQXVkaW9TYW1wbGV0AAhBQUNBdWRpb3QAC01lZGlhU3RyZWFt
dAALS2F5YWtCdWZmZXJ0AAhMYW5ndWFnZXQACFRlbXBvcmFsdAAKQnl0ZVN0cmVhbXQAC1JhdGVT
YW1wbGVkdAALQXVkaW9TYW1wbGV0AAZTdHJlYW10AAtBQUNNZXRhZGF0YXQABUF1ZGlveHNxAH4A
CHcMAAAAQD9AAAAAAAAac3IAT2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmlu
aXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJFNpbXBsZVR5cGUAAAAAAAAAAQIAAkwAEWVu
dW1lcmF0aW9uVmFsdWVzdAAQTGphdmEvdXRpbC9MaXN0O0wABHR5cGV0AENMY2EvZGlnaXRhbHJh
cGlkcy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9TaW1wbGVUeXBlc0VudW07eHIA
UmNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5
cGVEZWZpbml0aW9uJEtleURlZmluaXRpb24AAAAAAAAAAQIABEwAB2NvbW1lbnRxAH4ABUwAC2Rp
c3BsYXlOYW1lcQB+AAVMAAttdWx0aVZhbHVlZHQAE0xqYXZhL2xhbmcvQm9vbGVhbjtMAARuYW1l
cQB+AAV4cHQAHEluZGljYXRlcyB0aGUgbWVkaWEgZHVyYXRpb25wcHQADm1lZGlhX2R1cmF0aW9u
cH5yAEFjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZpbml0aW9uLm1vZGVsLlNp
bXBsZVR5cGVzRW51bQAAAAAAAAAAEgAAeHIADmphdmEubGFuZy5FbnVtAAAAAAAAAAASAAB4cHQA
BFRJTUVzcQB+ACB0ACZJbmRpY2F0ZXMgdGhlIG51bWJlciBvZiBhdWRpbyBjaGFubmVsc3BwdAAM
bnVtX2NoYW5uZWxzcH5xAH4AKHQAB0lOVEVHRVJzcQB+ACB0AEFkb3VibGVbXSAtIFZVIE1ldGVy
IGRhdGEgKG9uZSBlbnRyeSBwZXIgY2hhbm5lbCkgbm9ybWFsaXplZCBbMCwxXXBwdAAPYXVkaW9f
dnVfbWV0ZXJzcH5xAH4AKHQABk9CSkVDVHNxAH4AIHQAMUluZGljYXRlcyB0aGUgYXZlcmFnZSBi
aXQgcmF0ZSBpbiBiaXRzIHBlciBzZWNvbmRwcHQAEGF2ZXJhZ2VfYml0X3JhdGVwcQB+AC9zcQB+
ACB0AEFUcnVlIG9uIHRoZSBsYXN0IGRhdGEgcGFja2V0IG9mIHRoZSBTdHJlYW0gKHdpdGggb3Ig
d2l0aG91dCBkYXRhKXBwdAALZW5kT2ZTdHJlYW1wfnEAfgAodAAHQk9PTEVBTnNxAH4AIHQAF0lu
ZGljYXRlcyBieXRlIG9yZGVyaW5ncHB0AAZlbmRpYW5zcgATamF2YS51dGlsLkFycmF5TGlzdHiB
0h2Zx2GdAwABSQAEc2l6ZXhwAAAAAncEAAAAAnNyADdjYS5kaWdpdGFscmFwaWRzLmtheWFrLnBs
dWdpbi54bWwuS2F5YWtFbnVtZXJhdGlvblZhbHVlAAAAAAAAAAECAAVMAAtkZXNjcmlwdGlvbnEA
fgAFTAALZGlzcGxheU5hbWVxAH4ABUwABmhpZGRlbnEAfgAkTAAOdmFsdWVBdHRyaWJ1dGVxAH4A
BUwADXZhbHVlRW1iZWRkZWRxAH4ABXhwcHBwcHQAA2JpZ3NxAH4AQ3BwcHB0AAZsaXR0bGV4fnEA
fgAodAAGU1RSSU5Hc3EAfgAgdAA0SW5kaWNhdGVzIHRoZSBNUEVHNCBhdWRpbyBkZXNjcmlwdG9y
IGZvciB0aGlzIHN0cmVhbXBwdAAOYWFjX2Rlc2NyaXB0b3JwfnEAfgAodAAKQllURV9BUlJBWXNx
AH4AIHQAF0Nhbm9uaWNhbCBMYW5ndWFnZSBDb2RlcHB0AA1sYW5ndWFnZV9jb2Rlc3EAfgBBAAAA
uncEAAAAunNxAH4AQ3B0AAlVbmRlZmluZWRwdAADdW5kdAAAc3EAfgBDcHQADk5vdCBBcHBsaWNh
YmxlcHQAA3p4eHEAfgBWc3EAfgBDcHQAEUFia2hhemlhbiwgQWJraGF6cHQAAmFicQB+AFZzcQB+
AENwdAAEQWZhcnB0AAJhYXEAfgBWc3EAfgBDcHQACUFmcmlrYWFuc3B0AAJhZnEAfgBWc3EAfgBD
cHQABEFrYW5wdAACYWtxAH4AVnNxAH4AQ3B0AAhBbGJhbmlhbnB0AAJzcXEAfgBWc3EAfgBDcHQA
B0FtaGFyaWNwdAACYW1xAH4AVnNxAH4AQ3B0AAZBcmFiaWNwdAACYXJxAH4AVnNxAH4AQ3B0AAlB
cmFnb25lc2VwdAACYW5xAH4AVnNxAH4AQ3B0AAhBcm1lbmlhbnB0AAJoeXEAfgBWc3EAfgBDcHQA
CEFzc2FtZXNlcHQAAmFzcQB+AFZzcQB+AENwdAAGQXZhcmljcHQAAmF2cQB+AFZzcQB+AENwdAAH
QXZlc3RhbnB0AAJhZXEAfgBWc3EAfgBDcHQABkF5bWFyYXB0AAJheXEAfgBWc3EAfgBDcHQAC0F6
ZXJiYWlqYW5pcHQAAmF6cQB+AFZzcQB+AENwdAAHQmFtYmFyYXB0AAJibXEAfgBWc3EAfgBDcHQA
B0Jhc2hraXJwdAACYmFxAH4AVnNxAH4AQ3B0AAZCYXNxdWVwdAACZXVxAH4AVnNxAH4AQ3B0AApC
ZWxhcnVzaWFucHQAAmJlcQB+AFZzcQB+AENwdAAHQmVuZ2FsaXB0AAJibnEAfgBWc3EAfgBDcHQA
EEJpaGFyaSBMYW5ndWFnZXNwdAACYmhxAH4AVnNxAH4AQ3B0AAdCaXNsYW1hcHQAAmJpcQB+AFZz
cQB+AENwdAAHQm9zbmlhbnB0AAJic3EAfgBWc3EAfgBDcHQABkJyZXRvbnB0AAJicnEAfgBWc3EA
fgBDcHQACUJ1bGdhcmlhbnB0AAJiZ3EAfgBWc3EAfgBDcHQAB0J1cm1lc2VwdAACbXlxAH4AVnNx
AH4AQ3B0ABJDYXRhbGFuLCBWYWxlbmNpYW5wdAACY2FxAH4AVnNxAH4AQ3B0AAhDaGFtb3Jyb3B0
AAJjaHEAfgBWc3EAfgBDcHQAB0NoZWNoZW5wdAACY2VxAH4AVnNxAH4AQ3B0ABdDaGljaGV3YSwg
Q2hld2EsIE55YW5qYXB0AAJueXEAfgBWc3EAfgBDcHQAB0NoaW5lc2VwdAACemhxAH4AVnNxAH4A
Q3B0ABxDaHVyY2ggU2xhdmljLCBPbGQgQnVsZ2FyaWFucHQAAmN1cQB+AFZzcQB+AENwdAAHQ2h1
dmFzaHB0AAJjdnEAfgBWc3EAfgBDcHQAB0Nvcm5pc2hwdAACa3dxAH4AVnNxAH4AQ3B0AAhDb3Jz
aWNhbnB0AAJjb3EAfgBWc3EAfgBDcHQABENyZWVwdAACY3JxAH4AVnNxAH4AQ3B0AAhDcm9hdGlh
bnB0AAJocnEAfgBWc3EAfgBDcHQABUN6ZWNocHQAAmNzcQB+AFZzcQB+AENwdAAGRGFuaXNocHQA
AmRhcQB+AFZzcQB+AENwdAAaRGl2ZWhpLCBEaGl2ZWhpLCBNYWxkaXZpYW5wdAACZHZxAH4AVnNx
AH4AQ3B0AA5EdXRjaCwgRmxlbWlzaHB0AAJubHEAfgBWc3EAfgBDcHQACER6b25na2hhcHQAAmR6
cQB+AFZzcQB+AENwdAAHRW5nbGlzaHB0AAJlbnEAfgBWc3EAfgBDcHQACUVzcGVyYW50b3B0AAJl
b3EAfgBWc3EAfgBDcHQACEVzdG9uaWFucHQAAmV0cQB+AFZzcQB+AENwdAADRXdlcHQAAmVlcQB+
AFZzcQB+AENwdAAHRmFyb2VzZXB0AAJmb3EAfgBWc3EAfgBDcHQABkZpamlhbnB0AAJmanEAfgBW
c3EAfgBDcHQAB0Zpbm5pc2hwdAACZmlxAH4AVnNxAH4AQ3B0AAZGcmVuY2hwdAACZnJxAH4AVnNx
AH4AQ3B0AAVGdWxhaHB0AAJmZnEAfgBWc3EAfgBDcHQACEdhbGljaWFucHQAAmdscQB+AFZzcQB+
AENwdAAFR2FuZGFwdAACbGdxAH4AVnNxAH4AQ3B0AAhHZW9yZ2lhbnB0AAJrYXEAfgBWc3EAfgBD
cHQABkdlcm1hbnB0AAJkZXEAfgBWc3EAfgBDcHQAB0d1YXJhbmlwdAACZ25xAH4AVnNxAH4AQ3B0
AAhHdWphcmF0aXB0AAJndXEAfgBWc3EAfgBDcHQAD0hhaXRpYW4sIENyZW9sZXB0AAJodHEAfgBW
c3EAfgBDcHQABUhhdXNhcHQAAmhhcQB+AFZzcQB+AENwdAAGSGVicmV3cHQAAmhlcQB+AFZzcQB+
AENwdAAGSGVyZXJvcHQAAmh6cQB+AFZzcQB+AENwdAAFSGluZGlwdAACaGlxAH4AVnNxAH4AQ3B0
AAlIaXJpIE1vdHVwdAACaG9xAH4AVnNxAH4AQ3B0AAlIdW5nYXJpYW5wdAACaHVxAH4AVnNxAH4A
Q3B0AAlJY2VsYW5kaWNwdAACaXNxAH4AVnNxAH4AQ3B0AANJZG9wdAACaW9xAH4AVnNxAH4AQ3B0
AARJZ2JvcHQAAmlncQB+AFZzcQB+AENwdAAKSW5kb25lc2lhbnB0AAJpZHEAfgBWc3EAfgBDcHQA
C0ludGVybGluZ3VhcHQAAmlhcQB+AFZzcQB+AENwdAAXSW50ZXJsaW5ndWUsIE9jY2lkZW50YWxw
dAACaWVxAH4AVnNxAH4AQ3B0AAlJbnVrdGl0dXRwdAACaXVxAH4AVnNxAH4AQ3B0AAdJbnVwaWFx
cHQAAmlrcQB+AFZzcQB+AENwdAAFSXJpc2hwdAACZ2FxAH4AVnNxAH4AQ3B0AAdJdGFsaWFucHQA
Aml0cQB+AFZzcQB+AENwdAAISmFwYW5lc2VwdAACamFxAH4AVnNxAH4AQ3B0AAhKYXZhbmVzZXB0
AAJqdnEAfgBWc3EAfgBDcHQAGEthbGFhbGxpc3V0LCBHcmVlbmxhbmRpY3B0AAJrbHEAfgBWc3EA
fgBDcHQAB0thbm5hZGFwdAACa25xAH4AVnNxAH4AQ3B0AAZLYW51cmlwdAACa3JxAH4AVnNxAH4A
Q3B0AAhLYXNobWlyaXB0AAJrc3EAfgBWc3EAfgBDcHQABkthemFraHB0AAJra3EAfgBWc3EAfgBD
cHQABUtobWVycHQAAmttcQB+AFZzcQB+AENwdAAOS2lrdXl1LCBHaWt1eXVwdAACa2lxAH4AVnNx
AH4AQ3B0AAtLaW55YXJ3YW5kYXB0AAJyd3EAfgBWc3EAfgBDcHQAD0tpcmdoaXosIEt5cmd5enB0
AAJreXEAfgBWc3EAfgBDcHQAB0tpcnVuZGlwdAACcm5xAH4AVnNxAH4AQ3B0AARLb21pcHQAAmt2
cQB+AFZzcQB+AENwdAAFS29uZ29wdAACa2dxAH4AVnNxAH4AQ3B0AAZLb3JlYW5wdAACa29xAH4A
VnNxAH4AQ3B0ABJLdWFueWFtYSwgS3dhbnlhbWFwdAACa2pxAH4AVnNxAH4AQ3B0AAdLdXJkaXNo
cHQAAmt1cQB+AFZzcQB+AENwdAADTGFvcHQAAmxvcQB+AFZzcQB+AENwdAAFTGF0aW5wdAACbGFx
AH4AVnNxAH4AQ3B0AAdMYXR2aWFucHQAAmx2cQB+AFZzcQB+AENwdAAgTGltYnVyZ2FuLCBMaW1i
dXJnZXIsIExpbWJ1cmdpc2hwdAACbGlxAH4AVnNxAH4AQ3B0AAdMaW5nYWxhcHQAAmxucQB+AFZz
cQB+AENwdAAKTGl0aHVhbmlhbnB0AAJsdHEAfgBWc3EAfgBDcHQADEx1YmEtS2F0YW5nYXB0AAJs
dXEAfgBWc3EAfgBDcHQAHEx1eGVtYm91cmdpc2gsIExldHplYnVyZ2VzY2hwdAACbGJxAH4AVnNx
AH4AQ3B0AApNYWNlZG9uaWFucHQAAm1rcQB+AFZzcQB+AENwdAAITWFsYWdhc3lwdAACbWdxAH4A
VnNxAH4AQ3B0AAVNYWxheXB0AAJtc3EAfgBWc3EAfgBDcHQACU1hbGF5YWxhbXB0AAJtbHEAfgBW
c3EAfgBDcHQAB01hbHRlc2VwdAACbXRxAH4AVnNxAH4AQ3B0AARNYW54cHQAAmd2cQB+AFZzcQB+
AENwdAAFTWFvcmlwdAACbWlxAH4AVnNxAH4AQ3B0AAdNYXJhdGhpcHQAAm1ycQB+AFZzcQB+AENw
dAALTWFyc2hhbGxlc2VwdAACbWhxAH4AVnNxAH4AQ3B0AAxNb2Rlcm4gR3JlZWtwdAACZWxxAH4A
VnNxAH4AQ3B0AAlNb25nb2xpYW5wdAACbW5xAH4AVnNxAH4AQ3B0AAVOYXVydXB0AAJuYXEAfgBW
c3EAfgBDcHQADk5hdmFqbywgTmF2YWhvcHQAAm52cQB+AFZzcQB+AENwdAAGTmRvbmdhcHQAAm5n
cQB+AFZzcQB+AENwdAAGTmVwYWxpcHQAAm5lcQB+AFZzcQB+AENwdAANTm9ydGggTmRlYmVsZXB0
AAJuZHEAfgBWc3EAfgBDcHQADU5vcnRoZXJuIFNhbWlwdAACc2VxAH4AVnNxAH4AQ3B0AAlOb3J3
ZWdpYW5wdAACbm9xAH4AVnNxAH4AQ3B0ABFOb3J3ZWdpYW4gQm9rbcOlbHB0AAJuYnEAfgBWc3EA
fgBDcHQAEU5vcndlZ2lhbiBOeW5vcnNrcHQAAm5ucQB+AFZzcQB+AENwdAATT2NjaXRhbiAocG9z
dCAxNTAwKXB0AAJvY3EAfgBWc3EAfgBDcHQABk9qaWJ3YXB0AAJvanEAfgBWc3EAfgBDcHQABU9y
aXlhcHQAAm9ycQB+AFZzcQB+AENwdAAFT3JvbW9wdAACb21xAH4AVnNxAH4AQ3B0ABFPc3NldGlh
biwgT3NzZXRpY3B0AAJvc3EAfgBWc3EAfgBDcHQABFBhbGlwdAACcGlxAH4AVnNxAH4AQ3B0ABBQ
YW5qYWJpLCBQdW5qYWJpcHQAAnBhcQB+AFZzcQB+AENwdAAHUGVyc2lhbnB0AAJmYXEAfgBWc3EA
fgBDcHQABlBvbGlzaHB0AAJwbHEAfgBWc3EAfgBDcHQAClBvcnR1Z3Vlc2VwdAACcHRxAH4AVnNx
AH4AQ3B0AA5QdXNodG8sIFBhc2h0b3B0AAJwc3EAfgBWc3EAfgBDcHQAB1F1ZWNodWFwdAACcXVx
AH4AVnNxAH4AQ3B0AB1Sb21hbmlhbiwgTW9sZGF2aWFuLCBNb2xkb3ZhbnB0AAJyb3EAfgBWc3EA
fgBDcHQAB1JvbWFuc2hwdAACcm1xAH4AVnNxAH4AQ3B0AAdSdXNzaWFucHQAAnJ1cQB+AFZzcQB+
AENwdAAGU2Ftb2FucHQAAnNtcQB+AFZzcQB+AENwdAAFU2FuZ29wdAACc2dxAH4AVnNxAH4AQ3B0
AAhTYW5za3JpdHB0AAJzYXEAfgBWc3EAfgBDcHQACVNhcmRpbmlhbnB0AAJzY3EAfgBWc3EAfgBD
cHQAD1Njb3R0aXNoIEdhZWxpY3B0AAJnZHEAfgBWc3EAfgBDcHQAB1NlcmJpYW5wdAACc3JxAH4A
VnNxAH4AQ3B0AAVTaG9uYXB0AAJzbnEAfgBWc3EAfgBDcHQAEVNpY2h1YW4gWWksIE51b3N1cHQA
AmlpcQB+AFZzcQB+AENwdAAGU2luZGhpcHQAAnNkcQB+AFZzcQB+AENwdAASU2luaGFsYSwgU2lu
aGFsZXNlcHQAAnNpcQB+AFZzcQB+AENwdAAGU2xvdmFrcHQAAnNrcQB+AFZzcQB+AENwdAAJU2xv
dmVuaWFucHQAAnNscQB+AFZzcQB+AENwdAAGU29tYWxpcHQAAnNvcQB+AFZzcQB+AENwdAANU291
dGggTmRlYmVsZXB0AAJucnEAfgBWc3EAfgBDcHQADlNvdXRoZXJuIFNvdGhvcHQAAnN0cQB+AFZz
cQB+AENwdAASU3BhbmlzaCwgQ2FzdGlsaWFucHQAAmVzcQB+AFZzcQB+AENwdAAJU3VuZGFuZXNl
cHQAAnN1cQB+AFZzcQB+AENwdAAHU3dhaGlsaXB0AAJzd3EAfgBWc3EAfgBDcHQABVN3YXRpcHQA
AnNzcQB+AFZzcQB+AENwdAAHU3dlZGlzaHB0AAJzdnEAfgBWc3EAfgBDcHQAB1RhZ2Fsb2dwdAAC
dGxxAH4AVnNxAH4AQ3B0AAhUYWhpdGlhbnB0AAJ0eXEAfgBWc3EAfgBDcHQABVRhamlrcHQAAnRn
cQB+AFZzcQB+AENwdAAFVGFtaWxwdAACdGFxAH4AVnNxAH4AQ3B0AAVUYXRhcnB0AAJ0dHEAfgBW
c3EAfgBDcHQABlRlbHVndXB0AAJ0ZXEAfgBWc3EAfgBDcHQABFRoYWlwdAACdGhxAH4AVnNxAH4A
Q3B0AAdUaWJldGFucHQAAmJvcQB+AFZzcQB+AENwdAAIVGlncmlueWFwdAACdGlxAH4AVnNxAH4A
Q3B0ABVUb25nYSAoVG9uZ2EgSXNsYW5kcylwdAACdG9xAH4AVnNxAH4AQ3B0AAZUc29uZ2FwdAAC
dHNxAH4AVnNxAH4AQ3B0AAZUc3dhbmFwdAACdG5xAH4AVnNxAH4AQ3B0AAdUdXJraXNocHQAAnRy
cQB+AFZzcQB+AENwdAAHVHVya21lbnB0AAJ0a3EAfgBWc3EAfgBDcHQAA1R3aXB0AAJ0d3EAfgBW
c3EAfgBDcHQADlVpZ2h1ciwgVXlnaHVycHQAAnVncQB+AFZzcQB+AENwdAAJVWtyYWluaWFucHQA
AnVrcQB+AFZzcQB+AENwdAAEVXJkdXB0AAJ1cnEAfgBWc3EAfgBDcHQABVV6YmVrcHQAAnV6cQB+
AFZzcQB+AENwdAAFVmVuZGFwdAACdmVxAH4AVnNxAH4AQ3B0AApWaWV0bmFtZXNlcHQAAnZpcQB+
AFZzcQB+AENwdAAIVm9sYXDDvGtwdAACdm9xAH4AVnNxAH4AQ3B0AAdXYWxsb29ucHQAAndhcQB+
AFZzcQB+AENwdAAFV2Vsc2hwdAACY3lxAH4AVnNxAH4AQ3B0AA9XZXN0ZXJuIEZyaXNpYW5wdAAC
ZnlxAH4AVnNxAH4AQ3B0AAVXb2xvZnB0AAJ3b3EAfgBWc3EAfgBDcHQABVhob3NhcHQAAnhocQB+
AFZzcQB+AENwdAAHWWlkZGlzaHB0AAJ5aXEAfgBWc3EAfgBDcHQABllvcnViYXB0AAJ5b3EAfgBW
c3EAfgBDcHQADlpodWFuZywgQ2h1YW5ncHQAAnphcQB+AFZzcQB+AENwdAAEWnVsdXB0AAJ6dXEA
fgBWeHEAfgBIc3IAUGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24u
bW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJENvbXBsZXhUeXBlAAAAAAAAAAECAAJMAAhvcHRpb25h
bHEAfgAkTAAEdHlwZXEAfgAFeHEAfgAjdAApRGV0YWlsIGluZm9ybWF0aW9uIGZvciBlYWNoIGF1
ZGlvIGNoYW5uZWxwc3IAEWphdmEubGFuZy5Cb29sZWFuzSBygNWc+u4CAAFaAAV2YWx1ZXhwAXQA
FWF1ZGlvX2NoYW5uZWxfZGV0YWlsc3B0AAxBdWRpb0NoYW5uZWxzcQB+ACB0ACJJbmRpY2F0ZXMg
dGhlIEFBQyBiaXRzdHJlYW0gZm9ybWF0cHB0AAphYWNfZm9ybWF0c3EAfgBBAAAABncEAAAABnNx
AH4AQ3B0AANSYXdwdAADcmF3cQB+AFZzcQB+AENwdAAKQURUUyBNUEVHNHB0AAphZHRzX21wZWc0
cQB+AFZzcQB+AENwdAAKQURUUyBNUEVHMnB0AAphZHRzX21wZWcycQB+AFZzcQB+AENwdAAEQURJ
RnB0AARhZGlmcQB+AFZzcQB+AENwdAAETEFUTXB0AARsYXRtcQB+AFZzcQB+AENwdAAETE9BU3B0
AARsb2FzcQB+AFZ4cQB+AEhzcQB+ACB0AB9JbmRpY2F0ZXMgdGhlIHByb2ZpbGUgYW5kIGxldmVs
cHB0ABVhYWNfcHJvZmlsZV9hbmRfbGV2ZWxwcQB+AC9zcQB+ACB0ADdJbmRpY2F0ZXMgdGhlIG51
bWJlciBvZiBjaGFubmVscyB3aXRoIGFjdHVhbCBhdWRpbyBkYXRhcHB0ABNudW1fYWN0aXZlX2No
YW5uZWxzcHEAfgAvc3EAfgAgdAAqSW50ZXJwcmV0IHRoZSBzYW1wbGUgYXMgc2lnbmVkIG9yIHVu
c2lnbmVkcHB0AA1zYW1wbGVfc2lnbmVkcHEAfgA8c3EAfgAgdAAZSW5kaWNhdGVzIHRoZSBvYmpl
Y3QgdHlwZXBwdAAPYWFjX29iamVjdF90eXBlcHEAfgAvc3EAfgAgdAAxSW5kaWNhdGVzIHRoZSBt
YXhpbXVtIGJpdCByYXRlIGluIGJpdHMgcGVyIHNlY29uZHBwdAAQbWF4aW11bV9iaXRfcmF0ZXBx
AH4AL3NxAH4AIHQAI1RvdGFsIGxlbmd0aCBvZiB0aGUgc3RyZWFtIGlmIGtub3ducHB0AA5sZW5n
dGhPZlN0cmVhbXB+cQB+ACh0AARMT05Hc3EAfgAgdAAySW5kaWNhdGVzIHRoZSBzaXplIG9mIHRo
ZSBkZWNvZGluZyBidWZmZXIgaW4gYnl0ZXNwcHQAFGRlY29kaW5nX2J1ZmZlcl9zaXplcHEAfgAv
c3EAfgAgdABJVGhlIHRpbWUgcGVydGFpbmluZyB0byB0aGUgZW5kIG9mIHRoZSBkYXRhICh0aW1l
ICsgZHVyYXRpb24gb2YgdGhpcyBkYXRhKXBwdAAHdGltZUVuZHBxAH4AKnNxAH4AIHQARkxldmVs
IG9mIHByZWNpc2lvbiAtIGNhbiBiZSBsb3dlciB0aGFuIHRoZSBhY3R1YWwgbnVtYmVyIG9mIHZh
bGlkIGJpdHNwcHQAGGFjY3VyYWN5X2JpdHNfcGVyX3NhbXBsZXBxAH4AL3NxAH4AIHQAMVRvdGFs
IG51bWJlciBvZiB2YWxpZCBhbmQgaW52YWxpZCBiaXRzIHBlciBzYW1wbGVwcHQAF3N0b3JhZ2Vf
Yml0c19wZXJfc2FtcGxlcHEAfgAvc3EAfgAgdAAvSW5kaWNhdGVzIGlmIHRoZSBzdHJlYW0gaGFz
IGEgY29uc3RhbnQgYml0IHJhdGVwcHQAEWNvbnN0YW50X2JpdF9yYXRlcHEAfgA8c3EAfgAgdAAz
UG9zaXRpb24gb3Igb2Zmc2V0IGZyb20gdGhlIGJlZ2lubmluZyBvZiB0aGUgc3RyZWFtcHB0ABBw
b3NpdGlvbkluU3RyZWFtcHEAfgKxc3EAfgAgcHBwdAALc2FtcGxlX3JhdGVwfnEAfgAodAAIUkFU
SU9OQUxzcQB+ACB0ADFJbmRpY2F0ZXMgdGhlIG51bWJlciBvZiBzYW1wbGVzIGluIGVhY2ggQUFD
IGZyYW1lcHB0ABVhYWNfc2FtcGxlc19wZXJfZnJhbWVzcQB+AEEAAAACdwQAAAACc3EAfgBDcHQA
Azk2MHBxAH4CznEAfgBWc3EAfgBDcHQABDEwMjRwcQB+AtBxAH4AVnhxAH4ASHNxAH4AIHQAH051
bWJlciBvZiB2YWxpZCBiaXRzIHBlciBzYW1wbGVwcHQAD2JpdHNfcGVyX3NhbXBsZXBxAH4AL3Nx
AH4AIHQALVRoZSBtb3N0IHJlbGV2YW50IHRpbWUgcGVydGFpbmluZyB0byB0aGUgZGF0YXBwdAAE
dGltZXBxAH4AKnhwc3IAEWphdmEudXRpbC5IYXNoTWFwBQfawcMWYNEDAAJGAApsb2FkRmFjdG9y
SQAJdGhyZXNob2xkeHA/AAAAAAAAIHcIAAAAQAAAABNxAH4ALnNyABFqYXZhLmxhbmcuSW50ZWdl
chLioKT3gYc4AgABSQAFdmFsdWV4cgAQamF2YS5sYW5nLk51bWJlcoaslR0LlOCLAgAAeHAAAAAC
cQB+ACdzcgAkY2EuZGlnaXRhbHJhcGlkcy5rYXlhay50aW1lLlRpbWVJbXBsAAAAAAAAAAECAAJM
AAhyYXRpb25hbHQAMUxjYS9kaWdpdGFscmFwaWRzL2theWFrL2RhdGEvaW1wbC9SYXRpb25hbE51
bWJlcjtMAAh0aW1lQmFzZXQAJkxjYS9kaWdpdGFscmFwaWRzL2theWFrL3RpbWUvVGltZUJhc2U7
eHBzcgAvY2EuZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhLmltcGwuUmF0aW9uYWxOdW1iZXIAAAAA
AAAAAQIABEoAC2Rlbm9taW5hdG9yWgAJaXNSZWR1Y2VkWgATbmVlZHNCaWdGb3JNdWx0aXBseUoA
CW51bWVyYXRvcnhxAH4C2gAAAAAAALuAAAAAAAAAAIf4AHNyAChjYS5kaWdpdGFscmFwaWRzLmth
eWFrLnRpbWUuVGltZUJhc2VJbXBsAAAAAAAAAAECAAFMAA5vZmZzZXRSYXRpb25hbHEAfgLdeHBz
cQB+AuAAAAAAO5rKAAAAAAAAAAAAAABxAH4AOHNxAH4C2QAC475xAH4ATHVyAAJbQqzzF/gGCFTg
AgAAeHAAAAAkA4CAgB8AQBAEgICAFEAVAAYAAAPyvgAC474FgICAAhGQBgECcQB+AodzcgAmamF2
YS51dGlsLkNvbGxlY3Rpb25zJFVubW9kaWZpYWJsZUxpc3T8DyUxteyOEAIAAUwABGxpc3RxAH4A
IXhyACxqYXZhLnV0aWwuQ29sbGVjdGlvbnMkVW5tb2RpZmlhYmxlQ29sbGVjdGlvbhlCAIDLXvce
AgABTAABY3QAFkxqYXZhL3V0aWwvQ29sbGVjdGlvbjt4cHNxAH4AQQAAAAJ3BAAAAAJzcQB+AAAA
c3EAfgAEdAAqR2VuZXJhbCBpbmZvcm1hdGlvbiBhYm91dCBhbiBBdWRpbyBDaGFubmVsc3IAJWph
dmEudXRpbC5Db2xsZWN0aW9ucyRVbm1vZGlmaWFibGVTZXSAHZLRj5uAVQIAAHhxAH4C6XNxAH4A
CHcMAAAAED9AAAAAAAABdAAITGFuZ3VhZ2V4c3EAfgLwc3EAfgAIdwwAAAAQP0AAAAAAAARxAH4A
T3NxAH4CgnBwcHQAF2NoYW5uZWxfZm9ybWF0X292ZXJyaWRlcQB+AoZ0AAZTdHJlYW1zcQB+ACB0
ABNTcGVha2VyIGRlc2lnbmF0aW9ucHB0ABhjaGFubmVsX3NwZWFrZXJfcG9zaXRpb25zcQB+AEEA
AAAedwQAAAAec3EAfgBDcHQABE1vbm9wdAAETU9OT3EAfgBWc3EAfgBDcHQABkNlbnRlcnB0AAhD
X0NFTlRFUnEAfgBWc3EAfgBDcHQABExlZnRwdAAGTF9MRUZUcQB+AFZzcQB+AENwdAAFUmlnaHRw
dAAHUl9SSUdIVHEAfgBWc3EAfgBDcHQAB0x0IExlZnRwdAAHTHRfTEVGVHEAfgBWc3EAfgBDcHQA
CFJ0IFJpZ2h0cHQACFJ0X1JJR0hUcQB+AFZzcQB+AENwdAAPQ2VudGVyIHN1cnJvdW5kcHQAEkNz
X0NFTlRFUl9TVVJST1VORHEAfgBWc3EAfgBDcHQADUxlZnQgc3Vycm91bmRwdAAQTHNfTEVGVF9T
VVJST1VORHEAfgBWc3EAfgBDcHQADlJpZ2h0IHN1cnJvdW5kcHQAEVJzX1JJR0hUX1NVUlJPVU5E
cQB+AFZzcQB+AENwdAANTG93IGZyZXF1ZW5jeXB0ABFMRkVfTE9XX0ZSRVFVRU5DWXEAfgBWc3EA
fgBDcHQAElJlYXIgbGVmdCBzdXJyb3VuZHB0ABZSbHNfUkVBUl9MRUZUX1NVUlJPVU5EcQB+AFZz
cQB+AENwdAATUmVhciByaWdodCBzdXJyb3VuZHB0ABdScnNfUkVBUl9SSUdIVF9TVVJST1VORHEA
fgBWc3EAfgBDcHQAElNpZGUgbGVmdCBzdXJyb3VuZHB0ABZTbHNfU0lERV9MRUZUX1NVUlJPVU5E
cQB+AFZzcQB+AENwdAATU2lkZSByaWdodCBzdXJyb3VuZHB0ABdTcnNfU0lERV9SSUdIVF9TVVJS
T1VORHEAfgBWc3EAfgBDcHQAC0xlZnQgY2VudGVycHQADkxjX0xFRlRfQ0VOVEVScQB+AFZzcQB+
AENwdAAMUmlnaHQgY2VudGVycHQAD1JjX1JJR0hUX0NFTlRFUnEAfgBWc3EAfgBDcHQACUxlZnQg
d2lkZXB0AAxMd19MRUZUX1dJREVxAH4AVnNxAH4AQ3B0AApSaWdodCB3aWRlcHQADVJ3X1JJR0hU
X1dJREVxAH4AVnNxAH4AQ3B0ABlMZWZ0IHN1cnJvdW5kIGRpcmVjdGlvbmFscHQAHUxzZF9MRUZU
X1NVUlJPVU5EX0RJUkVDVElPTkFMcQB+AFZzcQB+AENwdAAaUmlnaHQgc3Vycm91bmQgZGlyZWN0
aW9uYWxwdAAeUnNkX1JJR0hUX1NVUlJPVU5EX0RJUkVDVElPTkFMcQB+AFZzcQB+AENwdAAPTG93
IGZyZXF1ZW5jeSAycHQAEkxGRTJfTE9XX0ZSRVFVRU5DWXEAfgBWc3EAfgBDcHQAE1RvcCBjZW50
ZXIgc3Vycm91bmRwdAAWVHNfVE9QX0NFTlRFUl9TVVJST1VORHEAfgBWc3EAfgBDcHQAFlZlcnRp
Y2FsIGhlaWdodCBjZW50ZXJwdAAaVmhjX1ZFUlRJQ0FMX0hFSUdIVF9DRU5URVJxAH4AVnNxAH4A
Q3B0ABRWZXJ0aWNhbCBoZWlnaHQgbGVmdHB0ABhWaGxfVkVSVElDQUxfSEVJR0hUX0xFRlRxAH4A
VnNxAH4AQ3B0ABVWZXJ0aWNhbCBoZWlnaHQgcmlnaHRwdAAZVmhyX1ZFUlRJQ0FMX0hFSUdIVF9S
SUdIVHEAfgBWc3EAfgBDcHQAGVZlcnRpY2FsIGhlaWdodCBzaWRlIGxlZnRwdAAeVmhzbF9WRVJU
SUNBTF9IRUlHSFRfU0lERV9MRUZUcQB+AFZzcQB+AENwdAAaVmVydGljYWwgaGVpZ2h0IHNpZGUg
cmlnaHRwdAAfVmhzcl9WRVJUSUNBTF9IRUlHSFRfU0lERV9SSUdIVHEAfgBWc3EAfgBDcHQAG1Zl
cnRpY2FsIGhlaWdodCByZWFyIGNlbnRlcnB0ACBWaHJjX1ZFUlRJQ0FMX0hFSUdIVF9SRUFSX0NF
TlRFUnEAfgBWc3EAfgBDcHQAGVZlcnRpY2FsIGhlaWdodCByZWFyIGxlZnRwdAAeVmhybF9WRVJU
SUNBTF9IRUlHSFRfUkVBUl9MRUZUcQB+AFZzcQB+AENwdAAaVmVydGljYWwgaGVpZ2h0IHJlYXIg
cmlnaHRwdAAfVmhycl9WRVJUSUNBTF9IRUlHSFRfUkVBUl9SSUdIVHEAfgBWeHEAfgBIc3EAfgAg
cHBwdAANY2hhbm5lbF9ncm91cHBxAH4ASHh0AAxBdWRpb0NoYW5uZWxzcQB+Atc/AAAAAAAAIHcI
AAAAQAAAAAJxAH4AUXQAAmVucQB+Avt0AAZMX0xFRlR4c3EAfgAAAHEAfgLuc3EAfgLXPwAAAAAA
ACB3CAAAAEAAAAACcQB+AFF0AAJlbnEAfgL7dAAHUl9SSUdIVHh4cQB+AuxxAH4AUXQAAmVucQB+
AotxAH4CknEAfgKhc3EAfgLZAAAAKXEAfgKkcQB+AttxAH4Cp3NxAH4ChQBxAH4CqnEAfgLbcQB+
Aq1zcQB+AtkAA/K+cQB+ArVzcQB+AtkAAAYAcQB+ArtzcQB+AtkAAAAQcQB+Ar5xAH4DZnEAfgLB
cQB+A2NxAH4CxnNxAH4C4AAAAAAAAAABAQAAAAAAAAC7gHEAfgLLdAAEMTAyNHEAfgLTcQB+A2Z4
</property>
                        <pinDefinition name="CompressedAudio" displayName="Compressed Audio (AAC)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="UncompressedVideo" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAQD9AAAAAAAAgdAAIT3ZlcnNjYW50AAZFbmRpYW50ABZWaWRl
b1NhbXBsZUluZm9ybWF0aW9udAARVW5jb21wcmVzc2VkSW1hZ2V0ABBWaWRlb0ludGVybGFjaW5n
dAALTWVkaWFUaW1pbmd0ABBNZWRpYVJhdGVDb250cm9sdAAOU2NhbmxpbmVTdHJpZGV0AAVJbWFn
ZXQAC1Jhc3RlckltYWdldAAITGFuZ3VhZ2V0AAhUZW1wb3JhbHQACkJ5dGVTdHJlYW10ABFVbmNv
bXByZXNzZWRWaWRlb3QABlN0cmVhbXQAC1BpeGVsRm9ybWF0dAALVmlkZW9TYW1wbGV0AAlGcmFt
ZVJhdGV0ABFJbWFnZVRyYW5zcGFyZW5jeXQABlJhc3RlcnQAC1ZpZGVvU3RyZWFtdAAOSW1hZ2VE
aW1lbnNpb250ABFTYW1wbGVJbmZvcm1hdGlvbnQAEkRhdGFJc01hbnVmYWN0dXJlZHQAF1VuY29t
cHJlc3NlZFZpZGVvU3RyZWFtdAAMU2FtcGxlRm9ybWF0dAAOVmlkZW9EaW1lbnNpb250AAtBc3Bl
Y3RSYXRpb3QAC01lZGlhU3RyZWFtdAALS2F5YWtCdWZmZXJ0ABdVbmNvbXByZXNzZWRWaWRlb1Nh
bXBsZXQABVZpZGVveHNxAH4ACHcMAAAAQD9AAAAAAAApc3IAT2NhLmRpZ2l0YWxyYXBpZHMua2F5
YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJFNpbXBsZVR5
cGUAAAAAAAAAAQIAAkwAEWVudW1lcmF0aW9uVmFsdWVzdAAQTGphdmEvdXRpbC9MaXN0O0wABHR5
cGV0AENMY2EvZGlnaXRhbHJhcGlkcy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9T
aW1wbGVUeXBlc0VudW07eHIAUmNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmlu
aXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJEtleURlZmluaXRpb24AAAAAAAAAAQIABEwA
B2NvbW1lbnRxAH4ABUwAC2Rpc3BsYXlOYW1lcQB+AAVMAAttdWx0aVZhbHVlZHQAE0xqYXZhL2xh
bmcvQm9vbGVhbjtMAARuYW1lcQB+AAV4cHQAHEluZGljYXRlcyB0aGUgbWVkaWEgZHVyYXRpb25w
cHQADm1lZGlhX2R1cmF0aW9ucH5yAEFjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5k
ZWZpbml0aW9uLm1vZGVsLlNpbXBsZVR5cGVzRW51bQAAAAAAAAAAEgAAeHIADmphdmEubGFuZy5F
bnVtAAAAAAAAAAASAAB4cHQABFRJTUVzcQB+ACt0ADFJbmRpY2F0ZXMgdGhlIGF2ZXJhZ2UgYml0
IHJhdGUgaW4gYml0cyBwZXIgc2Vjb25kcHB0ABBhdmVyYWdlX2JpdF9yYXRlcH5xAH4AM3QAB0lO
VEVHRVJzcQB+ACt0ACpJbmRpY2F0ZXMgdGhlIGludGVybGFjaW5nIGFuZCBmaWVsZCBsYXlvdXRw
cHQADGZyYW1lX2xheW91dHNyABNqYXZhLnV0aWwuQXJyYXlMaXN0eIHSHZnHYZ0DAAFJAARzaXpl
eHAAAAADdwQAAAADc3IAN2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsucGx1Z2luLnhtbC5LYXlha0Vu
dW1lcmF0aW9uVmFsdWUAAAAAAAAAAQIABUwAC2Rlc2NyaXB0aW9ucQB+AAVMAAtkaXNwbGF5TmFt
ZXEAfgAFTAAGaGlkZGVucQB+AC9MAA52YWx1ZUF0dHJpYnV0ZXEAfgAFTAANdmFsdWVFbWJlZGRl
ZHEAfgAFeHBwdAALUHJvZ3Jlc3NpdmVwdAALcHJvZ3Jlc3NpdmV0AABzcQB+AEFwdAAKSW50ZXJs
YWNlZHB0AAppbnRlcmxhY2VkcQB+AEVzcQB+AEFwdAAMU2luZ2xlIGZpZWxkcHQADHNpbmdsZV9m
aWVsZHEAfgBFeH5xAH4AM3QABlNUUklOR3NxAH4AK3QAQVRydWUgb24gdGhlIGxhc3QgZGF0YSBw
YWNrZXQgb2YgdGhlIFN0cmVhbSAod2l0aCBvciB3aXRob3V0IGRhdGEpcHB0AAtlbmRPZlN0cmVh
bXB+cQB+ADN0AAdCT09MRUFOc3IAUGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRl
ZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJENvbXBsZXhUeXBlAAAAAAAAAAECAAJM
AAhvcHRpb25hbHEAfgAvTAAEdHlwZXEAfgAFeHEAfgAudAAtRGV0YWlsIGluZm9ybWF0aW9uIG9u
IHRoZSBwbGFuZXMgKHNpbmNlIHYxLjEpcHNyABFqYXZhLmxhbmcuQm9vbGVhbs0gcoDVnPruAgAB
WgAFdmFsdWV4cAF0ABNwbGFuYXJfaW1hZ2VfcGxhbmVzcHQAF1BpeGVsRm9ybWF0UGxhbmVEZXRh
aWxzc3EAfgArdABNVGhlIG51bWJlciBvZiBieXRlcyBmcm9tIHRoZSBzdGFydCBvZiBvbmUgc2Nh
bmxpbmUsIHRvIHRoZSBzdGFydCBvZiB0aGUgbmV4dC5wcHQAD3NjYW5saW5lX3N0cmlkZXBxAH4A
OnNxAH4AK3QALEluZGljYXRlcyBpZiB0aGUgZnJhbWUgcmF0ZSByZW1haW5zIGNvbnN0YW50cHB0
ABNjb25zdGFudF9mcmFtZV9yYXRlcHEAfgBRc3EAfgArdABwV2hlbiB0aGUgZnJhbWVfbGF5b3V0
PXNpbmdsZV9maWVsZCwgaW5kaWNhdGVzIGlmIGZpZWxkIDEgb3IgZmllbGQgMiAoaW4gdGhlIHBy
ZXNlbnRhdGlvbiBvcmRlcikgaXMgdGhlIHRvcCBmaWVsZHBwdAANZmllbGRfdG9wbmVzc3NxAH4A
PwAAAAJ3BAAAAAJzcQB+AEFwdAABMXBxAH4AZXEAfgBFc3EAfgBBcHQAATJwcQB+AGdxAH4ARXhx
AH4ATHNxAH4AK3QAF0luZGljYXRlcyBieXRlIG9yZGVyaW5ncHB0AAZlbmRpYW5zcQB+AD8AAAAC
dwQAAAACc3EAfgBBcHBwcHQAA2JpZ3NxAH4AQXBwcHB0AAZsaXR0bGV4cQB+AExzcQB+ACt0ADRJ
bmRpY2F0ZXMgdGhlIHR5cGUgb2YgcHVsbGRvd24gYXBwbGllZCB0byB0aGUgdmlkZW8ucHB0AAhw
dWxsZG93bnNxAH4APwAAAAV3BAAAAAVzcQB+AEFwdAAETm9uZXB0AARub25lcQB+AEVzcQB+AEFw
dAADMjoycHQAAzJfMnEAfgBFc3EAfgBBcHQABzI6MjoyOjRwdAAHMl8yXzJfNHEAfgBFc3EAfgBB
cHQAAzI6M3B0AAMyXzNxAH4ARXNxAH4AQXB0AAcyOjM6MzoycHQABzJfM18zXzJxAH4ARXhxAH4A
THNxAH4AK3QAF0Nhbm9uaWNhbCBMYW5ndWFnZSBDb2RlcHB0AA1sYW5ndWFnZV9jb2Rlc3EAfgA/
AAAAuncEAAAAunNxAH4AQXB0AAlVbmRlZmluZWRwdAADdW5kcQB+AEVzcQB+AEFwdAAOTm90IEFw
cGxpY2FibGVwdAADenh4cQB+AEVzcQB+AEFwdAARQWJraGF6aWFuLCBBYmtoYXpwdAACYWJxAH4A
RXNxAH4AQXB0AARBZmFycHQAAmFhcQB+AEVzcQB+AEFwdAAJQWZyaWthYW5zcHQAAmFmcQB+AEVz
cQB+AEFwdAAEQWthbnB0AAJha3EAfgBFc3EAfgBBcHQACEFsYmFuaWFucHQAAnNxcQB+AEVzcQB+
AEFwdAAHQW1oYXJpY3B0AAJhbXEAfgBFc3EAfgBBcHQABkFyYWJpY3B0AAJhcnEAfgBFc3EAfgBB
cHQACUFyYWdvbmVzZXB0AAJhbnEAfgBFc3EAfgBBcHQACEFybWVuaWFucHQAAmh5cQB+AEVzcQB+
AEFwdAAIQXNzYW1lc2VwdAACYXNxAH4ARXNxAH4AQXB0AAZBdmFyaWNwdAACYXZxAH4ARXNxAH4A
QXB0AAdBdmVzdGFucHQAAmFlcQB+AEVzcQB+AEFwdAAGQXltYXJhcHQAAmF5cQB+AEVzcQB+AEFw
dAALQXplcmJhaWphbmlwdAACYXpxAH4ARXNxAH4AQXB0AAdCYW1iYXJhcHQAAmJtcQB+AEVzcQB+
AEFwdAAHQmFzaGtpcnB0AAJiYXEAfgBFc3EAfgBBcHQABkJhc3F1ZXB0AAJldXEAfgBFc3EAfgBB
cHQACkJlbGFydXNpYW5wdAACYmVxAH4ARXNxAH4AQXB0AAdCZW5nYWxpcHQAAmJucQB+AEVzcQB+
AEFwdAAQQmloYXJpIExhbmd1YWdlc3B0AAJiaHEAfgBFc3EAfgBBcHQAB0Jpc2xhbWFwdAACYmlx
AH4ARXNxAH4AQXB0AAdCb3NuaWFucHQAAmJzcQB+AEVzcQB+AEFwdAAGQnJldG9ucHQAAmJycQB+
AEVzcQB+AEFwdAAJQnVsZ2FyaWFucHQAAmJncQB+AEVzcQB+AEFwdAAHQnVybWVzZXB0AAJteXEA
fgBFc3EAfgBBcHQAEkNhdGFsYW4sIFZhbGVuY2lhbnB0AAJjYXEAfgBFc3EAfgBBcHQACENoYW1v
cnJvcHQAAmNocQB+AEVzcQB+AEFwdAAHQ2hlY2hlbnB0AAJjZXEAfgBFc3EAfgBBcHQAF0NoaWNo
ZXdhLCBDaGV3YSwgTnlhbmphcHQAAm55cQB+AEVzcQB+AEFwdAAHQ2hpbmVzZXB0AAJ6aHEAfgBF
c3EAfgBBcHQAHENodXJjaCBTbGF2aWMsIE9sZCBCdWxnYXJpYW5wdAACY3VxAH4ARXNxAH4AQXB0
AAdDaHV2YXNocHQAAmN2cQB+AEVzcQB+AEFwdAAHQ29ybmlzaHB0AAJrd3EAfgBFc3EAfgBBcHQA
CENvcnNpY2FucHQAAmNvcQB+AEVzcQB+AEFwdAAEQ3JlZXB0AAJjcnEAfgBFc3EAfgBBcHQACENy
b2F0aWFucHQAAmhycQB+AEVzcQB+AEFwdAAFQ3plY2hwdAACY3NxAH4ARXNxAH4AQXB0AAZEYW5p
c2hwdAACZGFxAH4ARXNxAH4AQXB0ABpEaXZlaGksIERoaXZlaGksIE1hbGRpdmlhbnB0AAJkdnEA
fgBFc3EAfgBBcHQADkR1dGNoLCBGbGVtaXNocHQAAm5scQB+AEVzcQB+AEFwdAAIRHpvbmdraGFw
dAACZHpxAH4ARXNxAH4AQXB0AAdFbmdsaXNocHQAAmVucQB+AEVzcQB+AEFwdAAJRXNwZXJhbnRv
cHQAAmVvcQB+AEVzcQB+AEFwdAAIRXN0b25pYW5wdAACZXRxAH4ARXNxAH4AQXB0AANFd2VwdAAC
ZWVxAH4ARXNxAH4AQXB0AAdGYXJvZXNlcHQAAmZvcQB+AEVzcQB+AEFwdAAGRmlqaWFucHQAAmZq
cQB+AEVzcQB+AEFwdAAHRmlubmlzaHB0AAJmaXEAfgBFc3EAfgBBcHQABkZyZW5jaHB0AAJmcnEA
fgBFc3EAfgBBcHQABUZ1bGFocHQAAmZmcQB+AEVzcQB+AEFwdAAIR2FsaWNpYW5wdAACZ2xxAH4A
RXNxAH4AQXB0AAVHYW5kYXB0AAJsZ3EAfgBFc3EAfgBBcHQACEdlb3JnaWFucHQAAmthcQB+AEVz
cQB+AEFwdAAGR2VybWFucHQAAmRlcQB+AEVzcQB+AEFwdAAHR3VhcmFuaXB0AAJnbnEAfgBFc3EA
fgBBcHQACEd1amFyYXRpcHQAAmd1cQB+AEVzcQB+AEFwdAAPSGFpdGlhbiwgQ3Jlb2xlcHQAAmh0
cQB+AEVzcQB+AEFwdAAFSGF1c2FwdAACaGFxAH4ARXNxAH4AQXB0AAZIZWJyZXdwdAACaGVxAH4A
RXNxAH4AQXB0AAZIZXJlcm9wdAACaHpxAH4ARXNxAH4AQXB0AAVIaW5kaXB0AAJoaXEAfgBFc3EA
fgBBcHQACUhpcmkgTW90dXB0AAJob3EAfgBFc3EAfgBBcHQACUh1bmdhcmlhbnB0AAJodXEAfgBF
c3EAfgBBcHQACUljZWxhbmRpY3B0AAJpc3EAfgBFc3EAfgBBcHQAA0lkb3B0AAJpb3EAfgBFc3EA
fgBBcHQABElnYm9wdAACaWdxAH4ARXNxAH4AQXB0AApJbmRvbmVzaWFucHQAAmlkcQB+AEVzcQB+
AEFwdAALSW50ZXJsaW5ndWFwdAACaWFxAH4ARXNxAH4AQXB0ABdJbnRlcmxpbmd1ZSwgT2NjaWRl
bnRhbHB0AAJpZXEAfgBFc3EAfgBBcHQACUludWt0aXR1dHB0AAJpdXEAfgBFc3EAfgBBcHQAB0lu
dXBpYXFwdAACaWtxAH4ARXNxAH4AQXB0AAVJcmlzaHB0AAJnYXEAfgBFc3EAfgBBcHQAB0l0YWxp
YW5wdAACaXRxAH4ARXNxAH4AQXB0AAhKYXBhbmVzZXB0AAJqYXEAfgBFc3EAfgBBcHQACEphdmFu
ZXNlcHQAAmp2cQB+AEVzcQB+AEFwdAAYS2FsYWFsbGlzdXQsIEdyZWVubGFuZGljcHQAAmtscQB+
AEVzcQB+AEFwdAAHS2FubmFkYXB0AAJrbnEAfgBFc3EAfgBBcHQABkthbnVyaXB0AAJrcnEAfgBF
c3EAfgBBcHQACEthc2htaXJpcHQAAmtzcQB+AEVzcQB+AEFwdAAGS2F6YWtocHQAAmtrcQB+AEVz
cQB+AEFwdAAFS2htZXJwdAACa21xAH4ARXNxAH4AQXB0AA5LaWt1eXUsIEdpa3V5dXB0AAJraXEA
fgBFc3EAfgBBcHQAC0tpbnlhcndhbmRhcHQAAnJ3cQB+AEVzcQB+AEFwdAAPS2lyZ2hpeiwgS3ly
Z3l6cHQAAmt5cQB+AEVzcQB+AEFwdAAHS2lydW5kaXB0AAJybnEAfgBFc3EAfgBBcHQABEtvbWlw
dAACa3ZxAH4ARXNxAH4AQXB0AAVLb25nb3B0AAJrZ3EAfgBFc3EAfgBBcHQABktvcmVhbnB0AAJr
b3EAfgBFc3EAfgBBcHQAEkt1YW55YW1hLCBLd2FueWFtYXB0AAJranEAfgBFc3EAfgBBcHQAB0t1
cmRpc2hwdAACa3VxAH4ARXNxAH4AQXB0AANMYW9wdAACbG9xAH4ARXNxAH4AQXB0AAVMYXRpbnB0
AAJsYXEAfgBFc3EAfgBBcHQAB0xhdHZpYW5wdAACbHZxAH4ARXNxAH4AQXB0ACBMaW1idXJnYW4s
IExpbWJ1cmdlciwgTGltYnVyZ2lzaHB0AAJsaXEAfgBFc3EAfgBBcHQAB0xpbmdhbGFwdAACbG5x
AH4ARXNxAH4AQXB0AApMaXRodWFuaWFucHQAAmx0cQB+AEVzcQB+AEFwdAAMTHViYS1LYXRhbmdh
cHQAAmx1cQB+AEVzcQB+AEFwdAAcTHV4ZW1ib3VyZ2lzaCwgTGV0emVidXJnZXNjaHB0AAJsYnEA
fgBFc3EAfgBBcHQACk1hY2Vkb25pYW5wdAACbWtxAH4ARXNxAH4AQXB0AAhNYWxhZ2FzeXB0AAJt
Z3EAfgBFc3EAfgBBcHQABU1hbGF5cHQAAm1zcQB+AEVzcQB+AEFwdAAJTWFsYXlhbGFtcHQAAm1s
cQB+AEVzcQB+AEFwdAAHTWFsdGVzZXB0AAJtdHEAfgBFc3EAfgBBcHQABE1hbnhwdAACZ3ZxAH4A
RXNxAH4AQXB0AAVNYW9yaXB0AAJtaXEAfgBFc3EAfgBBcHQAB01hcmF0aGlwdAACbXJxAH4ARXNx
AH4AQXB0AAtNYXJzaGFsbGVzZXB0AAJtaHEAfgBFc3EAfgBBcHQADE1vZGVybiBHcmVla3B0AAJl
bHEAfgBFc3EAfgBBcHQACU1vbmdvbGlhbnB0AAJtbnEAfgBFc3EAfgBBcHQABU5hdXJ1cHQAAm5h
cQB+AEVzcQB+AEFwdAAOTmF2YWpvLCBOYXZhaG9wdAACbnZxAH4ARXNxAH4AQXB0AAZOZG9uZ2Fw
dAACbmdxAH4ARXNxAH4AQXB0AAZOZXBhbGlwdAACbmVxAH4ARXNxAH4AQXB0AA1Ob3J0aCBOZGVi
ZWxlcHQAAm5kcQB+AEVzcQB+AEFwdAANTm9ydGhlcm4gU2FtaXB0AAJzZXEAfgBFc3EAfgBBcHQA
CU5vcndlZ2lhbnB0AAJub3EAfgBFc3EAfgBBcHQAEU5vcndlZ2lhbiBCb2ttw6VscHQAAm5icQB+
AEVzcQB+AEFwdAARTm9yd2VnaWFuIE55bm9yc2twdAACbm5xAH4ARXNxAH4AQXB0ABNPY2NpdGFu
IChwb3N0IDE1MDApcHQAAm9jcQB+AEVzcQB+AEFwdAAGT2ppYndhcHQAAm9qcQB+AEVzcQB+AEFw
dAAFT3JpeWFwdAACb3JxAH4ARXNxAH4AQXB0AAVPcm9tb3B0AAJvbXEAfgBFc3EAfgBBcHQAEU9z
c2V0aWFuLCBPc3NldGljcHQAAm9zcQB+AEVzcQB+AEFwdAAEUGFsaXB0AAJwaXEAfgBFc3EAfgBB
cHQAEFBhbmphYmksIFB1bmphYmlwdAACcGFxAH4ARXNxAH4AQXB0AAdQZXJzaWFucHQAAmZhcQB+
AEVzcQB+AEFwdAAGUG9saXNocHQAAnBscQB+AEVzcQB+AEFwdAAKUG9ydHVndWVzZXB0AAJwdHEA
fgBFc3EAfgBBcHQADlB1c2h0bywgUGFzaHRvcHQAAnBzcQB+AEVzcQB+AEFwdAAHUXVlY2h1YXB0
AAJxdXEAfgBFc3EAfgBBcHQAHVJvbWFuaWFuLCBNb2xkYXZpYW4sIE1vbGRvdmFucHQAAnJvcQB+
AEVzcQB+AEFwdAAHUm9tYW5zaHB0AAJybXEAfgBFc3EAfgBBcHQAB1J1c3NpYW5wdAACcnVxAH4A
RXNxAH4AQXB0AAZTYW1vYW5wdAACc21xAH4ARXNxAH4AQXB0AAVTYW5nb3B0AAJzZ3EAfgBFc3EA
fgBBcHQACFNhbnNrcml0cHQAAnNhcQB+AEVzcQB+AEFwdAAJU2FyZGluaWFucHQAAnNjcQB+AEVz
cQB+AEFwdAAPU2NvdHRpc2ggR2FlbGljcHQAAmdkcQB+AEVzcQB+AEFwdAAHU2VyYmlhbnB0AAJz
cnEAfgBFc3EAfgBBcHQABVNob25hcHQAAnNucQB+AEVzcQB+AEFwdAARU2ljaHVhbiBZaSwgTnVv
c3VwdAACaWlxAH4ARXNxAH4AQXB0AAZTaW5kaGlwdAACc2RxAH4ARXNxAH4AQXB0ABJTaW5oYWxh
LCBTaW5oYWxlc2VwdAACc2lxAH4ARXNxAH4AQXB0AAZTbG92YWtwdAACc2txAH4ARXNxAH4AQXB0
AAlTbG92ZW5pYW5wdAACc2xxAH4ARXNxAH4AQXB0AAZTb21hbGlwdAACc29xAH4ARXNxAH4AQXB0
AA1Tb3V0aCBOZGViZWxlcHQAAm5ycQB+AEVzcQB+AEFwdAAOU291dGhlcm4gU290aG9wdAACc3Rx
AH4ARXNxAH4AQXB0ABJTcGFuaXNoLCBDYXN0aWxpYW5wdAACZXNxAH4ARXNxAH4AQXB0AAlTdW5k
YW5lc2VwdAACc3VxAH4ARXNxAH4AQXB0AAdTd2FoaWxpcHQAAnN3cQB+AEVzcQB+AEFwdAAFU3dh
dGlwdAACc3NxAH4ARXNxAH4AQXB0AAdTd2VkaXNocHQAAnN2cQB+AEVzcQB+AEFwdAAHVGFnYWxv
Z3B0AAJ0bHEAfgBFc3EAfgBBcHQACFRhaGl0aWFucHQAAnR5cQB+AEVzcQB+AEFwdAAFVGFqaWtw
dAACdGdxAH4ARXNxAH4AQXB0AAVUYW1pbHB0AAJ0YXEAfgBFc3EAfgBBcHQABVRhdGFycHQAAnR0
cQB+AEVzcQB+AEFwdAAGVGVsdWd1cHQAAnRlcQB+AEVzcQB+AEFwdAAEVGhhaXB0AAJ0aHEAfgBF
c3EAfgBBcHQAB1RpYmV0YW5wdAACYm9xAH4ARXNxAH4AQXB0AAhUaWdyaW55YXB0AAJ0aXEAfgBF
c3EAfgBBcHQAFVRvbmdhIChUb25nYSBJc2xhbmRzKXB0AAJ0b3EAfgBFc3EAfgBBcHQABlRzb25n
YXB0AAJ0c3EAfgBFc3EAfgBBcHQABlRzd2FuYXB0AAJ0bnEAfgBFc3EAfgBBcHQAB1R1cmtpc2hw
dAACdHJxAH4ARXNxAH4AQXB0AAdUdXJrbWVucHQAAnRrcQB+AEVzcQB+AEFwdAADVHdpcHQAAnR3
cQB+AEVzcQB+AEFwdAAOVWlnaHVyLCBVeWdodXJwdAACdWdxAH4ARXNxAH4AQXB0AAlVa3JhaW5p
YW5wdAACdWtxAH4ARXNxAH4AQXB0AARVcmR1cHQAAnVycQB+AEVzcQB+AEFwdAAFVXpiZWtwdAAC
dXpxAH4ARXNxAH4AQXB0AAVWZW5kYXB0AAJ2ZXEAfgBFc3EAfgBBcHQAClZpZXRuYW1lc2VwdAAC
dmlxAH4ARXNxAH4AQXB0AAhWb2xhcMO8a3B0AAJ2b3EAfgBFc3EAfgBBcHQAB1dhbGxvb25wdAAC
d2FxAH4ARXNxAH4AQXB0AAVXZWxzaHB0AAJjeXEAfgBFc3EAfgBBcHQAD1dlc3Rlcm4gRnJpc2lh
bnB0AAJmeXEAfgBFc3EAfgBBcHQABVdvbG9mcHQAAndvcQB+AEVzcQB+AEFwdAAFWGhvc2FwdAAC
eGhxAH4ARXNxAH4AQXB0AAdZaWRkaXNocHQAAnlpcQB+AEVzcQB+AEFwdAAGWW9ydWJhcHQAAnlv
cQB+AEVzcQB+AEFwdAAOWmh1YW5nLCBDaHVhbmdwdAACemFxAH4ARXNxAH4AQXB0AARadWx1cHQA
Anp1cQB+AEV4cQB+AExzcQB+ACt0AB9JbmRpY2F0ZXMgdGhlIGhlaWdodCBpbiBwaXhlbHMucHB0
AAxpbWFnZV9oZWlnaHRwcQB+ADpzcQB+ACt0ACpJbnRlcnByZXQgdGhlIHNhbXBsZSBhcyBzaWdu
ZWQgb3IgdW5zaWduZWRwcHQADXNhbXBsZV9zaWduZWRwcQB+AFFzcQB+ACt0ADhJbmRpY2F0ZXMg
aWYgdGhlIHRvcCBvciBib3R0b20gZmllbGQgaXMgdGVtcG9yYWxseSBmaXJzdHBwdAAPZmllbGRf
ZG9taW5hbmNlc3EAfgA/AAAAAncEAAAAAnNxAH4AQXB0AAlUb3AgZmllbGRwdAAJdG9wX2ZpZWxk
cQB+AEVzcQB+AEFwdAAMQm90dG9tIGZpZWxkcHQADGJvdHRvbV9maWVsZHEAfgBFeHEAfgBMc3EA
fgArdAAlSW5kaWNhdGVzIHdoZXRoZXIgb3ZlcnNjYW4gaXMgcHJlc2VudHBwdAAMaGFzX292ZXJz
Y2FucHEAfgBRc3EAfgArdAAxSW5kaWNhdGVzIHRoZSBtYXhpbXVtIGJpdCByYXRlIGluIGJpdHMg
cGVyIHNlY29uZHBwdAAQbWF4aW11bV9iaXRfcmF0ZXBxAH4AOnNxAH4AK3QAGkluZGljYXRlcyB0
aGUgY29sb3Igc3BhY2UucHB0AAtjb2xvcl9zcGFjZXNxAH4APwAAAAN3BAAAAANzcQB+AEFwdAAD
WVVWcHQAA3l1dnEAfgBFc3EAfgBBcHQAA1JHQnB0AANyZ2JxAH4ARXNxAH4AQXB0AAlHcmF5c2Nh
bGVwdAAJZ3JheXNjYWxlcQB+AEV4cQB+AExzcQB+ACt0ADNJbmRpY2F0ZXMgdGhlIGludGVybGFj
aW5nIHR5cGUgb2YgdGhpcyB2aWRlbyBzYW1wbGVwcHQAEXZpZGVvX3NhbXBsZV90eXBlc3EAfgA/
AAAAA3cEAAAAA3NxAH4AQXB0AAVGcmFtZXB0AAVmcmFtZXEAfgBFc3EAfgBBcHEAfgLAcHEAfgLB
cQB+AEVzcQB+AEFwcQB+AsNwcQB+AsRxAH4ARXhxAH4ATHNxAH4AK3BwcHQAFGRhdGFfaXNfbWFu
dWZhY3R1cmVkcHEAfgBRc3EAfgArdAAjVG90YWwgbGVuZ3RoIG9mIHRoZSBzdHJlYW0gaWYga25v
d25wcHQADmxlbmd0aE9mU3RyZWFtcH5xAH4AM3QABExPTkdzcQB+ACt0ADJJbmRpY2F0ZXMgdGhl
IHNpemUgb2YgdGhlIGRlY29kaW5nIGJ1ZmZlciBpbiBieXRlc3BwdAAUZGVjb2RpbmdfYnVmZmVy
X3NpemVwcQB+ADpzcQB+ACt0AElUaGUgdGltZSBwZXJ0YWluaW5nIHRvIHRoZSBlbmQgb2YgdGhl
IGRhdGEgKHRpbWUgKyBkdXJhdGlvbiBvZiB0aGlzIGRhdGEpcHB0AAd0aW1lRW5kcHEAfgA1c3EA
fgArcHBwdAAQdHJhbnNwYXJlbmN5VHlwZXNxAH4APwAAAAN3BAAAAANzcQB+AEFwdAANQWxwaGEg
Y2hhbm5lbHB0AA1hbHBoYV9jaGFubmVscQB+AEVzcQB+AEFwdAASVHJhbnNwYXJlbmN5IGNvbG9y
cHQAEnRyYW5zcGFyZW5jeV9jb2xvcnEAfgBFc3EAfgBBcHQABk9wYXF1ZXB0AAZvcGFxdWVxAH4A
RXhxAH4ATHNxAH4AK3QARkxldmVsIG9mIHByZWNpc2lvbiAtIGNhbiBiZSBsb3dlciB0aGFuIHRo
ZSBhY3R1YWwgbnVtYmVyIG9mIHZhbGlkIGJpdHNwcHQAGGFjY3VyYWN5X2JpdHNfcGVyX3NhbXBs
ZXBxAH4AOnNxAH4AK3QAK0luZGljYXRlcyB0aGUgc2FtcGxpbmcgb2YgdGhlIHBpeGVsIHNhbXBs
ZS5wcHQADnBpeGVsX3NhbXBsaW5nc3EAfgA/AAAACHcEAAAACHNxAH4AQXB0AAE0cHEAfgMCcQB+
AEVzcQB+AEFwdAACNDRwcQB+AwRxAH4ARXNxAH4AQXB0AAM0NDRwcQB+AwZxAH4ARXNxAH4AQXB0
AAM0MjJwcQB+AwhxAH4ARXNxAH4AQXB0AAM0MjBwcQB+AwpxAH4ARXNxAH4AQXB0AAM0MTFwcQB+
AwxxAH4ARXNxAH4AQXB0AAQ0NDQ0cHEAfgMOcQB+AEVzcQB+AEFwdAAENDIyNHBxAH4DEHEAfgBF
eHEAfgBMc3EAfgArdAAqSW5kaWNhdGVzIHRoZSBzdGFuZGFyZCBvZiB0aGUgY29sb3Igc3BhY2Uu
cHB0ABRjb2xvcl9zcGFjZV9zdGFuZGFyZHNxAH4APwAAAAh3BAAAAAhzcQB+AEFwdAAHUmVjIDYw
MXB0AAZyZWM2MDFxAH4ARXNxAH4AQXB0AAdSZWMgNzA5cHQABnJlYzcwOXEAfgBFc3EAfgBBcHQA
ElJlYyA2MDEgRnVsbCBSYW5nZXB0ABFyZWM2MDFfZnVsbF9yYW5nZXEAfgBFc3EAfgBBcHQAElJl
YyA3MDkgRnVsbCBSYW5nZXB0ABFyZWM3MDlfZnVsbF9yYW5nZXEAfgBFc3EAfgBBcHQAClN0dWRp
byBSR0JwdAAJc3R1ZGlvUkdCcQB+AEVzcQB+AEFwdAAMQ29tcHV0ZXIgUkdCcHQAC2NvbXB1dGVy
UkdCcQB+AEVzcQB+AEFwdAAUR3JheXNjYWxlIEhlYWQgUmFuZ2VwdAAUZ3JheXNjYWxlX2hlYWRf
cmFuZ2VxAH4ARXNxAH4AQXB0ABRHcmF5c2NhbGUgRnVsbCBSYW5nZXB0ABRncmF5c2NhbGVfZnVs
bF9yYW5nZXEAfgBFeHEAfgBMc3EAfgArdABESW5kaWNhdGVzIHRoZSBudW1iZXIgb2YgYnl0ZXMg
Zm9yIGVhY2ggcGxhbmUgKGRlcHJlY2F0ZWQgYXMgb2YgdjEuMSlwcQB+AFd0AA9ieXRlc19wZXJf
cGxhbmVwcQB+ADpzcQB+ACt0AB5JbmRpY2F0ZXMgdGhlIHdpZHRoIGluIHBpeGVscy5wcHQAC2lt
YWdlX3dpZHRocHEAfgA6c3EAfgArdAAxVG90YWwgbnVtYmVyIG9mIHZhbGlkIGFuZCBpbnZhbGlk
IGJpdHMgcGVyIHNhbXBsZXBwdAAXc3RvcmFnZV9iaXRzX3Blcl9zYW1wbGVwcQB+ADpzcQB+ACt0
AI9TZXQgdG8gdHJ1ZSBpZiB0aGUgc2lnbmFsIGlzIGludGVybGFjZWQgYnV0IGJvdGggZmllbGRz
IGFyZSBjb2luY2lkZW50IGluIHRpbWUgKG1hcHBpbmcgb2YgYSBwcm9ncmVzc2l2ZSBzY2FuIG9u
IGFuIGludGVybGFjZWQgc2lnbmFsIGVuY29kaW5nKXBwdAAPc2VnbWVudGVkX2ZyYW1lcHEAfgBR
c3EAfgArdAAYSW5kaWNhdGVzIHRoZSBmcmFtZSByYXRlcHB0AApmcmFtZV9yYXRlcH5xAH4AM3QA
CFJBVElPTkFMc3EAfgArdAAxSW5kaWNhdGVzIHRoZSBwYWNraW5nIG1ldGhvZCBvZiB0aGUgcGl4
ZWwgc2FtcGxlLnBwdAAWc2FtcGxlX2xheW91dF9zdHJhdGVneXNxAH4APwAAAAN3BAAAAANzcQB+
AEFwdAAGUGFja2VkcHQABnBhY2tlZHEAfgBFc3EAfgBBcHQABlBsYW5hcnB0AAZwbGFuYXJxAH4A
RXNxAH4AQXB0AAdQYWxldHRlcHQAB3BhbGV0dGVxAH4ARXhxAH4ATHNxAH4AK3QAL0luZGljYXRl
cyBpZiB0aGUgc3RyZWFtIGhhcyBhIGNvbnN0YW50IGJpdCByYXRlcHB0ABFjb25zdGFudF9iaXRf
cmF0ZXBxAH4AUXNxAH4AK3QAM0RpcmVjdGlvbiBvZiB0aGUgaW1hZ2UgcmFzdGVyICh0b3BfZG93
biwgYm90dG9tX3VwKXBwdAAScmFzdGVyX29yaWVudGF0aW9uc3EAfgA/AAAAAncEAAAAAnNxAH4A
QXB0AAhUb3AgZG93bnB0AAh0b3BfZG93bnEAfgBFc3EAfgBBcHQACUJvdHRvbSB1cHB0AAlib3R0
b21fdXBxAH4ARXhxAH4ATHNxAH4AK3QAM1Bvc2l0aW9uIG9yIG9mZnNldCBmcm9tIHRoZSBiZWdp
bm5pbmcgb2YgdGhlIHN0cmVhbXBwdAAQcG9zaXRpb25JblN0cmVhbXBxAH4C5nNxAH4AK3QAXUlu
ZGljYXRlcyB0aGF0IHRoZSBhbHBoYSBjaGFubmVsIGhhcyBiZWVuIHByZW11bHRpcGxpZWQgaW50
byB0aGUgb3RoZXIgY2hhbm5lbHMgKHNpbmNlIHYxLjEwKXBwdAAeYWxwaGFfY2hhbm5lbF9pc19w
cmVtdWx0aXBsaWVkcHEAfgBRc3EAfgArdABHSW5kaWNhdGVzIHRoZSBudW1iZXIgb2YgYnl0ZXMg
Zm9yIGVhY2ggc2NhbmxpbmUgKGRlcHJlY2F0ZWQgYXMgb2YgdjEuMSlwcQB+AFd0ABJieXRlc19w
ZXJfc2NhbmxpbmVwcQB+ADpzcQB+ACt0ACJJbmRpY2F0ZXMgdGhlIGRpc3BsYXkgYXNwZWN0IHJh
dGlvcHB0ABRkaXNwbGF5X2FzcGVjdF9yYXRpb3BxAH4DPHNxAH4AK3QAH051bWJlciBvZiB2YWxp
ZCBiaXRzIHBlciBzYW1wbGVwcHQAD2JpdHNfcGVyX3NhbXBsZXBxAH4AOnNxAH4AK3QALVRoZSBt
b3N0IHJlbGV2YW50IHRpbWUgcGVydGFpbmluZyB0byB0aGUgZGF0YXBwdAAEdGltZXBxAH4ANXNx
AH4AK3QAO0luZGljYXRlcyB0aGUgbGF5b3V0IGFuZCBmb3JtYXQgb2YgcGl4ZWwgc2FtcGxlcyBp
biBtZW1vcnkucHB0ABVzYW1wbGVfbGF5b3V0X2RldGFpbHNzcQB+AD8AAAATdwQAAAATc3EAfgBB
cHQAA0JHUnB0AANiZ3JxAH4ARXNxAH4AQXB0AARCR1JBcHQABGJncmFxAH4ARXNxAH4AQXBxAH4C
03BxAH4C1HEAfgBFc3EAfgBBcHQABFJHQkFwdAAEcmdiYXEAfgBFc3EAfgBBcHQABEFSR0JwdAAE
YXJnYnEAfgBFc3EAfgBBcHQABFVZVllwdAAEdXl2eXEAfgBFc3EAfgBBcHQABFlVWVZwdAAEeXV5
dnEAfgBFc3EAfgBBcHQABlVZVlkxMHB0AAZ1eXZ5MTBxAH4ARXNxAH4AQXB0AAZZVVlWMTBwdAAG
eXV5djEwcQB+AEVzcQB+AEFwdAAGWVVWMjEwcHQABnl1djIxMHEAfgBFc3EAfgBBcHQABllVVjQx
MHB0AAZ5dXY0MTBxAH4ARXNxAH4AQXBxAH4C0HBxAH4C0XEAfgBFc3EAfgBBcHQABFlVVkFwdAAE
eXV2YXEAfgBFc3EAfgBBcHQAA1lWVXB0AAN5dnVxAH4ARXNxAH4AQXBxAH4C1nBxAH4C13EAfgBF
c3EAfgBBcHQAD0dyYXlzY2FsZSBhbHBoYXB0AA9ncmF5c2NhbGVfYWxwaGFxAH4ARXNxAH4AQXB0
ABBOZXhpbyAxMC1iaXQgWVVWcHQADG5leGlvX3l1eXYxMHEAfgBFc3EAfgBBcHQAEE5leGlvIDEy
LWJpdCBZVVZwdAAMbmV4aW9feXV5djEycQB+AEVzcQB+AEFwdAAEUjEwa3B0AARyMTBrcQB+AEV4
cQB+AEx4cHNyABFqYXZhLnV0aWwuSGFzaE1hcAUH2sHDFmDRAwACRgAKbG9hZEZhY3RvckkACXRo
cmVzaG9sZHhwPwAAAAAAACB3CAAAAEAAAAAWcQB+ADJzcgAkY2EuZGlnaXRhbHJhcGlkcy5rYXlh
ay50aW1lLlRpbWVJbXBsAAAAAAAAAAECAAJMAAhyYXRpb25hbHQAMUxjYS9kaWdpdGFscmFwaWRz
L2theWFrL2RhdGEvaW1wbC9SYXRpb25hbE51bWJlcjtMAAh0aW1lQmFzZXQAJkxjYS9kaWdpdGFs
cmFwaWRzL2theWFrL3RpbWUvVGltZUJhc2U7eHBzcgAvY2EuZGlnaXRhbHJhcGlkcy5rYXlhay5k
YXRhLmltcGwuUmF0aW9uYWxOdW1iZXIAAAAAAAAAAQIABEoAC2Rlbm9taW5hdG9yWgAJaXNSZWR1
Y2VkWgATbmVlZHNCaWdGb3JNdWx0aXBseUoACW51bWVyYXRvcnhyABBqYXZhLmxhbmcuTnVtYmVy
hqyVHQuU4IsCAAB4cAAAAAAAAF3AAAAAAAAAAEP4MnNyAChjYS5kaWdpdGFscmFwaWRzLmtheWFr
LnRpbWUuVGltZUJhc2VJbXBsAAAAAAAAAAECAAFMAA5vZmZzZXRSYXRpb25hbHEAfgOkeHBzcQB+
A6cAAAAAO5rKAAAAAAAAAAAAAABxAH4AOXNyAA5qYXZhLmxhbmcuTG9uZzuL5JDMjyPfAgABSgAF
dmFsdWV4cQB+A6gAAAAAGt4fM3EAfgA+dAALcHJvZ3Jlc3NpdmVxAH4AWHNyACZqYXZhLnV0aWwu
Q29sbGVjdGlvbnMkVW5tb2RpZmlhYmxlTGlzdPwPJTG17I4QAgABTAAEbGlzdHEAfgAseHIALGph
dmEudXRpbC5Db2xsZWN0aW9ucyRVbm1vZGlmaWFibGVDb2xsZWN0aW9uGUIAgMte9x4CAAFMAAFj
dAAWTGphdmEvdXRpbC9Db2xsZWN0aW9uO3hwc3EAfgA/AAAAAHcEAAAAAHhxAH4DtHEAfgBfcQB+
AFdxAH4AhXQAAmVucQB+ArdzcgARamF2YS5sYW5nLkludGVnZXIS4qCk94GHOAIAAUkABXZhbHVl
eHEAfgOoAAADMHEAfgLKc3EAfgOtAAAAABreHzNxAH4CzXEAfgLRcQB+AtpxAH4C3nEAfgL/cQB+
AwpxAH4DL3NxAH4DsHNxAH4APwAAAAN3BAAAAANzcQB+A7YAF+gAc3EAfgO2AAX6AHNxAH4DtgAF
+gB4cQB+A7pxAH4DMnNxAH4DtgAAB4BxAH4DNXNxAH4DtgAAAAhxAH4DO3NxAH4DpwAAAAAAAAPp
AQAAAAAAAABdwHEAfgNAcQB+A0dxAH4DTXEAfgBXcQB+A1BxAH4DVHEAfgNgc3EAfgOwc3EAfgA/
AAAAA3cEAAAAA3NxAH4DtgAAB4BzcQB+A7YAAAPAc3EAfgO2AAADwHhxAH4DwnEAfgNjc3EAfgOn
AAAAAAAAABEBAAAAAAAAAAAocQB+A2ZxAH4Dv3EAfgNscQB+AtF4</property>
                        <pinDefinition name="UncompressedVideo" displayName="Uncompressed Video" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAED9AAAAAAAAKdAAJRnJhbWVSYXRldAAOVGltZWNvZGVTdHJl
YW10AAhUZW1wb3JhbHQACFRpbWVjb2RldAALTWVkaWFUaW1pbmd0AA1EYXRhSXNNaXNzaW5ndAAG
U3RyZWFtdAARU2FtcGxlSW5mb3JtYXRpb250ABlUaW1lY29kZVNhbXBsZUluZm9ybWF0aW9udAAO
VGltZWNvZGVTYW1wbGV4c3EAfgAIdwwAAAAgP0AAAAAAABFzcgBPY2EuZGlnaXRhbHJhcGlkcy5r
YXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5EYXRhVHlwZURlZmluaXRpb24kU2ltcGxl
VHlwZQAAAAAAAAABAgACTAARZW51bWVyYXRpb25WYWx1ZXN0ABBMamF2YS91dGlsL0xpc3Q7TAAE
dHlwZXQAQ0xjYS9kaWdpdGFscmFwaWRzL2theWFrL2RhdGF0eXBlcy9kZWZpbml0aW9uL21vZGVs
L1NpbXBsZVR5cGVzRW51bTt4cgBSY2EuZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhdHlwZXMuZGVm
aW5pdGlvbi5tb2RlbC5EYXRhVHlwZURlZmluaXRpb24kS2V5RGVmaW5pdGlvbgAAAAAAAAABAgAE
TAAHY29tbWVudHEAfgAFTAALZGlzcGxheU5hbWVxAH4ABUwAC211bHRpVmFsdWVkdAATTGphdmEv
bGFuZy9Cb29sZWFuO0wABG5hbWVxAH4ABXhwdAAcSW5kaWNhdGVzIHRoZSBtZWRpYSBkdXJhdGlv
bnBwdAAObWVkaWFfZHVyYXRpb25wfnIAQWNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVz
LmRlZmluaXRpb24ubW9kZWwuU2ltcGxlVHlwZXNFbnVtAAAAAAAAAAASAAB4cgAOamF2YS5sYW5n
LkVudW0AAAAAAAAAABIAAHhwdAAEVElNRXNxAH4AFXQASVRoZSB0aW1lIHBlcnRhaW5pbmcgdG8g
dGhlIGVuZCBvZiB0aGUgZGF0YSAodGltZSArIGR1cmF0aW9uIG9mIHRoaXMgZGF0YSlwcHQAB3Rp
bWVFbmRwcQB+AB9zcQB+ABV0AEd0cnVlIGlmIHRoZSB0aW1lY29kZSByZXNldCB0byAwMDowMDow
MDowMCB3aGVuIHJlYWNoaW5nIGEgY2VydGFpbiB2YWx1ZXBwdAAOdGltZWNvZGVfcmVzZXRzcgAT
amF2YS51dGlsLkFycmF5TGlzdHiB0h2Zx2GdAwABSQAEc2l6ZXhwAAAAA3cEAAAAA3NyADdjYS5k
aWdpdGFscmFwaWRzLmtheWFrLnBsdWdpbi54bWwuS2F5YWtFbnVtZXJhdGlvblZhbHVlAAAAAAAA
AAECAAVMAAtkZXNjcmlwdGlvbnEAfgAFTAALZGlzcGxheU5hbWVxAH4ABUwABmhpZGRlbnEAfgAZ
TAAOdmFsdWVBdHRyaWJ1dGVxAH4ABUwADXZhbHVlRW1iZWRkZWRxAH4ABXhwcHQABE5vbmVwdAAE
bm9uZXQAAHNxAH4AKXB0AAcxMiBob3VycHQAAzEyaHEAfgAtc3EAfgApcHQABzI0IGhvdXJwdAAD
MjRocQB+AC14fnEAfgAddAAGU1RSSU5Hc3EAfgAVdAAidHJ1ZSBpZiB0aGUgdGltZWNvZGUgaXMg
ZHJvcCBmcmFtZXBwdAATdGltZWNvZGVfZHJvcF9mcmFtZXB+cQB+AB10AAdCT09MRUFOc3EAfgAV
dABBVHJ1ZSBvbiB0aGUgbGFzdCBkYXRhIHBhY2tldCBvZiB0aGUgU3RyZWFtICh3aXRoIG9yIHdp
dGhvdXQgZGF0YSlwcHQAC2VuZE9mU3RyZWFtcHEAfgA5c3EAfgAVdAAYSW5kaWNhdGVzIHRoZSBm
cmFtZSByYXRlcHB0ABN0aW1lY29kZV9mcmFtZV9yYXRlcH5xAH4AHXQAB0lOVEVHRVJzcQB+ABV0
AB5JbmRpY2F0ZXMgdGhlIDMyLWJpdCB1c2VyIGRhdGFwcHQAEnRpbWVjb2RlX3VzZXJfYml0c3Bx
AH4AQXNxAH4AFXQALEluZGljYXRlcyBpZiB0aGUgZnJhbWUgcmF0ZSByZW1haW5zIGNvbnN0YW50
cHB0ABNjb25zdGFudF9mcmFtZV9yYXRlcHEAfgA5c3EAfgAVdAAYSW5kaWNhdGVzIHRoZSBmcmFt
ZSByYXRlcHB0AApmcmFtZV9yYXRlcH5xAH4AHXQACFJBVElPTkFMc3EAfgAVcHBwdAAPZGF0YV9p
c19taXNzaW5ncHEAfgA5c3EAfgAVdAAjSW5kaWNhdGVzIHRoZSB0aW1lY29kZSBmaWVsZCBudW1i
ZXJwcHQADnRpbWVjb2RlX2ZpZWxkc3EAfgAnAAAAAncEAAAAAnNxAH4AKXB0AAExcHEAfgBVcQB+
AC1zcQB+AClwdAABMnBxAH4AV3EAfgAteHEAfgBBc3EAfgAVdAARVGhlIHRpbWVjb2RlIHR5cGVw
cHQADXRpbWVjb2RlX3R5cGVzcQB+ACcAAAADdwQAAAADc3EAfgApcHQACFRpbWVjb2RlcHQACHRp
bWVjb2RlcQB+AC1zcQB+AClwdAARTG9jYWwgdGltZSBvZiBkYXlwdAARbG9jYWxfdGltZV9vZl9k
YXlxAH4ALXNxAH4AKXB0AA9VVEMgdGltZSBvZiBkYXlwdAAPVVRDX3RpbWVfb2ZfZGF5cQB+AC14
cQB+ADRzcQB+ABV0AC1UaGUgbW9zdCByZWxldmFudCB0aW1lIHBlcnRhaW5pbmcgdG8gdGhlIGRh
dGFwcHQABHRpbWVwcQB+AB9zcQB+ABV0ACdJbmRpY2F0ZXMgaWYgdGhlIHRpbWVjb2RlIGlzIGNv
bnRpbnVvdXNwcHQAFnRpbWVjb2RlX2lzX2NvbnRpbnVvdXNwcQB+ADlzcQB+ABV0ACBJbmRpY2F0
ZXMgdGhlIGJpbmFyeSBncm91cCBmbGFnc3BwdAAbdGltZWNvZGVfYmluYXJ5X2dyb3VwX2ZsYWdz
cHEAfgBBc3EAfgAVdABBdHJ1ZSBpZiB0aGUgdGltZWNvZGUgc3RyZWFtIGNvbnRhaW5zIG9uZSB0
aW1lY29kZSB2YWx1ZSBwZXIgZmllbGRwcHQAF3RpbWVjb2RlX2lzX2ZpZWxkX2Jhc2VkcHEAfgA5
c3EAfgAVdABBdHJ1ZSBpZiB0aGUgdGltZSBjb2RlIGlzIHN5bmNocm9uaXNlZCB3aXRoIGEgY29s
b3IgZmllbGQgc2VxdWVuY2VwcHQAGXRpbWVjb2RlX2NvbG9yX2ZyYW1lX2ZsYWdwcQB+ADl4cHNy
ABFqYXZhLnV0aWwuSGFzaE1hcAUH2sHDFmDRAwACRgAKbG9hZEZhY3RvckkACXRocmVzaG9sZHhw
PwAAAAAAAEB3CAAAAEAAAAAAeA==</property>
                        <pinDefinition name="Timecode" displayName="Timecode (AVC SEI)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Data608Service" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAAQdAAJRnJhbWVSYXRldAANRGF0YUlzTWlzc2lu
Z3QAC01lZGlhVGltaW5ndAAORGF0YTYwOFNlcnZpY2V0ABREYXRhNjA4U2VydmljZVN0cmVhbXQA
EVNhbXBsZUluZm9ybWF0aW9udAALRGF0YVNlcnZpY2V0ABREYXRhNjA4U2VydmljZVNhbXBsZXQA
EURhdGFTZXJ2aWNlU2FtcGxldAAIVGVtcG9yYWx0AAtNZWRpYVN0cmVhbXQAC0theWFrQnVmZmVy
dAAKQnl0ZVN0cmVhbXQAC01lZGlhT3JpZ2ludAARRGF0YVNlcnZpY2VTdHJlYW10AAZTdHJlYW14
c3EAfgAIdwwAAAAQP0AAAAAAAAtzcgBPY2EuZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhdHlwZXMu
ZGVmaW5pdGlvbi5tb2RlbC5EYXRhVHlwZURlZmluaXRpb24kU2ltcGxlVHlwZQAAAAAAAAABAgAC
TAARZW51bWVyYXRpb25WYWx1ZXN0ABBMamF2YS91dGlsL0xpc3Q7TAAEdHlwZXQAQ0xjYS9kaWdp
dGFscmFwaWRzL2theWFrL2RhdGF0eXBlcy9kZWZpbml0aW9uL21vZGVsL1NpbXBsZVR5cGVzRW51
bTt4cgBSY2EuZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5E
YXRhVHlwZURlZmluaXRpb24kS2V5RGVmaW5pdGlvbgAAAAAAAAABAgAETAAHY29tbWVudHEAfgAF
TAALZGlzcGxheU5hbWVxAH4ABUwAC211bHRpVmFsdWVkdAATTGphdmEvbGFuZy9Cb29sZWFuO0wA
BG5hbWVxAH4ABXhwdABJVGhlIHRpbWUgcGVydGFpbmluZyB0byB0aGUgZW5kIG9mIHRoZSBkYXRh
ICh0aW1lICsgZHVyYXRpb24gb2YgdGhpcyBkYXRhKXBwdAAHdGltZUVuZHB+cgBBY2EuZGlnaXRh
bHJhcGlkcy5rYXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5TaW1wbGVUeXBlc0VudW0A
AAAAAAAAABIAAHhyAA5qYXZhLmxhbmcuRW51bQAAAAAAAAAAEgAAeHB0AARUSU1Fc3EAfgAbdAAc
SW5kaWNhdGVzIHRoZSBtZWRpYSBkdXJhdGlvbnBwdAAObWVkaWFfZHVyYXRpb25wcQB+ACVzcQB+
ABtwcHB0AA9kYXRhX2lzX21pc3NpbmdwfnEAfgAjdAAHQk9PTEVBTnNxAH4AG3QAM1Bvc2l0aW9u
IG9yIG9mZnNldCBmcm9tIHRoZSBiZWdpbm5pbmcgb2YgdGhlIHN0cmVhbXBwdAAQcG9zaXRpb25J
blN0cmVhbXB+cQB+ACN0AARMT05Hc3EAfgAbcHBzcgARamF2YS5sYW5nLkJvb2xlYW7NIHKA1Zz6
7gIAAVoABXZhbHVleHABdAAMbWVkaWFfb3JpZ2luc3IAE2phdmEudXRpbC5BcnJheUxpc3R4gdId
mcdhnQMAAUkABHNpemV4cAAAABV3BAAAABVzcgA3Y2EuZGlnaXRhbHJhcGlkcy5rYXlhay5wbHVn
aW4ueG1sLktheWFrRW51bWVyYXRpb25WYWx1ZQAAAAAAAAABAgAFTAALZGVzY3JpcHRpb25xAH4A
BUwAC2Rpc3BsYXlOYW1lcQB+AAVMAAZoaWRkZW5xAH4AH0wADnZhbHVlQXR0cmlidXRlcQB+AAVM
AA12YWx1ZUVtYmVkZGVkcQB+AAV4cHBwcHB0AAxNYW51ZmFjdHVyZWRzcQB+ADlwdAAIRFJDVmlk
ZW9wcQB+AD10AABzcQB+ADlwdAADR1hGcHEAfgBAcQB+AD5zcQB+ADlwdAADTFhGcHEAfgBCcQB+
AD5zcQB+ADlwdAADTVhGcHEAfgBEcQB+AD5zcQB+ADlwdAAJUXVpY2tUaW1lcHEAfgBGcQB+AD5z
cQB+ADlwdAANV2luZG93cyBNZWRpYXB0AAxXaW5kb3dzTWVkaWFxAH4APnNxAH4AOXB0AAlVc2Vy
IERhdGFwdAAJVXNlcl9kYXRhcQB+AD5zcQB+ADlwdAAOQW5jaWxsYXJ5IERhdGFwdAAOQW5jaWxs
YXJ5X2RhdGFxAH4APnNxAH4AOXB0AAJEVnBxAH4AUXEAfgA+c3EAfgA5cHQAA1ZDM3BxAH4AU3EA
fgA+c3EAfgA5cHQAFkFWQyBQaWN0dXJlIFRpbWluZyBTRUlwdAAWQVZDX1BpY3R1cmVfVGltaW5n
X1NFSXEAfgA+c3EAfgA5cHQAEE1QRUcyIEdPUCBIZWFkZXJwdAAQTVBFRzJfR09QX2hlYWRlcnEA
fgA+c3EAfgA5cHQAFE1YRiBNYXRlcmlhbCBQYWNrYWdlcHQAFE1YRl9tYXRlcmlhbF9wYWNrYWdl
cQB+AD5zcQB+ADlwdAAQTVhGIFN5c3RlbSBUcmFja3B0ABBNWEZfc3lzdGVtX3RyYWNrcQB+AD5z
cQB+ADlwdAATQW5jaWxsYXJ5IERhdGEgVklUQ3B0ABNBbmNpbGxhcnlfZGF0YV9WSVRDcQB+AD5z
cQB+ADlwdAASQW5jaWxsYXJ5IERhdGEgTFRDcHQAEkFuY2lsbGFyeV9kYXRhX0xUQ3EAfgA+c3EA
fgA5cHQAEFNDVEUyMCBVc2VyIERhdGFwdAAQVXNlcl9kYXRhX1NDVEUyMHEAfgA+c3EAfgA5cHQA
DkFUU0MgVXNlciBEYXRhcHQADlVzZXJfZGF0YV9BVFNDcQB+AD5zcQB+ADlwcQB+AGpwdAADU0ND
cQB+AD5zcQB+ADlwdAAZQW5jaWxsYXJ5IERhdGEgTGVnYWN5IDYwOHB0ABlBbmNpbGxhcnlfZGF0
YV9sZWdhY3lfNjA4cQB+AD54fnEAfgAjdAAGU1RSSU5Hc3EAfgAbdABBVHJ1ZSBvbiB0aGUgbGFz
dCBkYXRhIHBhY2tldCBvZiB0aGUgU3RyZWFtICh3aXRoIG9yIHdpdGhvdXQgZGF0YSlwcHQAC2Vu
ZE9mU3RyZWFtcHEAfgAsc3EAfgAbdAAsSW5kaWNhdGVzIGlmIHRoZSBmcmFtZSByYXRlIHJlbWFp
bnMgY29uc3RhbnRwcHQAE2NvbnN0YW50X2ZyYW1lX3JhdGVwcQB+ACxzcgBQY2EuZGlnaXRhbHJh
cGlkcy5rYXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5EYXRhVHlwZURlZmluaXRpb24k
Q29tcGxleFR5cGUAAAAAAAAAAQIAAkwACG9wdGlvbmFscQB+AB9MAAR0eXBlcQB+AAV4cQB+AB50
ACFEZXRhaWwgaW5mb3JtYXRpb24gZm9yIGVhY2ggdHJhY2twcQB+ADV0AAhzZXJ2aWNlc3B0AA5E
YXRhNjA4Q29udGVudHNxAH4AG3QALVRoZSBtb3N0IHJlbGV2YW50IHRpbWUgcGVydGFpbmluZyB0
byB0aGUgZGF0YXBwdAAEdGltZXBxAH4AJXNxAH4AG3QAGEluZGljYXRlcyB0aGUgZnJhbWUgcmF0
ZXBwdAAKZnJhbWVfcmF0ZXB+cQB+ACN0AAhSQVRJT05BTHNxAH4AG3QAI1RvdGFsIGxlbmd0aCBv
ZiB0aGUgc3RyZWFtIGlmIGtub3ducHB0AA5sZW5ndGhPZlN0cmVhbXBxAH4AMXhwc3IAEWphdmEu
dXRpbC5IYXNoTWFwBQfawcMWYNEDAAJGAApsb2FkRmFjdG9ySQAJdGhyZXNob2xkeHA/AAAAAAAA
IHcIAAAAQAAAAAJxAH4ANnNyACZqYXZhLnV0aWwuQ29sbGVjdGlvbnMkVW5tb2RpZmlhYmxlTGlz
dPwPJTG17I4QAgABTAAEbGlzdHEAfgAceHIALGphdmEudXRpbC5Db2xsZWN0aW9ucyRVbm1vZGlm
aWFibGVDb2xsZWN0aW9uGUIAgMte9x4CAAFMAAFjdAAWTGphdmEvdXRpbC9Db2xsZWN0aW9uO3hw
c3EAfgA3AAAAAXcEAAAAAXEAfgBoeHEAfgCPcQB+AHxzcQB+AItzcQB+ADcAAAAAdwQAAAAAeHEA
fgCReA==</property>
                        <pinDefinition name="Data608Service" displayName="EIA-608 Captions (SCTE-20 User Data)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Data608Service 2" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAAQdAAJRnJhbWVSYXRldAANRGF0YUlzTWlzc2lu
Z3QAC01lZGlhVGltaW5ndAAORGF0YTYwOFNlcnZpY2V0ABREYXRhNjA4U2VydmljZVN0cmVhbXQA
EVNhbXBsZUluZm9ybWF0aW9udAALRGF0YVNlcnZpY2V0ABREYXRhNjA4U2VydmljZVNhbXBsZXQA
EURhdGFTZXJ2aWNlU2FtcGxldAAIVGVtcG9yYWx0AAtNZWRpYVN0cmVhbXQAC0theWFrQnVmZmVy
dAAKQnl0ZVN0cmVhbXQAC01lZGlhT3JpZ2ludAARRGF0YVNlcnZpY2VTdHJlYW10AAZTdHJlYW14
c3EAfgAIdwwAAAAQP0AAAAAAAAtzcgBPY2EuZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhdHlwZXMu
ZGVmaW5pdGlvbi5tb2RlbC5EYXRhVHlwZURlZmluaXRpb24kU2ltcGxlVHlwZQAAAAAAAAABAgAC
TAARZW51bWVyYXRpb25WYWx1ZXN0ABBMamF2YS91dGlsL0xpc3Q7TAAEdHlwZXQAQ0xjYS9kaWdp
dGFscmFwaWRzL2theWFrL2RhdGF0eXBlcy9kZWZpbml0aW9uL21vZGVsL1NpbXBsZVR5cGVzRW51
bTt4cgBSY2EuZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5E
YXRhVHlwZURlZmluaXRpb24kS2V5RGVmaW5pdGlvbgAAAAAAAAABAgAETAAHY29tbWVudHEAfgAF
TAALZGlzcGxheU5hbWVxAH4ABUwAC211bHRpVmFsdWVkdAATTGphdmEvbGFuZy9Cb29sZWFuO0wA
BG5hbWVxAH4ABXhwdABJVGhlIHRpbWUgcGVydGFpbmluZyB0byB0aGUgZW5kIG9mIHRoZSBkYXRh
ICh0aW1lICsgZHVyYXRpb24gb2YgdGhpcyBkYXRhKXBwdAAHdGltZUVuZHB+cgBBY2EuZGlnaXRh
bHJhcGlkcy5rYXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5TaW1wbGVUeXBlc0VudW0A
AAAAAAAAABIAAHhyAA5qYXZhLmxhbmcuRW51bQAAAAAAAAAAEgAAeHB0AARUSU1Fc3EAfgAbdAAc
SW5kaWNhdGVzIHRoZSBtZWRpYSBkdXJhdGlvbnBwdAAObWVkaWFfZHVyYXRpb25wcQB+ACVzcQB+
ABtwcHB0AA9kYXRhX2lzX21pc3NpbmdwfnEAfgAjdAAHQk9PTEVBTnNxAH4AG3Bwc3IAEWphdmEu
bGFuZy5Cb29sZWFuzSBygNWc+u4CAAFaAAV2YWx1ZXhwAXQADG1lZGlhX29yaWdpbnNyABNqYXZh
LnV0aWwuQXJyYXlMaXN0eIHSHZnHYZ0DAAFJAARzaXpleHAAAAAVdwQAAAAVc3IAN2NhLmRpZ2l0
YWxyYXBpZHMua2F5YWsucGx1Z2luLnhtbC5LYXlha0VudW1lcmF0aW9uVmFsdWUAAAAAAAAAAQIA
BUwAC2Rlc2NyaXB0aW9ucQB+AAVMAAtkaXNwbGF5TmFtZXEAfgAFTAAGaGlkZGVucQB+AB9MAA52
YWx1ZUF0dHJpYnV0ZXEAfgAFTAANdmFsdWVFbWJlZGRlZHEAfgAFeHBwcHBwdAAMTWFudWZhY3R1
cmVkc3EAfgA0cHQACERSQ1ZpZGVvcHEAfgA4dAAAc3EAfgA0cHQAA0dYRnBxAH4AO3EAfgA5c3EA
fgA0cHQAA0xYRnBxAH4APXEAfgA5c3EAfgA0cHQAA01YRnBxAH4AP3EAfgA5c3EAfgA0cHQACVF1
aWNrVGltZXBxAH4AQXEAfgA5c3EAfgA0cHQADVdpbmRvd3MgTWVkaWFwdAAMV2luZG93c01lZGlh
cQB+ADlzcQB+ADRwdAAJVXNlciBEYXRhcHQACVVzZXJfZGF0YXEAfgA5c3EAfgA0cHQADkFuY2ls
bGFyeSBEYXRhcHQADkFuY2lsbGFyeV9kYXRhcQB+ADlzcQB+ADRwdAACRFZwcQB+AExxAH4AOXNx
AH4ANHB0AANWQzNwcQB+AE5xAH4AOXNxAH4ANHB0ABZBVkMgUGljdHVyZSBUaW1pbmcgU0VJcHQA
FkFWQ19QaWN0dXJlX1RpbWluZ19TRUlxAH4AOXNxAH4ANHB0ABBNUEVHMiBHT1AgSGVhZGVycHQA
EE1QRUcyX0dPUF9oZWFkZXJxAH4AOXNxAH4ANHB0ABRNWEYgTWF0ZXJpYWwgUGFja2FnZXB0ABRN
WEZfbWF0ZXJpYWxfcGFja2FnZXEAfgA5c3EAfgA0cHQAEE1YRiBTeXN0ZW0gVHJhY2twdAAQTVhG
X3N5c3RlbV90cmFja3EAfgA5c3EAfgA0cHQAE0FuY2lsbGFyeSBEYXRhIFZJVENwdAATQW5jaWxs
YXJ5X2RhdGFfVklUQ3EAfgA5c3EAfgA0cHQAEkFuY2lsbGFyeSBEYXRhIExUQ3B0ABJBbmNpbGxh
cnlfZGF0YV9MVENxAH4AOXNxAH4ANHB0ABBTQ1RFMjAgVXNlciBEYXRhcHQAEFVzZXJfZGF0YV9T
Q1RFMjBxAH4AOXNxAH4ANHB0AA5BVFNDIFVzZXIgRGF0YXB0AA5Vc2VyX2RhdGFfQVRTQ3EAfgA5
c3EAfgA0cHEAfgBlcHQAA1NDQ3EAfgA5c3EAfgA0cHQAGUFuY2lsbGFyeSBEYXRhIExlZ2FjeSA2
MDhwdAAZQW5jaWxsYXJ5X2RhdGFfbGVnYWN5XzYwOHEAfgA5eH5xAH4AI3QABlNUUklOR3NxAH4A
G3QAM1Bvc2l0aW9uIG9yIG9mZnNldCBmcm9tIHRoZSBiZWdpbm5pbmcgb2YgdGhlIHN0cmVhbXBw
dAAQcG9zaXRpb25JblN0cmVhbXB+cQB+ACN0AARMT05Hc3EAfgAbdABBVHJ1ZSBvbiB0aGUgbGFz
dCBkYXRhIHBhY2tldCBvZiB0aGUgU3RyZWFtICh3aXRoIG9yIHdpdGhvdXQgZGF0YSlwcHQAC2Vu
ZE9mU3RyZWFtcHEAfgAsc3EAfgAbdAAsSW5kaWNhdGVzIGlmIHRoZSBmcmFtZSByYXRlIHJlbWFp
bnMgY29uc3RhbnRwcHQAE2NvbnN0YW50X2ZyYW1lX3JhdGVwcQB+ACxzcgBQY2EuZGlnaXRhbHJh
cGlkcy5rYXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5EYXRhVHlwZURlZmluaXRpb24k
Q29tcGxleFR5cGUAAAAAAAAAAQIAAkwACG9wdGlvbmFscQB+AB9MAAR0eXBlcQB+AAV4cQB+AB50
ACFEZXRhaWwgaW5mb3JtYXRpb24gZm9yIGVhY2ggdHJhY2twcQB+ADB0AAhzZXJ2aWNlc3B0AA5E
YXRhNjA4Q29udGVudHNxAH4AG3QALVRoZSBtb3N0IHJlbGV2YW50IHRpbWUgcGVydGFpbmluZyB0
byB0aGUgZGF0YXBwdAAEdGltZXBxAH4AJXNxAH4AG3QAGEluZGljYXRlcyB0aGUgZnJhbWUgcmF0
ZXBwdAAKZnJhbWVfcmF0ZXB+cQB+ACN0AAhSQVRJT05BTHNxAH4AG3QAI1RvdGFsIGxlbmd0aCBv
ZiB0aGUgc3RyZWFtIGlmIGtub3ducHB0AA5sZW5ndGhPZlN0cmVhbXBxAH4AcXhwc3IAEWphdmEu
dXRpbC5IYXNoTWFwBQfawcMWYNEDAAJGAApsb2FkRmFjdG9ySQAJdGhyZXNob2xkeHA/AAAAAAAA
IHcIAAAAQAAAAAJxAH4AMXNyACZqYXZhLnV0aWwuQ29sbGVjdGlvbnMkVW5tb2RpZmlhYmxlTGlz
dPwPJTG17I4QAgABTAAEbGlzdHEAfgAceHIALGphdmEudXRpbC5Db2xsZWN0aW9ucyRVbm1vZGlm
aWFibGVDb2xsZWN0aW9uGUIAgMte9x4CAAFMAAFjdAAWTGphdmEvdXRpbC9Db2xsZWN0aW9uO3hw
c3EAfgAyAAAAAXcEAAAAAXEAfgBmeHEAfgCPcQB+AHxzcQB+AItzcQB+ADIAAAAAdwQAAAAAeHEA
fgCReA==</property>
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
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAAQdAAJRnJhbWVSYXRldAANRGF0YUlzTWlzc2lu
Z3QAC01lZGlhVGltaW5ndAAURGF0YTcwOFNlcnZpY2VTYW1wbGV0ABFTYW1wbGVJbmZvcm1hdGlv
bnQAC0RhdGFTZXJ2aWNldAAORGF0YTcwOFNlcnZpY2V0ABFEYXRhU2VydmljZVNhbXBsZXQACFRl
bXBvcmFsdAALTWVkaWFTdHJlYW10AAtLYXlha0J1ZmZlcnQACkJ5dGVTdHJlYW10AAtNZWRpYU9y
aWdpbnQAEURhdGFTZXJ2aWNlU3RyZWFtdAAGU3RyZWFtdAAURGF0YTcwOFNlcnZpY2VTdHJlYW14
c3EAfgAIdwwAAAAQP0AAAAAAAAxzcgBPY2EuZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhdHlwZXMu
ZGVmaW5pdGlvbi5tb2RlbC5EYXRhVHlwZURlZmluaXRpb24kU2ltcGxlVHlwZQAAAAAAAAABAgAC
TAARZW51bWVyYXRpb25WYWx1ZXN0ABBMamF2YS91dGlsL0xpc3Q7TAAEdHlwZXQAQ0xjYS9kaWdp
dGFscmFwaWRzL2theWFrL2RhdGF0eXBlcy9kZWZpbml0aW9uL21vZGVsL1NpbXBsZVR5cGVzRW51
bTt4cgBSY2EuZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5E
YXRhVHlwZURlZmluaXRpb24kS2V5RGVmaW5pdGlvbgAAAAAAAAABAgAETAAHY29tbWVudHEAfgAF
TAALZGlzcGxheU5hbWVxAH4ABUwAC211bHRpVmFsdWVkdAATTGphdmEvbGFuZy9Cb29sZWFuO0wA
BG5hbWVxAH4ABXhwdABJVGhlIHRpbWUgcGVydGFpbmluZyB0byB0aGUgZW5kIG9mIHRoZSBkYXRh
ICh0aW1lICsgZHVyYXRpb24gb2YgdGhpcyBkYXRhKXBwdAAHdGltZUVuZHB+cgBBY2EuZGlnaXRh
bHJhcGlkcy5rYXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5TaW1wbGVUeXBlc0VudW0A
AAAAAAAAABIAAHhyAA5qYXZhLmxhbmcuRW51bQAAAAAAAAAAEgAAeHB0AARUSU1Fc3EAfgAbdAAc
SW5kaWNhdGVzIHRoZSBtZWRpYSBkdXJhdGlvbnBwdAAObWVkaWFfZHVyYXRpb25wcQB+ACVzcQB+
ABtwcHB0AA9kYXRhX2lzX21pc3NpbmdwfnEAfgAjdAAHQk9PTEVBTnNxAH4AG3QAM1Bvc2l0aW9u
IG9yIG9mZnNldCBmcm9tIHRoZSBiZWdpbm5pbmcgb2YgdGhlIHN0cmVhbXBwdAAQcG9zaXRpb25J
blN0cmVhbXB+cQB+ACN0AARMT05Hc3EAfgAbcHBzcgARamF2YS5sYW5nLkJvb2xlYW7NIHKA1Zz6
7gIAAVoABXZhbHVleHABdAAMbWVkaWFfb3JpZ2luc3IAE2phdmEudXRpbC5BcnJheUxpc3R4gdId
mcdhnQMAAUkABHNpemV4cAAAABV3BAAAABVzcgA3Y2EuZGlnaXRhbHJhcGlkcy5rYXlhay5wbHVn
aW4ueG1sLktheWFrRW51bWVyYXRpb25WYWx1ZQAAAAAAAAABAgAFTAALZGVzY3JpcHRpb25xAH4A
BUwAC2Rpc3BsYXlOYW1lcQB+AAVMAAZoaWRkZW5xAH4AH0wADnZhbHVlQXR0cmlidXRlcQB+AAVM
AA12YWx1ZUVtYmVkZGVkcQB+AAV4cHBwcHB0AAxNYW51ZmFjdHVyZWRzcQB+ADlwdAAIRFJDVmlk
ZW9wcQB+AD10AABzcQB+ADlwdAADR1hGcHEAfgBAcQB+AD5zcQB+ADlwdAADTFhGcHEAfgBCcQB+
AD5zcQB+ADlwdAADTVhGcHEAfgBEcQB+AD5zcQB+ADlwdAAJUXVpY2tUaW1lcHEAfgBGcQB+AD5z
cQB+ADlwdAANV2luZG93cyBNZWRpYXB0AAxXaW5kb3dzTWVkaWFxAH4APnNxAH4AOXB0AAlVc2Vy
IERhdGFwdAAJVXNlcl9kYXRhcQB+AD5zcQB+ADlwdAAOQW5jaWxsYXJ5IERhdGFwdAAOQW5jaWxs
YXJ5X2RhdGFxAH4APnNxAH4AOXB0AAJEVnBxAH4AUXEAfgA+c3EAfgA5cHQAA1ZDM3BxAH4AU3EA
fgA+c3EAfgA5cHQAFkFWQyBQaWN0dXJlIFRpbWluZyBTRUlwdAAWQVZDX1BpY3R1cmVfVGltaW5n
X1NFSXEAfgA+c3EAfgA5cHQAEE1QRUcyIEdPUCBIZWFkZXJwdAAQTVBFRzJfR09QX2hlYWRlcnEA
fgA+c3EAfgA5cHQAFE1YRiBNYXRlcmlhbCBQYWNrYWdlcHQAFE1YRl9tYXRlcmlhbF9wYWNrYWdl
cQB+AD5zcQB+ADlwdAAQTVhGIFN5c3RlbSBUcmFja3B0ABBNWEZfc3lzdGVtX3RyYWNrcQB+AD5z
cQB+ADlwdAATQW5jaWxsYXJ5IERhdGEgVklUQ3B0ABNBbmNpbGxhcnlfZGF0YV9WSVRDcQB+AD5z
cQB+ADlwdAASQW5jaWxsYXJ5IERhdGEgTFRDcHQAEkFuY2lsbGFyeV9kYXRhX0xUQ3EAfgA+c3EA
fgA5cHQAEFNDVEUyMCBVc2VyIERhdGFwdAAQVXNlcl9kYXRhX1NDVEUyMHEAfgA+c3EAfgA5cHQA
DkFUU0MgVXNlciBEYXRhcHQADlVzZXJfZGF0YV9BVFNDcQB+AD5zcQB+ADlwcQB+AGpwdAADU0ND
cQB+AD5zcQB+ADlwdAAZQW5jaWxsYXJ5IERhdGEgTGVnYWN5IDYwOHB0ABlBbmNpbGxhcnlfZGF0
YV9sZWdhY3lfNjA4cQB+AD54fnEAfgAjdAAGU1RSSU5Hc3EAfgAbdABBVHJ1ZSBvbiB0aGUgbGFz
dCBkYXRhIHBhY2tldCBvZiB0aGUgU3RyZWFtICh3aXRoIG9yIHdpdGhvdXQgZGF0YSlwcHQAC2Vu
ZE9mU3RyZWFtcHEAfgAsc3EAfgAbdAAsSW5kaWNhdGVzIGlmIHRoZSBmcmFtZSByYXRlIHJlbWFp
bnMgY29uc3RhbnRwcHQAE2NvbnN0YW50X2ZyYW1lX3JhdGVwcQB+ACxzcgBQY2EuZGlnaXRhbHJh
cGlkcy5rYXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5EYXRhVHlwZURlZmluaXRpb24k
Q29tcGxleFR5cGUAAAAAAAAAAQIAAkwACG9wdGlvbmFscQB+AB9MAAR0eXBlcQB+AAV4cQB+AB5w
cHEAfgA1dAAMY2RwX3NlcnZpY2VzcHQADkRhdGE2MDhDb250ZW50c3EAfgAbdAAtVGhlIG1vc3Qg
cmVsZXZhbnQgdGltZSBwZXJ0YWluaW5nIHRvIHRoZSBkYXRhcHB0AAR0aW1lcHEAfgAlc3EAfgAb
dAAYSW5kaWNhdGVzIHRoZSBmcmFtZSByYXRlcHB0AApmcmFtZV9yYXRlcH5xAH4AI3QACFJBVElP
TkFMc3EAfgB5dAAtT3B0aW9uYWwgdGltZWNvZGUgaW5mb3JtYXRpb24gaW4gdGhlIDcwOCBDRFBz
cHNxAH4ANAB0AAxjZHBfdGltZWNvZGVxAH4ANXQACFRpbWVjb2Rlc3EAfgAbdAAjVG90YWwgbGVu
Z3RoIG9mIHRoZSBzdHJlYW0gaWYga25vd25wcHQADmxlbmd0aE9mU3RyZWFtcHEAfgAxeHBzcgAR
amF2YS51dGlsLkhhc2hNYXAFB9rBwxZg0QMAAkYACmxvYWRGYWN0b3JJAAl0aHJlc2hvbGR4cD8A
AAAAAAAgdwgAAABAAAAAAnEAfgA2c3IAJmphdmEudXRpbC5Db2xsZWN0aW9ucyRVbm1vZGlmaWFi
bGVMaXN0/A8lMbXsjhACAAFMAARsaXN0cQB+ABx4cgAsamF2YS51dGlsLkNvbGxlY3Rpb25zJFVu
bW9kaWZpYWJsZUNvbGxlY3Rpb24ZQgCAy173HgIAAUwAAWN0ABZMamF2YS91dGlsL0NvbGxlY3Rp
b247eHBzcQB+ADcAAAABdwQAAAABcQB+AGt4cQB+AJNxAH4Ae3NxAH4Aj3NxAH4ANwAAAAB3BAAA
AAB4cQB+AJV4</property>
                        <pinDefinition name="Data708Service" displayName="EIA-708 Captions (ATSC User Data)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="Timecode 2" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAED9AAAAAAAAMdAAJRnJhbWVSYXRldAAOVGltZWNvZGVTdHJl
YW10AAhUZW1wb3JhbHQAC0theWFrQnVmZmVydAAIVGltZWNvZGV0AA1EYXRhSXNNaXNzaW5ndAAL
TWVkaWFUaW1pbmd0AAtNZWRpYU9yaWdpbnQABlN0cmVhbXQAEVNhbXBsZUluZm9ybWF0aW9udAAZ
VGltZWNvZGVTYW1wbGVJbmZvcm1hdGlvbnQADlRpbWVjb2RlU2FtcGxleHNxAH4ACHcMAAAAID9A
AAAAAAASc3IAT2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9k
ZWwuRGF0YVR5cGVEZWZpbml0aW9uJFNpbXBsZVR5cGUAAAAAAAAAAQIAAkwAEWVudW1lcmF0aW9u
VmFsdWVzdAAQTGphdmEvdXRpbC9MaXN0O0wABHR5cGV0AENMY2EvZGlnaXRhbHJhcGlkcy9rYXlh
ay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9TaW1wbGVUeXBlc0VudW07eHIAUmNhLmRpZ2l0
YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0
aW9uJEtleURlZmluaXRpb24AAAAAAAAAAQIABEwAB2NvbW1lbnRxAH4ABUwAC2Rpc3BsYXlOYW1l
cQB+AAVMAAttdWx0aVZhbHVlZHQAE0xqYXZhL2xhbmcvQm9vbGVhbjtMAARuYW1lcQB+AAV4cHQA
SVRoZSB0aW1lIHBlcnRhaW5pbmcgdG8gdGhlIGVuZCBvZiB0aGUgZGF0YSAodGltZSArIGR1cmF0
aW9uIG9mIHRoaXMgZGF0YSlwcHQAB3RpbWVFbmRwfnIAQWNhLmRpZ2l0YWxyYXBpZHMua2F5YWsu
ZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuU2ltcGxlVHlwZXNFbnVtAAAAAAAAAAASAAB4cgAO
amF2YS5sYW5nLkVudW0AAAAAAAAAABIAAHhwdAAEVElNRXNxAH4AF3QAHEluZGljYXRlcyB0aGUg
bWVkaWEgZHVyYXRpb25wcHQADm1lZGlhX2R1cmF0aW9ucHEAfgAhc3EAfgAXdABHdHJ1ZSBpZiB0
aGUgdGltZWNvZGUgcmVzZXQgdG8gMDA6MDA6MDA6MDAgd2hlbiByZWFjaGluZyBhIGNlcnRhaW4g
dmFsdWVwcHQADnRpbWVjb2RlX3Jlc2V0c3IAE2phdmEudXRpbC5BcnJheUxpc3R4gdIdmcdhnQMA
AUkABHNpemV4cAAAAAN3BAAAAANzcgA3Y2EuZGlnaXRhbHJhcGlkcy5rYXlhay5wbHVnaW4ueG1s
LktheWFrRW51bWVyYXRpb25WYWx1ZQAAAAAAAAABAgAFTAALZGVzY3JpcHRpb25xAH4ABUwAC2Rp
c3BsYXlOYW1lcQB+AAVMAAZoaWRkZW5xAH4AG0wADnZhbHVlQXR0cmlidXRlcQB+AAVMAA12YWx1
ZUVtYmVkZGVkcQB+AAV4cHB0AAROb25lcHQABG5vbmV0AABzcQB+ACtwdAAHMTIgaG91cnB0AAMx
MmhxAH4AL3NxAH4AK3B0AAcyNCBob3VycHQAAzI0aHEAfgAveH5xAH4AH3QABlNUUklOR3NxAH4A
F3Bwc3IAEWphdmEubGFuZy5Cb29sZWFuzSBygNWc+u4CAAFaAAV2YWx1ZXhwAXQADG1lZGlhX29y
aWdpbnNxAH4AKQAAABV3BAAAABVzcQB+ACtwcHBwdAAMTWFudWZhY3R1cmVkc3EAfgArcHQACERS
Q1ZpZGVvcHEAfgBAcQB+AC9zcQB+ACtwdAADR1hGcHEAfgBCcQB+AC9zcQB+ACtwdAADTFhGcHEA
fgBEcQB+AC9zcQB+ACtwdAADTVhGcHEAfgBGcQB+AC9zcQB+ACtwdAAJUXVpY2tUaW1lcHEAfgBI
cQB+AC9zcQB+ACtwdAANV2luZG93cyBNZWRpYXB0AAxXaW5kb3dzTWVkaWFxAH4AL3NxAH4AK3B0
AAlVc2VyIERhdGFwdAAJVXNlcl9kYXRhcQB+AC9zcQB+ACtwdAAOQW5jaWxsYXJ5IERhdGFwdAAO
QW5jaWxsYXJ5X2RhdGFxAH4AL3NxAH4AK3B0AAJEVnBxAH4AU3EAfgAvc3EAfgArcHQAA1ZDM3Bx
AH4AVXEAfgAvc3EAfgArcHQAFkFWQyBQaWN0dXJlIFRpbWluZyBTRUlwdAAWQVZDX1BpY3R1cmVf
VGltaW5nX1NFSXEAfgAvc3EAfgArcHQAEE1QRUcyIEdPUCBIZWFkZXJwdAAQTVBFRzJfR09QX2hl
YWRlcnEAfgAvc3EAfgArcHQAFE1YRiBNYXRlcmlhbCBQYWNrYWdlcHQAFE1YRl9tYXRlcmlhbF9w
YWNrYWdlcQB+AC9zcQB+ACtwdAAQTVhGIFN5c3RlbSBUcmFja3B0ABBNWEZfc3lzdGVtX3RyYWNr
cQB+AC9zcQB+ACtwdAATQW5jaWxsYXJ5IERhdGEgVklUQ3B0ABNBbmNpbGxhcnlfZGF0YV9WSVRD
cQB+AC9zcQB+ACtwdAASQW5jaWxsYXJ5IERhdGEgTFRDcHQAEkFuY2lsbGFyeV9kYXRhX0xUQ3EA
fgAvc3EAfgArcHQAEFNDVEUyMCBVc2VyIERhdGFwdAAQVXNlcl9kYXRhX1NDVEUyMHEAfgAvc3EA
fgArcHQADkFUU0MgVXNlciBEYXRhcHQADlVzZXJfZGF0YV9BVFNDcQB+AC9zcQB+ACtwcQB+AGxw
dAADU0NDcQB+AC9zcQB+ACtwdAAZQW5jaWxsYXJ5IERhdGEgTGVnYWN5IDYwOHB0ABlBbmNpbGxh
cnlfZGF0YV9sZWdhY3lfNjA4cQB+AC94cQB+ADZzcQB+ABd0ACJ0cnVlIGlmIHRoZSB0aW1lY29k
ZSBpcyBkcm9wIGZyYW1lcHB0ABN0aW1lY29kZV9kcm9wX2ZyYW1lcH5xAH4AH3QAB0JPT0xFQU5z
cQB+ABd0AEFUcnVlIG9uIHRoZSBsYXN0IGRhdGEgcGFja2V0IG9mIHRoZSBTdHJlYW0gKHdpdGgg
b3Igd2l0aG91dCBkYXRhKXBwdAALZW5kT2ZTdHJlYW1wcQB+AHZzcQB+ABd0ABhJbmRpY2F0ZXMg
dGhlIGZyYW1lIHJhdGVwcHQAE3RpbWVjb2RlX2ZyYW1lX3JhdGVwfnEAfgAfdAAHSU5URUdFUnNx
AH4AF3QAHkluZGljYXRlcyB0aGUgMzItYml0IHVzZXIgZGF0YXBwdAASdGltZWNvZGVfdXNlcl9i
aXRzcHEAfgB+c3EAfgAXdAAsSW5kaWNhdGVzIGlmIHRoZSBmcmFtZSByYXRlIHJlbWFpbnMgY29u
c3RhbnRwcHQAE2NvbnN0YW50X2ZyYW1lX3JhdGVwcQB+AHZzcQB+ABd0ABhJbmRpY2F0ZXMgdGhl
IGZyYW1lIHJhdGVwcHQACmZyYW1lX3JhdGVwfnEAfgAfdAAIUkFUSU9OQUxzcQB+ABdwcHB0AA9k
YXRhX2lzX21pc3NpbmdwcQB+AHZzcQB+ABd0ACNJbmRpY2F0ZXMgdGhlIHRpbWVjb2RlIGZpZWxk
IG51bWJlcnBwdAAOdGltZWNvZGVfZmllbGRzcQB+ACkAAAACdwQAAAACc3EAfgArcHQAATFwcQB+
AJJxAH4AL3NxAH4AK3B0AAEycHEAfgCUcQB+AC94cQB+AH5zcQB+ABd0ABFUaGUgdGltZWNvZGUg
dHlwZXBwdAANdGltZWNvZGVfdHlwZXNxAH4AKQAAAAN3BAAAAANzcQB+ACtwdAAIVGltZWNvZGVw
dAAIdGltZWNvZGVxAH4AL3NxAH4AK3B0ABFMb2NhbCB0aW1lIG9mIGRheXB0ABFsb2NhbF90aW1l
X29mX2RheXEAfgAvc3EAfgArcHQAD1VUQyB0aW1lIG9mIGRheXB0AA9VVENfdGltZV9vZl9kYXlx
AH4AL3hxAH4ANnNxAH4AF3QALVRoZSBtb3N0IHJlbGV2YW50IHRpbWUgcGVydGFpbmluZyB0byB0
aGUgZGF0YXBwdAAEdGltZXBxAH4AIXNxAH4AF3QAJ0luZGljYXRlcyBpZiB0aGUgdGltZWNvZGUg
aXMgY29udGludW91c3BwdAAWdGltZWNvZGVfaXNfY29udGludW91c3BxAH4AdnNxAH4AF3QAIElu
ZGljYXRlcyB0aGUgYmluYXJ5IGdyb3VwIGZsYWdzcHB0ABt0aW1lY29kZV9iaW5hcnlfZ3JvdXBf
ZmxhZ3NwcQB+AH5zcQB+ABd0AEF0cnVlIGlmIHRoZSB0aW1lY29kZSBzdHJlYW0gY29udGFpbnMg
b25lIHRpbWVjb2RlIHZhbHVlIHBlciBmaWVsZHBwdAAXdGltZWNvZGVfaXNfZmllbGRfYmFzZWRw
cQB+AHZzcQB+ABd0AEF0cnVlIGlmIHRoZSB0aW1lIGNvZGUgaXMgc3luY2hyb25pc2VkIHdpdGgg
YSBjb2xvciBmaWVsZCBzZXF1ZW5jZXBwdAAZdGltZWNvZGVfY29sb3JfZnJhbWVfZmxhZ3BxAH4A
dnhwc3IAEWphdmEudXRpbC5IYXNoTWFwBQfawcMWYNEDAAJGAApsb2FkRmFjdG9ySQAJdGhyZXNo
b2xkeHA/AAAAAAAAIHcIAAAAQAAAAAFxAH4AO3NyACZqYXZhLnV0aWwuQ29sbGVjdGlvbnMkVW5t
b2RpZmlhYmxlTGlzdPwPJTG17I4QAgABTAAEbGlzdHEAfgAYeHIALGphdmEudXRpbC5Db2xsZWN0
aW9ucyRVbm1vZGlmaWFibGVDb2xsZWN0aW9uGUIAgMte9x4CAAFMAAFjdAAWTGphdmEvdXRpbC9D
b2xsZWN0aW9uO3hwc3EAfgApAAAAAXcEAAAAAXEAfgBOeHEAfgC3eA==</property>
                        <pinDefinition name="Timecode 2" displayName="Timecode (328M)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="ActiveFormatDescriptionAndBarData" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAAQdAAJRnJhbWVSYXRldAAnQWN0aXZlRm9ybWF0
RGVzY3JpcHRpb25BbmRCYXJEYXRhU3RyZWFtdAAyQWN0aXZlRm9ybWF0RGVzY3JpcHRpb25BbmRC
YXJEYXRhU2FtcGxlSW5mb3JtYXRpb250AA1EYXRhSXNNaXNzaW5ndAALTWVkaWFUaW1pbmd0ABFT
YW1wbGVJbmZvcm1hdGlvbnQAF0FjdGl2ZUZvcm1hdERlc2NyaXB0aW9udAALQXNwZWN0UmF0aW90
ACFBY3RpdmVGb3JtYXREZXNjcmlwdGlvbkFuZEJhckRhdGF0AAhUZW1wb3JhbHQAC01lZGlhU3Ry
ZWFtdAALS2F5YWtCdWZmZXJ0AAdCYXJEYXRhdAAKQnl0ZVN0cmVhbXQAJ0FjdGl2ZUZvcm1hdERl
c2NyaXB0aW9uQW5kQmFyRGF0YVNhbXBsZXQABlN0cmVhbXhzcQB+AAh3DAAAACA/QAAAAAAAD3Ny
AE9jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZpbml0aW9uLm1vZGVsLkRhdGFU
eXBlRGVmaW5pdGlvbiRTaW1wbGVUeXBlAAAAAAAAAAECAAJMABFlbnVtZXJhdGlvblZhbHVlc3QA
EExqYXZhL3V0aWwvTGlzdDtMAAR0eXBldABDTGNhL2RpZ2l0YWxyYXBpZHMva2F5YWsvZGF0YXR5
cGVzL2RlZmluaXRpb24vbW9kZWwvU2ltcGxlVHlwZXNFbnVtO3hyAFJjYS5kaWdpdGFscmFwaWRz
LmtheWFrLmRhdGF0eXBlcy5kZWZpbml0aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlvbiRLZXlE
ZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21tZW50cQB+AAVMAAtkaXNwbGF5TmFtZXEAfgAFTAAL
bXVsdGlWYWx1ZWR0ABNMamF2YS9sYW5nL0Jvb2xlYW47TAAEbmFtZXEAfgAFeHB0AElUaGUgdGlt
ZSBwZXJ0YWluaW5nIHRvIHRoZSBlbmQgb2YgdGhlIGRhdGEgKHRpbWUgKyBkdXJhdGlvbiBvZiB0
aGlzIGRhdGEpcHB0AAd0aW1lRW5kcH5yAEFjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBl
cy5kZWZpbml0aW9uLm1vZGVsLlNpbXBsZVR5cGVzRW51bQAAAAAAAAAAEgAAeHIADmphdmEubGFu
Zy5FbnVtAAAAAAAAAAASAAB4cHQABFRJTUVzcQB+ABt0ABxJbmRpY2F0ZXMgdGhlIG1lZGlhIGR1
cmF0aW9ucHB0AA5tZWRpYV9kdXJhdGlvbnBxAH4AJXNxAH4AG3QAQVRydWUgb24gdGhlIGxhc3Qg
ZGF0YSBwYWNrZXQgb2YgdGhlIFN0cmVhbSAod2l0aCBvciB3aXRob3V0IGRhdGEpcHB0AAtlbmRP
ZlN0cmVhbXB+cQB+ACN0AAdCT09MRUFOc3EAfgAbdABZRmlyc3QgbGluZSBvZiBhIGhvcml6b250
YWwgbGV0dGVyYm94IGJvdHRvbSBiYXIgYXJlYS4gRXhwcmVzc2VkIHVzaW5nIFNNUFRFIGxpbmUg
bnVtYmVycy5wcHQAHGJvdHRvbV9iYXJfbGluZV9udW1iZXJfc3RhcnRwfnEAfgAjdAAHSU5URUdF
UnNxAH4AG3QALEluZGljYXRlcyBpZiB0aGUgZnJhbWUgcmF0ZSByZW1haW5zIGNvbnN0YW50cHB0
ABNjb25zdGFudF9mcmFtZV9yYXRlcHEAfgAtc3EAfgAbdABARmlyc3QgaG9yaXpvbnRhbCBsdW1p
bmFuY2Ugc2FtcGxlIG9mIGEgcGlsbGFyYm94IHJpZ2h0IGJhciBhcmVhLnBwdAAWcmlnaHRfYmFy
X3BpeGVsX251bWJlcnBxAH4AMnNxAH4AG3QAGEluZGljYXRlcyB0aGUgZnJhbWUgcmF0ZXBwdAAK
ZnJhbWVfcmF0ZXB+cQB+ACN0AAhSQVRJT05BTHNxAH4AG3BwcHQAD2RhdGFfaXNfbWlzc2luZ3Bx
AH4ALXNxAH4AG3QAM1Bvc2l0aW9uIG9yIG9mZnNldCBmcm9tIHRoZSBiZWdpbm5pbmcgb2YgdGhl
IHN0cmVhbXBwdAAQcG9zaXRpb25JblN0cmVhbXB+cQB+ACN0AARMT05Hc3EAfgAbdAAiSW5kaWNh
dGVzIHRoZSBkaXNwbGF5IGFzcGVjdCByYXRpb3BwdAAUZGlzcGxheV9hc3BlY3RfcmF0aW9wcQB+
AD1zcQB+ABt0AD5MYXN0IGhvcml6b250YWwgbHVtaW5hbmNlIHNhbXBsZSBvZiBhIHBpbGxhcmJv
eCBsZWZ0IGJhciBhcmVhLnBwdAAVbGVmdF9iYXJfcGl4ZWxfbnVtYmVycHEAfgAyc3EAfgAbdABU
RGVzY3JpYmVzIHRoZSAnYXJlYSBvZiBpbnRlcmVzdCcgaW4gdGVybXMgb2YgaXRzIGFzcGVjdCBy
YXRpbyB3aXRoaW4gdGhlIGNvZGVkIGZyYW1lcHB0AA1hY3RpdmVfZm9ybWF0c3IAE2phdmEudXRp
bC5BcnJheUxpc3R4gdIdmcdhnQMAAUkABHNpemV4cAAAABB3BAAAABBzcgA3Y2EuZGlnaXRhbHJh
cGlkcy5rYXlhay5wbHVnaW4ueG1sLktheWFrRW51bWVyYXRpb25WYWx1ZQAAAAAAAAABAgAFTAAL
ZGVzY3JpcHRpb25xAH4ABUwAC2Rpc3BsYXlOYW1lcQB+AAVMAAZoaWRkZW5xAH4AH0wADnZhbHVl
QXR0cmlidXRlcQB+AAVMAA12YWx1ZUVtYmVkZGVkcQB+AAV4cHB0AAxub24tc3RhbmRhcmRwdAAB
MHQAAHNxAH4AUXB0AAhyZXNlcnZlZHB0AAExcQB+AFVzcQB+AFFwdAAOYm94IDE2OjkgKHRvcClw
dAABMnEAfgBVc3EAfgBRcHQADmJveCAxNDo5ICh0b3ApcHQAATNxAH4AVXNxAH4AUXB0ABBib3gg
PiAxNjo5ICh0b3ApcHQAATRxAH4AVXNxAH4AUXBxAH4AV3B0AAE1cQB+AFVzcQB+AFFwcQB+AFdw
dAABNnEAfgBVc3EAfgBRcHEAfgBXcHQAATdxAH4AVXNxAH4AUXB0ACxhY3RpdmUgZm9ybWF0IGlz
IHRoZSBzYW1lIGFzIHRoZSBjb2RlZCBmcmFtZXB0AAE4cQB+AFVzcQB+AFFwdAAMNDozIChjZW50
cmUpcHQAATlxAH4AVXNxAH4AUXB0AA0xNjo5IChjZW50cmUpcHQAAjEwcQB+AFVzcQB+AFFwdAAN
MTQ6OSAoY2VudHJlKXB0AAIxMXEAfgBVc3EAfgBRcHEAfgBXcHQAAjEycQB+AFVzcQB+AFFwdAAq
NDozICh3aXRoIHNob290IGFuZCBwcm90ZWN0ZWQgMTQ6OSBjZW50cmUpcHQAAjEzcQB+AFVzcQB+
AFFwdAArMTY6OSAod2l0aCBzaG9vdCBhbmQgcHJvdGVjdGVkIDE0OjkgY2VudHJlKXB0AAIxNHEA
fgBVc3EAfgBRcHQAKjE2OjkgKHdpdGggc2hvb3QgYW5kIHByb3RlY3RlZCA0OjMgY2VudHJlKXB0
AAIxNXEAfgBVeHEAfgAyc3EAfgAbdAAtVGhlIG1vc3QgcmVsZXZhbnQgdGltZSBwZXJ0YWluaW5n
IHRvIHRoZSBkYXRhcHB0AAR0aW1lcHEAfgAlc3EAfgAbdABVTGFzdCBsaW5lIG9mIGEgaG9yaXpv
bnRhbCBsZXR0ZXJib3ggdG9wIGJhciBhcmVhLiBFeHByZXNzZWQgdXNpbmcgU01QVEUgbGluZSBu
dW1iZXJzLnBwdAAXdG9wX2Jhcl9saW5lX251bWJlcl9lbmRwcQB+ADJzcQB+ABt0ACNUb3RhbCBs
ZW5ndGggb2YgdGhlIHN0cmVhbSBpZiBrbm93bnBwdAAObGVuZ3RoT2ZTdHJlYW1wcQB+AER4cHNy
ABFqYXZhLnV0aWwuSGFzaE1hcAUH2sHDFmDRAwACRgAKbG9hZEZhY3RvckkACXRocmVzaG9sZHhw
PwAAAAAAAEB3CAAAAEAAAAAAeA==</property>
                        <pinDefinition name="ActiveFormatDescriptionAndBarData" displayName="Active Format Description" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="MPEGUserData" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAAOdAAXTVBFR1VEU2FtcGxlSW5mb3JtYXRpb250
AAlGcmFtZVJhdGV0ABZWaWRlb1NhbXBsZUluZm9ybWF0aW9udAASTVBFR1VzZXJEYXRhU3RyZWFt
dAANRGF0YUlzTWlzc2luZ3QAC01lZGlhVGltaW5ndAAMTVBFR1VzZXJEYXRhdAARU2FtcGxlSW5m
b3JtYXRpb250ABJNUEVHVXNlckRhdGFTYW1wbGV0AAhUZW1wb3JhbHQAC01lZGlhU3RyZWFtdAAL
S2F5YWtCdWZmZXJ0AApCeXRlU3RyZWFtdAAGU3RyZWFteHNxAH4ACHcMAAAAED9AAAAAAAALc3IA
T2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5
cGVEZWZpbml0aW9uJFNpbXBsZVR5cGUAAAAAAAAAAQIAAkwAEWVudW1lcmF0aW9uVmFsdWVzdAAQ
TGphdmEvdXRpbC9MaXN0O0wABHR5cGV0AENMY2EvZGlnaXRhbHJhcGlkcy9rYXlhay9kYXRhdHlw
ZXMvZGVmaW5pdGlvbi9tb2RlbC9TaW1wbGVUeXBlc0VudW07eHIAUmNhLmRpZ2l0YWxyYXBpZHMu
a2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJEtleURl
ZmluaXRpb24AAAAAAAAAAQIABEwAB2NvbW1lbnRxAH4ABUwAC2Rpc3BsYXlOYW1lcQB+AAVMAAtt
dWx0aVZhbHVlZHQAE0xqYXZhL2xhbmcvQm9vbGVhbjtMAARuYW1lcQB+AAV4cHQASVRoZSB0aW1l
IHBlcnRhaW5pbmcgdG8gdGhlIGVuZCBvZiB0aGUgZGF0YSAodGltZSArIGR1cmF0aW9uIG9mIHRo
aXMgZGF0YSlwcHQAB3RpbWVFbmRwfnIAQWNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVz
LmRlZmluaXRpb24ubW9kZWwuU2ltcGxlVHlwZXNFbnVtAAAAAAAAAAASAAB4cgAOamF2YS5sYW5n
LkVudW0AAAAAAAAAABIAAHhwdAAEVElNRXNxAH4AGXQAHEluZGljYXRlcyB0aGUgbWVkaWEgZHVy
YXRpb25wcHQADm1lZGlhX2R1cmF0aW9ucHEAfgAjc3EAfgAZcHBwdAAPZGF0YV9pc19taXNzaW5n
cH5xAH4AIXQAB0JPT0xFQU5zcQB+ABl0ADNQb3NpdGlvbiBvciBvZmZzZXQgZnJvbSB0aGUgYmVn
aW5uaW5nIG9mIHRoZSBzdHJlYW1wcHQAEHBvc2l0aW9uSW5TdHJlYW1wfnEAfgAhdAAETE9OR3Nx
AH4AGXQAJkluZGljYXRlcyB0aGUgdHlwZSBvZiB1c2VyIGRhdGEgc2FtcGxlcHB0AA51c2VyX2Rh
dGFfdHlwZXNyABNqYXZhLnV0aWwuQXJyYXlMaXN0eIHSHZnHYZ0DAAFJAARzaXpleHAAAAAEdwQA
AAAEc3IAN2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsucGx1Z2luLnhtbC5LYXlha0VudW1lcmF0aW9u
VmFsdWUAAAAAAAAAAQIABUwAC2Rlc2NyaXB0aW9ucQB+AAVMAAtkaXNwbGF5TmFtZXEAfgAFTAAG
aGlkZGVucQB+AB1MAA52YWx1ZUF0dHJpYnV0ZXEAfgAFTAANdmFsdWVFbWJlZGRlZHEAfgAFeHBw
cHBwdAAHVW5rbm93bnNxAH4ANnBwcHB0AAZBVFNDNTNzcQB+ADZwcHBwdAAGU0NURTIwc3EAfgA2
cHBwcHQACFNNUFRFMzI4eH5xAH4AIXQABlNUUklOR3NxAH4AGXQAQVRydWUgb24gdGhlIGxhc3Qg
ZGF0YSBwYWNrZXQgb2YgdGhlIFN0cmVhbSAod2l0aCBvciB3aXRob3V0IGRhdGEpcHB0AAtlbmRP
ZlN0cmVhbXBxAH4AKnNxAH4AGXQALEluZGljYXRlcyBpZiB0aGUgZnJhbWUgcmF0ZSByZW1haW5z
IGNvbnN0YW50cHB0ABNjb25zdGFudF9mcmFtZV9yYXRlcHEAfgAqc3EAfgAZdAAtVGhlIG1vc3Qg
cmVsZXZhbnQgdGltZSBwZXJ0YWluaW5nIHRvIHRoZSBkYXRhcHB0AAR0aW1lcHEAfgAjc3EAfgAZ
dAAYSW5kaWNhdGVzIHRoZSBmcmFtZSByYXRlcHB0AApmcmFtZV9yYXRlcH5xAH4AIXQACFJBVElP
TkFMc3EAfgAZdAAzSW5kaWNhdGVzIHRoZSBpbnRlcmxhY2luZyB0eXBlIG9mIHRoaXMgdmlkZW8g
c2FtcGxlcHB0ABF2aWRlb19zYW1wbGVfdHlwZXNxAH4ANAAAAAN3BAAAAANzcQB+ADZwdAAFRnJh
bWVwdAAFZnJhbWV0AABzcQB+ADZwdAAJVG9wIGZpZWxkcHQACXRvcF9maWVsZHEAfgBWc3EAfgA2
cHQADEJvdHRvbSBmaWVsZHB0AAxib3R0b21fZmllbGRxAH4AVnhxAH4AP3NxAH4AGXQAI1RvdGFs
IGxlbmd0aCBvZiB0aGUgc3RyZWFtIGlmIGtub3ducHB0AA5sZW5ndGhPZlN0cmVhbXBxAH4AL3hw
c3IAEWphdmEudXRpbC5IYXNoTWFwBQfawcMWYNEDAAJGAApsb2FkRmFjdG9ySQAJdGhyZXNob2xk
eHA/AAAAAAAAQHcIAAAAQAAAAAB4</property>
                        <pinDefinition name="MPEGUserData" displayName="User Data" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="CompressedVideo" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAQD9AAAAAAAAadAAIT3ZlcnNjYW50ABZWaWRlb1NhbXBsZUlu
Zm9ybWF0aW9udAAQVmlkZW9JbnRlcmxhY2luZ3QAC01lZGlhVGltaW5ndAAQTWVkaWFSYXRlQ29u
dHJvbHQAC0FWQ01ldGFkYXRhdAAITGFuZ3VhZ2V0AAhUZW1wb3JhbHQACkJ5dGVTdHJlYW10AAZT
dHJlYW10AAtWaWRlb1NhbXBsZXQAEkFWQ0ZyYW1lUmVvcmRlcmluZ3QACUZyYW1lUmF0ZXQAC1Zp
ZGVvU3RyZWFtdAAOSW1hZ2VEaW1lbnNpb250AA9GcmFtZVJlb3JkZXJpbmd0AA5BVkNWaWRlb1N0
cmVhbXQAEVNhbXBsZUluZm9ybWF0aW9udAAOVmlkZW9EaW1lbnNpb250AA5BVkNWaWRlb1NhbXBs
ZXQAC0FzcGVjdFJhdGlvdAALTWVkaWFTdHJlYW10AAtLYXlha0J1ZmZlcnQABVZpZGVvdAAZQVZD
VmlkZW9TYW1wbGVJbmZvcm1hdGlvbnQAEVJhbmRvbUFjY2Vzc1BvaW50eHNxAH4ACHcMAAAAgD9A
AAAAAAAzc3IAT2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9k
ZWwuRGF0YVR5cGVEZWZpbml0aW9uJFNpbXBsZVR5cGUAAAAAAAAAAQIAAkwAEWVudW1lcmF0aW9u
VmFsdWVzdAAQTGphdmEvdXRpbC9MaXN0O0wABHR5cGV0AENMY2EvZGlnaXRhbHJhcGlkcy9rYXlh
ay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9TaW1wbGVUeXBlc0VudW07eHIAUmNhLmRpZ2l0
YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0
aW9uJEtleURlZmluaXRpb24AAAAAAAAAAQIABEwAB2NvbW1lbnRxAH4ABUwAC2Rpc3BsYXlOYW1l
cQB+AAVMAAttdWx0aVZhbHVlZHQAE0xqYXZhL2xhbmcvQm9vbGVhbjtMAARuYW1lcQB+AAV4cHQA
LkluZGljYXRlcyB0aGUgaW5pdGlhbCBDUEIgcmVtb3ZhbCBkZWxheSBvZmZzZXRwcHQAJGF2Y19p
bml0aWFsX2NwYl9yZW1vdmFsX2RlbGF5X29mZnNldHB+cgBBY2EuZGlnaXRhbHJhcGlkcy5rYXlh
ay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5TaW1wbGVUeXBlc0VudW0AAAAAAAAAABIAAHhy
AA5qYXZhLmxhbmcuRW51bQAAAAAAAAAAEgAAeHB0AAdJTlRFR0VSc3EAfgAldAAqSW5kaWNhdGVz
IHRoZSBpbnRlcmxhY2luZyBhbmQgZmllbGQgbGF5b3V0cHB0AAxmcmFtZV9sYXlvdXRzcgATamF2
YS51dGlsLkFycmF5TGlzdHiB0h2Zx2GdAwABSQAEc2l6ZXhwAAAAA3cEAAAAA3NyADdjYS5kaWdp
dGFscmFwaWRzLmtheWFrLnBsdWdpbi54bWwuS2F5YWtFbnVtZXJhdGlvblZhbHVlAAAAAAAAAAEC
AAVMAAtkZXNjcmlwdGlvbnEAfgAFTAALZGlzcGxheU5hbWVxAH4ABUwABmhpZGRlbnEAfgApTAAO
dmFsdWVBdHRyaWJ1dGVxAH4ABUwADXZhbHVlRW1iZWRkZWRxAH4ABXhwcHQAC1Byb2dyZXNzaXZl
cHQAC3Byb2dyZXNzaXZldAAAc3EAfgA2cHQACkludGVybGFjZWRwdAAKaW50ZXJsYWNlZHEAfgA6
c3EAfgA2cHQADFNpbmdsZSBmaWVsZHB0AAxzaW5nbGVfZmllbGRxAH4AOnh+cQB+AC10AAZTVFJJ
TkdzcQB+ACV0ACdJbmRpY2F0ZXMgdGhlIGluaXRpYWwgQ1BCIHJlbW92YWwgZGVsYXlwcHQAHWF2
Y19pbml0aWFsX2NwYl9yZW1vdmFsX2RlbGF5cHEAfgAvc3EAfgAldAA0SW5kaWNhdGVzIHRoZSBz
dGF0dXMgb2YgdGhlIGVudHJvcHkgY29kaW5nIG1vZGUgZmxhZ3BwdAAcYXZjX2VudHJvcHlfY29k
aW5nX21vZGVfZmxhZ3B+cQB+AC10AAdCT09MRUFOc3EAfgAldAAXQ2Fub25pY2FsIExhbmd1YWdl
IENvZGVwcHQADWxhbmd1YWdlX2NvZGVzcQB+ADQAAAC6dwQAAAC6c3EAfgA2cHQACVVuZGVmaW5l
ZHB0AAN1bmRxAH4AOnNxAH4ANnB0AA5Ob3QgQXBwbGljYWJsZXB0AAN6eHhxAH4AOnNxAH4ANnB0
ABFBYmtoYXppYW4sIEFia2hhenB0AAJhYnEAfgA6c3EAfgA2cHQABEFmYXJwdAACYWFxAH4AOnNx
AH4ANnB0AAlBZnJpa2FhbnNwdAACYWZxAH4AOnNxAH4ANnB0AARBa2FucHQAAmFrcQB+ADpzcQB+
ADZwdAAIQWxiYW5pYW5wdAACc3FxAH4AOnNxAH4ANnB0AAdBbWhhcmljcHQAAmFtcQB+ADpzcQB+
ADZwdAAGQXJhYmljcHQAAmFycQB+ADpzcQB+ADZwdAAJQXJhZ29uZXNlcHQAAmFucQB+ADpzcQB+
ADZwdAAIQXJtZW5pYW5wdAACaHlxAH4AOnNxAH4ANnB0AAhBc3NhbWVzZXB0AAJhc3EAfgA6c3EA
fgA2cHQABkF2YXJpY3B0AAJhdnEAfgA6c3EAfgA2cHQAB0F2ZXN0YW5wdAACYWVxAH4AOnNxAH4A
NnB0AAZBeW1hcmFwdAACYXlxAH4AOnNxAH4ANnB0AAtBemVyYmFpamFuaXB0AAJhenEAfgA6c3EA
fgA2cHQAB0JhbWJhcmFwdAACYm1xAH4AOnNxAH4ANnB0AAdCYXNoa2lycHQAAmJhcQB+ADpzcQB+
ADZwdAAGQmFzcXVlcHQAAmV1cQB+ADpzcQB+ADZwdAAKQmVsYXJ1c2lhbnB0AAJiZXEAfgA6c3EA
fgA2cHQAB0JlbmdhbGlwdAACYm5xAH4AOnNxAH4ANnB0ABBCaWhhcmkgTGFuZ3VhZ2VzcHQAAmJo
cQB+ADpzcQB+ADZwdAAHQmlzbGFtYXB0AAJiaXEAfgA6c3EAfgA2cHQAB0Jvc25pYW5wdAACYnNx
AH4AOnNxAH4ANnB0AAZCcmV0b25wdAACYnJxAH4AOnNxAH4ANnB0AAlCdWxnYXJpYW5wdAACYmdx
AH4AOnNxAH4ANnB0AAdCdXJtZXNlcHQAAm15cQB+ADpzcQB+ADZwdAASQ2F0YWxhbiwgVmFsZW5j
aWFucHQAAmNhcQB+ADpzcQB+ADZwdAAIQ2hhbW9ycm9wdAACY2hxAH4AOnNxAH4ANnB0AAdDaGVj
aGVucHQAAmNlcQB+ADpzcQB+ADZwdAAXQ2hpY2hld2EsIENoZXdhLCBOeWFuamFwdAACbnlxAH4A
OnNxAH4ANnB0AAdDaGluZXNlcHQAAnpocQB+ADpzcQB+ADZwdAAcQ2h1cmNoIFNsYXZpYywgT2xk
IEJ1bGdhcmlhbnB0AAJjdXEAfgA6c3EAfgA2cHQAB0NodXZhc2hwdAACY3ZxAH4AOnNxAH4ANnB0
AAdDb3JuaXNocHQAAmt3cQB+ADpzcQB+ADZwdAAIQ29yc2ljYW5wdAACY29xAH4AOnNxAH4ANnB0
AARDcmVlcHQAAmNycQB+ADpzcQB+ADZwdAAIQ3JvYXRpYW5wdAACaHJxAH4AOnNxAH4ANnB0AAVD
emVjaHB0AAJjc3EAfgA6c3EAfgA2cHQABkRhbmlzaHB0AAJkYXEAfgA6c3EAfgA2cHQAGkRpdmVo
aSwgRGhpdmVoaSwgTWFsZGl2aWFucHQAAmR2cQB+ADpzcQB+ADZwdAAORHV0Y2gsIEZsZW1pc2hw
dAACbmxxAH4AOnNxAH4ANnB0AAhEem9uZ2toYXB0AAJkenEAfgA6c3EAfgA2cHQAB0VuZ2xpc2hw
dAACZW5xAH4AOnNxAH4ANnB0AAlFc3BlcmFudG9wdAACZW9xAH4AOnNxAH4ANnB0AAhFc3Rvbmlh
bnB0AAJldHEAfgA6c3EAfgA2cHQAA0V3ZXB0AAJlZXEAfgA6c3EAfgA2cHQAB0Zhcm9lc2VwdAAC
Zm9xAH4AOnNxAH4ANnB0AAZGaWppYW5wdAACZmpxAH4AOnNxAH4ANnB0AAdGaW5uaXNocHQAAmZp
cQB+ADpzcQB+ADZwdAAGRnJlbmNocHQAAmZycQB+ADpzcQB+ADZwdAAFRnVsYWhwdAACZmZxAH4A
OnNxAH4ANnB0AAhHYWxpY2lhbnB0AAJnbHEAfgA6c3EAfgA2cHQABUdhbmRhcHQAAmxncQB+ADpz
cQB+ADZwdAAIR2VvcmdpYW5wdAACa2FxAH4AOnNxAH4ANnB0AAZHZXJtYW5wdAACZGVxAH4AOnNx
AH4ANnB0AAdHdWFyYW5pcHQAAmducQB+ADpzcQB+ADZwdAAIR3VqYXJhdGlwdAACZ3VxAH4AOnNx
AH4ANnB0AA9IYWl0aWFuLCBDcmVvbGVwdAACaHRxAH4AOnNxAH4ANnB0AAVIYXVzYXB0AAJoYXEA
fgA6c3EAfgA2cHQABkhlYnJld3B0AAJoZXEAfgA6c3EAfgA2cHQABkhlcmVyb3B0AAJoenEAfgA6
c3EAfgA2cHQABUhpbmRpcHQAAmhpcQB+ADpzcQB+ADZwdAAJSGlyaSBNb3R1cHQAAmhvcQB+ADpz
cQB+ADZwdAAJSHVuZ2FyaWFucHQAAmh1cQB+ADpzcQB+ADZwdAAJSWNlbGFuZGljcHQAAmlzcQB+
ADpzcQB+ADZwdAADSWRvcHQAAmlvcQB+ADpzcQB+ADZwdAAESWdib3B0AAJpZ3EAfgA6c3EAfgA2
cHQACkluZG9uZXNpYW5wdAACaWRxAH4AOnNxAH4ANnB0AAtJbnRlcmxpbmd1YXB0AAJpYXEAfgA6
c3EAfgA2cHQAF0ludGVybGluZ3VlLCBPY2NpZGVudGFscHQAAmllcQB+ADpzcQB+ADZwdAAJSW51
a3RpdHV0cHQAAml1cQB+ADpzcQB+ADZwdAAHSW51cGlhcXB0AAJpa3EAfgA6c3EAfgA2cHQABUly
aXNocHQAAmdhcQB+ADpzcQB+ADZwdAAHSXRhbGlhbnB0AAJpdHEAfgA6c3EAfgA2cHQACEphcGFu
ZXNlcHQAAmphcQB+ADpzcQB+ADZwdAAISmF2YW5lc2VwdAACanZxAH4AOnNxAH4ANnB0ABhLYWxh
YWxsaXN1dCwgR3JlZW5sYW5kaWNwdAACa2xxAH4AOnNxAH4ANnB0AAdLYW5uYWRhcHQAAmtucQB+
ADpzcQB+ADZwdAAGS2FudXJpcHQAAmtycQB+ADpzcQB+ADZwdAAIS2FzaG1pcmlwdAACa3NxAH4A
OnNxAH4ANnB0AAZLYXpha2hwdAACa2txAH4AOnNxAH4ANnB0AAVLaG1lcnB0AAJrbXEAfgA6c3EA
fgA2cHQADktpa3V5dSwgR2lrdXl1cHQAAmtpcQB+ADpzcQB+ADZwdAALS2lueWFyd2FuZGFwdAAC
cndxAH4AOnNxAH4ANnB0AA9LaXJnaGl6LCBLeXJneXpwdAACa3lxAH4AOnNxAH4ANnB0AAdLaXJ1
bmRpcHQAAnJucQB+ADpzcQB+ADZwdAAES29taXB0AAJrdnEAfgA6c3EAfgA2cHQABUtvbmdvcHQA
AmtncQB+ADpzcQB+ADZwdAAGS29yZWFucHQAAmtvcQB+ADpzcQB+ADZwdAASS3VhbnlhbWEsIEt3
YW55YW1hcHQAAmtqcQB+ADpzcQB+ADZwdAAHS3VyZGlzaHB0AAJrdXEAfgA6c3EAfgA2cHQAA0xh
b3B0AAJsb3EAfgA6c3EAfgA2cHQABUxhdGlucHQAAmxhcQB+ADpzcQB+ADZwdAAHTGF0dmlhbnB0
AAJsdnEAfgA6c3EAfgA2cHQAIExpbWJ1cmdhbiwgTGltYnVyZ2VyLCBMaW1idXJnaXNocHQAAmxp
cQB+ADpzcQB+ADZwdAAHTGluZ2FsYXB0AAJsbnEAfgA6c3EAfgA2cHQACkxpdGh1YW5pYW5wdAAC
bHRxAH4AOnNxAH4ANnB0AAxMdWJhLUthdGFuZ2FwdAACbHVxAH4AOnNxAH4ANnB0ABxMdXhlbWJv
dXJnaXNoLCBMZXR6ZWJ1cmdlc2NocHQAAmxicQB+ADpzcQB+ADZwdAAKTWFjZWRvbmlhbnB0AAJt
a3EAfgA6c3EAfgA2cHQACE1hbGFnYXN5cHQAAm1ncQB+ADpzcQB+ADZwdAAFTWFsYXlwdAACbXNx
AH4AOnNxAH4ANnB0AAlNYWxheWFsYW1wdAACbWxxAH4AOnNxAH4ANnB0AAdNYWx0ZXNlcHQAAm10
cQB+ADpzcQB+ADZwdAAETWFueHB0AAJndnEAfgA6c3EAfgA2cHQABU1hb3JpcHQAAm1pcQB+ADpz
cQB+ADZwdAAHTWFyYXRoaXB0AAJtcnEAfgA6c3EAfgA2cHQAC01hcnNoYWxsZXNlcHQAAm1ocQB+
ADpzcQB+ADZwdAAMTW9kZXJuIEdyZWVrcHQAAmVscQB+ADpzcQB+ADZwdAAJTW9uZ29saWFucHQA
Am1ucQB+ADpzcQB+ADZwdAAFTmF1cnVwdAACbmFxAH4AOnNxAH4ANnB0AA5OYXZham8sIE5hdmFo
b3B0AAJudnEAfgA6c3EAfgA2cHQABk5kb25nYXB0AAJuZ3EAfgA6c3EAfgA2cHQABk5lcGFsaXB0
AAJuZXEAfgA6c3EAfgA2cHQADU5vcnRoIE5kZWJlbGVwdAACbmRxAH4AOnNxAH4ANnB0AA1Ob3J0
aGVybiBTYW1pcHQAAnNlcQB+ADpzcQB+ADZwdAAJTm9yd2VnaWFucHQAAm5vcQB+ADpzcQB+ADZw
dAARTm9yd2VnaWFuIEJva23DpWxwdAACbmJxAH4AOnNxAH4ANnB0ABFOb3J3ZWdpYW4gTnlub3Jz
a3B0AAJubnEAfgA6c3EAfgA2cHQAE09jY2l0YW4gKHBvc3QgMTUwMClwdAACb2NxAH4AOnNxAH4A
NnB0AAZPamlid2FwdAACb2pxAH4AOnNxAH4ANnB0AAVPcml5YXB0AAJvcnEAfgA6c3EAfgA2cHQA
BU9yb21vcHQAAm9tcQB+ADpzcQB+ADZwdAART3NzZXRpYW4sIE9zc2V0aWNwdAACb3NxAH4AOnNx
AH4ANnB0AARQYWxpcHQAAnBpcQB+ADpzcQB+ADZwdAAQUGFuamFiaSwgUHVuamFiaXB0AAJwYXEA
fgA6c3EAfgA2cHQAB1BlcnNpYW5wdAACZmFxAH4AOnNxAH4ANnB0AAZQb2xpc2hwdAACcGxxAH4A
OnNxAH4ANnB0AApQb3J0dWd1ZXNlcHQAAnB0cQB+ADpzcQB+ADZwdAAOUHVzaHRvLCBQYXNodG9w
dAACcHNxAH4AOnNxAH4ANnB0AAdRdWVjaHVhcHQAAnF1cQB+ADpzcQB+ADZwdAAdUm9tYW5pYW4s
IE1vbGRhdmlhbiwgTW9sZG92YW5wdAACcm9xAH4AOnNxAH4ANnB0AAdSb21hbnNocHQAAnJtcQB+
ADpzcQB+ADZwdAAHUnVzc2lhbnB0AAJydXEAfgA6c3EAfgA2cHQABlNhbW9hbnB0AAJzbXEAfgA6
c3EAfgA2cHQABVNhbmdvcHQAAnNncQB+ADpzcQB+ADZwdAAIU2Fuc2tyaXRwdAACc2FxAH4AOnNx
AH4ANnB0AAlTYXJkaW5pYW5wdAACc2NxAH4AOnNxAH4ANnB0AA9TY290dGlzaCBHYWVsaWNwdAAC
Z2RxAH4AOnNxAH4ANnB0AAdTZXJiaWFucHQAAnNycQB+ADpzcQB+ADZwdAAFU2hvbmFwdAACc25x
AH4AOnNxAH4ANnB0ABFTaWNodWFuIFlpLCBOdW9zdXB0AAJpaXEAfgA6c3EAfgA2cHQABlNpbmRo
aXB0AAJzZHEAfgA6c3EAfgA2cHQAElNpbmhhbGEsIFNpbmhhbGVzZXB0AAJzaXEAfgA6c3EAfgA2
cHQABlNsb3Zha3B0AAJza3EAfgA6c3EAfgA2cHQACVNsb3ZlbmlhbnB0AAJzbHEAfgA6c3EAfgA2
cHQABlNvbWFsaXB0AAJzb3EAfgA6c3EAfgA2cHQADVNvdXRoIE5kZWJlbGVwdAACbnJxAH4AOnNx
AH4ANnB0AA5Tb3V0aGVybiBTb3Rob3B0AAJzdHEAfgA6c3EAfgA2cHQAElNwYW5pc2gsIENhc3Rp
bGlhbnB0AAJlc3EAfgA6c3EAfgA2cHQACVN1bmRhbmVzZXB0AAJzdXEAfgA6c3EAfgA2cHQAB1N3
YWhpbGlwdAACc3dxAH4AOnNxAH4ANnB0AAVTd2F0aXB0AAJzc3EAfgA6c3EAfgA2cHQAB1N3ZWRp
c2hwdAACc3ZxAH4AOnNxAH4ANnB0AAdUYWdhbG9ncHQAAnRscQB+ADpzcQB+ADZwdAAIVGFoaXRp
YW5wdAACdHlxAH4AOnNxAH4ANnB0AAVUYWppa3B0AAJ0Z3EAfgA6c3EAfgA2cHQABVRhbWlscHQA
AnRhcQB+ADpzcQB+ADZwdAAFVGF0YXJwdAACdHRxAH4AOnNxAH4ANnB0AAZUZWx1Z3VwdAACdGVx
AH4AOnNxAH4ANnB0AARUaGFpcHQAAnRocQB+ADpzcQB+ADZwdAAHVGliZXRhbnB0AAJib3EAfgA6
c3EAfgA2cHQACFRpZ3JpbnlhcHQAAnRpcQB+ADpzcQB+ADZwdAAVVG9uZ2EgKFRvbmdhIElzbGFu
ZHMpcHQAAnRvcQB+ADpzcQB+ADZwdAAGVHNvbmdhcHQAAnRzcQB+ADpzcQB+ADZwdAAGVHN3YW5h
cHQAAnRucQB+ADpzcQB+ADZwdAAHVHVya2lzaHB0AAJ0cnEAfgA6c3EAfgA2cHQAB1R1cmttZW5w
dAACdGtxAH4AOnNxAH4ANnB0AANUd2lwdAACdHdxAH4AOnNxAH4ANnB0AA5VaWdodXIsIFV5Z2h1
cnB0AAJ1Z3EAfgA6c3EAfgA2cHQACVVrcmFpbmlhbnB0AAJ1a3EAfgA6c3EAfgA2cHQABFVyZHVw
dAACdXJxAH4AOnNxAH4ANnB0AAVVemJla3B0AAJ1enEAfgA6c3EAfgA2cHQABVZlbmRhcHQAAnZl
cQB+ADpzcQB+ADZwdAAKVmlldG5hbWVzZXB0AAJ2aXEAfgA6c3EAfgA2cHQACFZvbGFww7xrcHQA
AnZvcQB+ADpzcQB+ADZwdAAHV2FsbG9vbnB0AAJ3YXEAfgA6c3EAfgA2cHQABVdlbHNocHQAAmN5
cQB+ADpzcQB+ADZwdAAPV2VzdGVybiBGcmlzaWFucHQAAmZ5cQB+ADpzcQB+ADZwdAAFV29sb2Zw
dAACd29xAH4AOnNxAH4ANnB0AAVYaG9zYXB0AAJ4aHEAfgA6c3EAfgA2cHQAB1lpZGRpc2hwdAAC
eWlxAH4AOnNxAH4ANnB0AAZZb3J1YmFwdAACeW9xAH4AOnNxAH4ANnB0AA5aaHVhbmcsIENodWFu
Z3B0AAJ6YXEAfgA6c3EAfgA2cHQABFp1bHVwdAACenVxAH4AOnhxAH4AQXNxAH4AJXQAQ0luZGlj
YXRlcyBpZiBhIHJlY292ZXJ5IHBvaW50IFNFSSBtZXNzYWdlIGlzIGF0dGFjaGVkIHRvIHRoaXMg
ZnJhbWVwcHQAH2F2Y19yZWNvdmVyeV9wb2ludF9zZWlfYXR0YWNoZWRwcQB+AElzcQB+ACV0AB9J
bmRpY2F0ZXMgdGhlIGhlaWdodCBpbiBwaXhlbHMucHB0AAxpbWFnZV9oZWlnaHRwcQB+AC9zcQB+
ACV0ACNJbmRpY2F0ZXMgdGhlIHByb2ZpbGUgY29tcGF0aWJpbGl0eXBwdAAZYXZjX3Byb2ZpbGVf
Y29tcGF0aWJpbGl0eXBxAH4AL3NxAH4AJXQAJUluZGljYXRlcyB3aGV0aGVyIG92ZXJzY2FuIGlz
IHByZXNlbnRwcHQADGhhc19vdmVyc2NhbnBxAH4ASXNxAH4AJXQAQVNldCB0byB0cnVlIGlmIHRo
ZSBmcmFtZSByZXF1aXJlcyBvbiBvciBtb3JlIGZyYW1lcyB0byBiZSBkZWNvZGVkcHB0AAlkZXBl
bmRlbnRwcQB+AElzcQB+ACV0ACNUb3RhbCBsZW5ndGggb2YgdGhlIHN0cmVhbSBpZiBrbm93bnBw
dAAObGVuZ3RoT2ZTdHJlYW1wfnEAfgAtdAAETE9OR3NxAH4AJXQARUluZGljYXRlcyBpZiBhIGJ1
ZmZlcmluZyBwZXJpb2QgU0VJIG1lc3NhZ2UgaXMgYXR0YWNoZWQgdG8gdGhpcyBmcmFtZXBwdAAh
YXZjX2J1ZmZlcmluZ19wZXJpb2Rfc2VpX2F0dGFjaGVkcHEAfgBJc3EAfgAldAAzSW5kaWNhdGVz
IHRoZSBjaHJvbWEgZm9ybWF0IGFzIHNwZWNpZmllZCBpbiB0aGUgU1BTcHB0ABFhdmNfY2hyb21h
X2Zvcm1hdHBxAH4AL3NxAH4AJXQAHkluZGljYXRlcyB0aGUgd2lkdGggaW4gcGl4ZWxzLnBwdAAL
aW1hZ2Vfd2lkdGhwcQB+AC9zcQB+ACV0ABNJbmRpY2F0ZWQgdGhlIGxldmVscHB0AAlhdmNfbGV2
ZWxwcQB+AC9zcQB+ACV0ABhJbmRpY2F0ZXMgdGhlIGZyYW1lIHJhdGVwcHQACmZyYW1lX3JhdGVw
fnEAfgAtdAAIUkFUSU9OQUxzcQB+ACV0ABZJbmRpY2F0ZXMgdGhlIHByb2ZpbGUucHB0AAthdmNf
cHJvZmlsZXBxAH4AL3NyAFBjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZpbml0
aW9uLm1vZGVsLkRhdGFUeXBlRGVmaW5pdGlvbiRDb21wbGV4VHlwZQAAAAAAAAABAgACTAAIb3B0
aW9uYWxxAH4AKUwABHR5cGVxAH4ABXhxAH4AKHQAREluZm9ybWF0aW9uIGFib3V0IG9wdGlvbmFs
IHRpbWVjb2RlIGVtYmVkZGVkIGluIHRoZSBBVkMgdmlkZW8gc3RyZWFtcHB0AAxhdmNfdGltZWNv
ZGVzcgARamF2YS5sYW5nLkJvb2xlYW7NIHKA1Zz67gIAAVoABXZhbHVleHABdAAIVGltZWNvZGVz
cQB+ACV0AC5JbmRpY2F0ZXMgdGhlIFNlcXVlbmNlIFBhcmFtZXRlciBTZXQgRXh0ZW5zaW9ucHB0
AAthdmNfc3BzX2V4dHB+cQB+AC10AApCWVRFX0FSUkFZc3EAfgAldAAvSW5kaWNhdGVzIHRoZSBs
ZW5ndGggb2YgdGhlIE5BTCB1bml0IHNpemUgZmllbGRwcHQAGGF2Y19uYWxfdW5pdF9sZW5ndGhf
c2l6ZXBxAH4AL3NxAH4AJXQAI0luZGljYXRlcyB0aGUgUGljdHVyZSBQYXJhbWV0ZXIgU2V0cHB0
AAdhdmNfcHBzcHEAfgKvc3EAfgAldAAcSW5kaWNhdGVzIHRoZSBtZWRpYSBkdXJhdGlvbnBwdAAO
bWVkaWFfZHVyYXRpb25wfnEAfgAtdAAEVElNRXNxAH4AJXQAIVNldCB0byB0cnVlIGlmIEdPUCBz
aXplIGlzIGZpeGVkLnBwdAASYXZjX2ZpeGVkX2dvcF9zaXplcHEAfgBJc3EAfgAldAAwSW5kaWNh
dGVzIHRoZSBtYXhpbXVtIG51bWJlciBvZiBmcmFtZXMgaW4gYSBHT1AucHB0ABBhdmNfbWF4X2dv
cF9zaXplcHEAfgAvc3EAfgAldAAxSW5kaWNhdGVzIHRoZSBhdmVyYWdlIGJpdCByYXRlIGluIGJp
dHMgcGVyIHNlY29uZHBwdAAQYXZlcmFnZV9iaXRfcmF0ZXBxAH4AL3NxAH4AJXQAQVRydWUgb24g
dGhlIGxhc3QgZGF0YSBwYWNrZXQgb2YgdGhlIFN0cmVhbSAod2l0aCBvciB3aXRob3V0IGRhdGEp
cHB0AAtlbmRPZlN0cmVhbXBxAH4ASXNxAH4AJXQALEluZGljYXRlcyBpZiB0aGUgZnJhbWUgcmF0
ZSByZW1haW5zIGNvbnN0YW50cHB0ABNjb25zdGFudF9mcmFtZV9yYXRlcHEAfgBJc3EAfgAldABw
V2hlbiB0aGUgZnJhbWVfbGF5b3V0PXNpbmdsZV9maWVsZCwgaW5kaWNhdGVzIGlmIGZpZWxkIDEg
b3IgZmllbGQgMiAoaW4gdGhlIHByZXNlbnRhdGlvbiBvcmRlcikgaXMgdGhlIHRvcCBmaWVsZHBw
dAANZmllbGRfdG9wbmVzc3NxAH4ANAAAAAJ3BAAAAAJzcQB+ADZwdAABMXBxAH4C0HEAfgA6c3EA
fgA2cHQAATJwcQB+AtJxAH4AOnhxAH4AQXNxAH4AJXQAIkluZGljYXRlcyB0aGUgQVZDIGJpdHN0
cmVhbSBmb3JtYXRwcHQACmF2Y19mb3JtYXRzcQB+ADQAAAACdwQAAAACc3EAfgA2cHBwcHQACWNh
bm9uaWNhbHNxAH4ANnBwcHB0AANyYXd4cQB+AEFzcQB+ACV0ADRJbmRpY2F0ZXMgdGhlIHR5cGUg
b2YgcHVsbGRvd24gYXBwbGllZCB0byB0aGUgdmlkZW8ucHB0AAhwdWxsZG93bnNxAH4ANAAAAAV3
BAAAAAVzcQB+ADZwdAAETm9uZXB0AARub25lcQB+ADpzcQB+ADZwdAADMjoycHQAAzJfMnEAfgA6
c3EAfgA2cHQABzI6MjoyOjRwdAAHMl8yXzJfNHEAfgA6c3EAfgA2cHQAAzI6M3B0AAMyXzNxAH4A
OnNxAH4ANnB0AAcyOjM6MzoycHQABzJfM18zXzJxAH4AOnhxAH4AQXNxAH4AJXQAKUluZGljYXRl
cyB0aGUgdHlwZSBvZiBzbGljZSBpbiB0aGlzIGZyYW1lcHB0AA5hdmNfc2xpY2VfdHlwZXNxAH4A
NAAAAAR3BAAAAARzcQB+ADZwdAAFSURSIElwdAAFSURSX0lxAH4AOnNxAH4ANnB0AAFJcHEAfgL2
cQB+ADpzcQB+ADZwdAABUHBxAH4C+HEAfgA6c3EAfgA2cHQAAUJwcQB+AvpxAH4AOnhxAH4AQXNx
AH4AJXQANEluZGljYXRlcyB0aGUgbHVtYSBiaXQgZGVwdGggYXMgc3BlY2lmaWVkIGluIHRoZSBT
UFNwcHQAEmF2Y19sdW1hX2JpdF9kZXB0aHBxAH4AL3NxAH4AJXQAHEluZGljYXRlcyB0aGUgZGlz
cGxheSBvZmZzZXRwcHQAI2RlY29kaW5nX3RvX2NvbXBvc2l0aW9uX3RpbWVfb2Zmc2V0cHEAfgK6
c3EAfgAldAA4SW5kaWNhdGVzIGlmIHRoZSB0b3Agb3IgYm90dG9tIGZpZWxkIGlzIHRlbXBvcmFs
bHkgZmlyc3RwcHQAD2ZpZWxkX2RvbWluYW5jZXNxAH4ANAAAAAJ3BAAAAAJzcQB+ADZwdAAJVG9w
IGZpZWxkcHQACXRvcF9maWVsZHEAfgA6c3EAfgA2cHQADEJvdHRvbSBmaWVsZHB0AAxib3R0b21f
ZmllbGRxAH4AOnhxAH4AQXNxAH4AJXQAMUluZGljYXRlcyB0aGUgbWF4aW11bSBiaXQgcmF0ZSBp
biBiaXRzIHBlciBzZWNvbmRwcHQAEG1heGltdW1fYml0X3JhdGVwcQB+AC9zcQB+ACV0ADBJbmRp
Y2F0ZXMgdGhlIG1pbmltdW0gbnVtYmVyIG9mIGZyYW1lcyBpbiBhIEdPUC5wcHQAEGF2Y19taW5f
Z29wX3NpemVwcQB+AC9zcQB+ACV0ADNJbmRpY2F0ZXMgdGhlIGludGVybGFjaW5nIHR5cGUgb2Yg
dGhpcyB2aWRlbyBzYW1wbGVwcHQAEXZpZGVvX3NhbXBsZV90eXBlc3EAfgA0AAAAA3cEAAAAA3Nx
AH4ANnB0AAVGcmFtZXB0AAVmcmFtZXEAfgA6c3EAfgA2cHEAfgMGcHEAfgMHcQB+ADpzcQB+ADZw
cQB+AwlwcQB+AwpxAH4AOnhxAH4AQXNxAH4AJXQAMkluZGljYXRlcyB0aGUgc2l6ZSBvZiB0aGUg
ZGVjb2RpbmcgYnVmZmVyIGluIGJ5dGVzcHB0ABRkZWNvZGluZ19idWZmZXJfc2l6ZXBxAH4AL3Nx
AH4AJXQASVRoZSB0aW1lIHBlcnRhaW5pbmcgdG8gdGhlIGVuZCBvZiB0aGUgZGF0YSAodGltZSAr
IGR1cmF0aW9uIG9mIHRoaXMgZGF0YSlwcHQAB3RpbWVFbmRwcQB+ArpzcQB+ACV0AEJJbmRpY2F0
ZXMgdGhlIHRpbWUgdGhhdCBkZWNvZGVyIHNob3VsZCBidWZmZXIgZGF0YSBiZWZvcmUgcGxheWJh
Y2twcHQAGGF2Y19kZWNvZGluZ19idWZmZXJfdGltZXBxAH4CunNxAH4AJXQAj1NldCB0byB0cnVl
IGlmIHRoZSBzaWduYWwgaXMgaW50ZXJsYWNlZCBidXQgYm90aCBmaWVsZHMgYXJlIGNvaW5jaWRl
bnQgaW4gdGltZSAobWFwcGluZyBvZiBhIHByb2dyZXNzaXZlIHNjYW4gb24gYW4gaW50ZXJsYWNl
ZCBzaWduYWwgZW5jb2RpbmcpcHB0AA9zZWdtZW50ZWRfZnJhbWVwcQB+AElzcQB+ACV0AF1TZXQg
dG8gdHJ1ZSBpZiB0aGUgZnJhbWUgY2FuIGJlIGRyb3BwZWQgd2l0aG91dCBhbHRlcmluZyB0aGUg
ZGVjb2Rpbmcgb2YgdGhlIGZvbGxvd2luZyBmcmFtZXNwcHQACmRpc3Bvc2FibGVwcQB+AElzcQB+
ACV0AC9JbmRpY2F0ZXMgaWYgdGhlIHN0cmVhbSBoYXMgYSBjb25zdGFudCBiaXQgcmF0ZXBwdAAR
Y29uc3RhbnRfYml0X3JhdGVwcQB+AElzcQB+ACV0ADZJbmRpY2F0ZXMgdGhlIGNocm9tYSBiaXQg
ZGVwdGggYXMgc3BlY2lmaWVkIGluIHRoZSBTUFNwcHQAFGF2Y19jaHJvbWFfYml0X2RlcHRocHEA
fgAvc3EAfgAldAAzUG9zaXRpb24gb3Igb2Zmc2V0IGZyb20gdGhlIGJlZ2lubmluZyBvZiB0aGUg
c3RyZWFtcHB0ABBwb3NpdGlvbkluU3RyZWFtcHEAfgKPc3EAfgAldAA1U2V0IHRvIHRydWUgaWYg
YWxsIHRoZSBHT1BzIGluIHRoZSBzdHJlYW0gYXJlIGNsb3NlZC5wcHQAE2F2Y19jbG9zZWRfZ29w
X29ubHlwcQB+AElzcQB+ACV0ADFTZXQgdG8gdHJ1ZSBpZiB0aGUgZnJhbWUgaXMgYSByYW5kb20g
YWNjZXNzIHBvaW50cHB0ABNyYW5kb21fYWNjZXNzX3BvaW50cHEAfgBJc3EAfgAldAAiSW5kaWNh
dGVzIHRoZSBkaXNwbGF5IGFzcGVjdCByYXRpb3BwdAAUZGlzcGxheV9hc3BlY3RfcmF0aW9wcQB+
AqBzcQB+ACV0AC1UaGUgbW9zdCByZWxldmFudCB0aW1lIHBlcnRhaW5pbmcgdG8gdGhlIGRhdGFw
cHQABHRpbWVwcQB+ArpzcQB+ACV0ACRJbmRpY2F0ZXMgdGhlIFNlcXVlbmNlIFBhcmFtZXRlciBT
ZXRwcHQAB2F2Y19zcHNwcQB+Aq9zcQB+ACV0ADpJbmRpY2F0ZXMgdGhlIHNpemUgaW4gYnl0ZXMg
b2YgdGhlIE5BTCB1bml0cyBvZiB0aGlzIGZyYW1lcHEAfgKqdAASYXZjX25hbF91bml0X3NpemVz
cHEAfgAveHBzcgARamF2YS51dGlsLkhhc2hNYXAFB9rBwxZg0QMAAkYACmxvYWRGYWN0b3JJAAl0
aHJlc2hvbGR4cD8AAAAAAAAgdwgAAABAAAAAF3EAfgK5c3IAJGNhLmRpZ2l0YWxyYXBpZHMua2F5
YWsudGltZS5UaW1lSW1wbAAAAAAAAAABAgACTAAIcmF0aW9uYWx0ADFMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhL2ltcGwvUmF0aW9uYWxOdW1iZXI7TAAIdGltZUJhc2V0ACZMY2EvZGlnaXRh
bHJhcGlkcy9rYXlhay90aW1lL1RpbWVCYXNlO3hwc3IAL2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsu
ZGF0YS5pbXBsLlJhdGlvbmFsTnVtYmVyAAAAAAAAAAECAARKAAtkZW5vbWluYXRvcloACWlzUmVk
dWNlZFoAE25lZWRzQmlnRm9yTXVsdGlwbHlKAAludW1lcmF0b3J4cgAQamF2YS5sYW5nLk51bWJl
coaslR0LlOCLAgAAeHAAAAAAAABdwAAAAAAAAABD+DJzcgAoY2EuZGlnaXRhbHJhcGlkcy5rYXlh
ay50aW1lLlRpbWVCYXNlSW1wbAAAAAAAAAABAgABTAAOb2Zmc2V0UmF0aW9uYWxxAH4DR3hwc3EA
fgNKAAAAADuaygAAAAAAAAAAAAAAcQB+AsRzcgARamF2YS5sYW5nLkludGVnZXIS4qCk94GHOAIA
AUkABXZhbHVleHEAfgNLAA9CQHEAfgAzdAALcHJvZ3Jlc3NpdmVxAH4CynEAfgKqcQB+AtVxAH4C
2nEAfgBIcQB+AqpxAH4ATXQAAmVucQB+AoJzcQB+A1AAAAMwcQB+Av1zcQB+A1AAAAAIcQB+AoVz
cQB+A1AAAABAcQB+Aw1zcQB+A1AAD0JAcQB+ApZzcQB+A1AAAAABcQB+AplzcQB+A1AAAAeAcQB+
ApxzcQB+A1AAAAApcQB+Ap9zcQB+A0oAAAAAAAAD6QEAAAAAAAAAXcBxAH4CpHNxAH4DUAAAAE1x
AH4DLnEAfgNVcQB+AytxAH4CqnEAfgKzc3EAfgNQAAAABHEAfgM6c3EAfgNKAAAAAAAAABEBAAAA
AAAAAAAocQB+ArZ1cgACW0Ks8xf4BghU4AIAAHhwAAAABGjrc1JxAH4DQHVxAH4DXwAAAClnTUAp
llIA8Az3/gACAAKhAAADA+kAALuA4CAA9CQABbjf8Y4O0LFokHEAfgNDc3IAJmphdmEudXRpbC5D
b2xsZWN0aW9ucyRVbm1vZGlmaWFibGVMaXN0/A8lMbXsjhACAAFMAARsaXN0cQB+ACZ4cgAsamF2
YS51dGlsLkNvbGxlY3Rpb25zJFVubW9kaWZpYWJsZUNvbGxlY3Rpb24ZQgCAy173HgIAAUwAAWN0
ABZMamF2YS91dGlsL0NvbGxlY3Rpb247eHBzcQB+ADQAAAAAdwQAAAAAeHEAfgNmeA==</property>
                        <pinDefinition name="CompressedVideo" displayName="Compressed Video (AVC)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">1192.9999389648438,64.90400695800781</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="anchor_point">TOP_LEFT</property>
                    <property name="anchor_point_x_normalized">0.0</property>
                    <property name="anchor_point_y_normalized">0.0</property>
                    <property name="defaultInputPin">background</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="enable_scaling">FALSE</property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="maintain_par">FALSE</property>
                    <property name="manual_input">false</property>
                    <property name="opacity">1.0</property>
                    <property name="output_height">80</property>
                    <property name="output_height_normalized">1.0</property>
                    <property name="output_width">80</property>
                    <property name="output_width_normalized">1.0</property>
                    <property name="overlay_file" isNull="true"/>
                    <property name="position_x_normalized">0.1</property>
                    <property name="position_x_pixels">0</property>
                    <property name="position_y_normalized">0.1</property>
                    <property name="position_y_pixels">0</property>
                    <property name="threads">1</property>
                    <property name="use_pixels_for_position">FALSE</property>
                    <property name="use_pixels_for_scaling">FALSE</property>
                    <componentName>Graphic Overlay</componentName>
                    <componentDefinitionName>Graphic Overlay</componentDefinitionName>
                    <componentDefinitionGuid>FD517552-805D-4168-8102-B06C9048915A</componentDefinitionGuid>
                    <componentOwningPluginName>Overlay</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.Overlay</componentOwningPluginId>
                    <childComponents/>
                    <pin name="background" type="INPUT_PUSH"/>
                    <pin name="overlay" type="INPUT_PUSH"/>
                    <pin name="out" type="OUTPUT_PUSH"/>
                </component>
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
                    <property name="_graphDisplayLocation">159.99998474121094,312.0</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="always_use_directshow">false</property>
                    <property name="blackThreshold">0.10</property>
                    <property name="black_border_detection">false</property>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin" isNull="true"/>
                    <property name="enable_directshow">false</property>
                    <property name="filename">C:\Users\xpouyat\Videos\logo.png</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="inspection_max_megabytes" isNull="true"/>
                    <property name="inspection_max_seconds" isNull="true"/>
                    <property name="inspection_mode" isNull="true"/>
                    <property name="logFile" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="noiseThreshold">0.10</property>
                    <property name="probeDuration">60.0</property>
                    <property name="probeRate">0.10</property>
                    <property name="probeTimeInterval">1.0</property>
                    <property name="truncation">true</property>
                    <componentName>Media File Input Logo</componentName>
                    <componentDefinitionName>Media File Input</componentDefinitionName>
                    <componentDefinitionGuid>7cec6ecd-a477-4834-bc6f-97e34aa58bb5</componentDefinitionGuid>
                    <componentOwningPluginName>MediaInspection</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.MediaInspection</componentOwningPluginId>
                    <childComponents/>
                    <pin name="filename" type="PROPERTY">
                        <property name="_pinProperty">filename</property>
                    </pin>
                    <pin name="SampleInformation" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAAUdAAGRW5kaWFudAARVW5jb21wcmVzc2VkSW1h
Z2V0ABFJbWFnZVRyYW5zcGFyZW5jeXQABlJhc3RlcnQADkltYWdlRGltZW5zaW9udAAIRmlsZW5h
bWV0ABFTYW1wbGVJbmZvcm1hdGlvbnQADlNjYW5saW5lU3RyaWRldAAFSW1hZ2V0AAtSYXN0ZXJJ
bWFnZXQADFNhbXBsZUZvcm1hdHQACUlucHV0RmlsZXQAC0FzcGVjdFJhdGlvdAAJQ29udGFpbmVy
dAALS2F5YWtCdWZmZXJ0AAhUZW1wb3JhbHQAE0lucHV0RmlsZUJ5dGVTdHJlYW10AApCeXRlU3Ry
ZWFtdAAGU3RyZWFtdAALUGl4ZWxGb3JtYXR4c3EAfgAIdwwAAABAP0AAAAAAABtzcgBPY2EuZGln
aXRhbHJhcGlkcy5rYXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5EYXRhVHlwZURlZmlu
aXRpb24kU2ltcGxlVHlwZQAAAAAAAAABAgACTAARZW51bWVyYXRpb25WYWx1ZXN0ABBMamF2YS91
dGlsL0xpc3Q7TAAEdHlwZXQAQ0xjYS9kaWdpdGFscmFwaWRzL2theWFrL2RhdGF0eXBlcy9kZWZp
bml0aW9uL21vZGVsL1NpbXBsZVR5cGVzRW51bTt4cgBSY2EuZGlnaXRhbHJhcGlkcy5rYXlhay5k
YXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5EYXRhVHlwZURlZmluaXRpb24kS2V5RGVmaW5pdGlv
bgAAAAAAAAABAgAETAAHY29tbWVudHEAfgAFTAALZGlzcGxheU5hbWVxAH4ABUwAC211bHRpVmFs
dWVkdAATTGphdmEvbGFuZy9Cb29sZWFuO0wABG5hbWVxAH4ABXhwdABBVHJ1ZSBvbiB0aGUgbGFz
dCBkYXRhIHBhY2tldCBvZiB0aGUgU3RyZWFtICh3aXRoIG9yIHdpdGhvdXQgZGF0YSlwcHQAC2Vu
ZE9mU3RyZWFtcH5yAEFjYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5kZWZpbml0aW9u
Lm1vZGVsLlNpbXBsZVR5cGVzRW51bQAAAAAAAAAAEgAAeHIADmphdmEubGFuZy5FbnVtAAAAAAAA
AAASAAB4cHQAB0JPT0xFQU5zcgBQY2EuZGlnaXRhbHJhcGlkcy5rYXlhay5kYXRhdHlwZXMuZGVm
aW5pdGlvbi5tb2RlbC5EYXRhVHlwZURlZmluaXRpb24kQ29tcGxleFR5cGUAAAAAAAAAAQIAAkwA
CG9wdGlvbmFscQB+ACNMAAR0eXBlcQB+AAV4cQB+ACJ0AC1EZXRhaWwgaW5mb3JtYXRpb24gb24g
dGhlIHBsYW5lcyAoc2luY2UgdjEuMSlwc3IAEWphdmEubGFuZy5Cb29sZWFuzSBygNWc+u4CAAFa
AAV2YWx1ZXhwAXQAE3BsYW5hcl9pbWFnZV9wbGFuZXNwdAAXUGl4ZWxGb3JtYXRQbGFuZURldGFp
bHNzcQB+AB90AE1UaGUgbnVtYmVyIG9mIGJ5dGVzIGZyb20gdGhlIHN0YXJ0IG9mIG9uZSBzY2Fu
bGluZSwgdG8gdGhlIHN0YXJ0IG9mIHRoZSBuZXh0LnBwdAAPc2NhbmxpbmVfc3RyaWRlcH5xAH4A
J3QAB0lOVEVHRVJzcQB+AB90ABdJbmRpY2F0ZXMgYnl0ZSBvcmRlcmluZ3BwdAAGZW5kaWFuc3IA
E2phdmEudXRpbC5BcnJheUxpc3R4gdIdmcdhnQMAAUkABHNpemV4cAAAAAJ3BAAAAAJzcgA3Y2Eu
ZGlnaXRhbHJhcGlkcy5rYXlhay5wbHVnaW4ueG1sLktheWFrRW51bWVyYXRpb25WYWx1ZQAAAAAA
AAABAgAFTAALZGVzY3JpcHRpb25xAH4ABUwAC2Rpc3BsYXlOYW1lcQB+AAVMAAZoaWRkZW5xAH4A
I0wADnZhbHVlQXR0cmlidXRlcQB+AAVMAA12YWx1ZUVtYmVkZGVkcQB+AAV4cHBwcHB0AANiaWdz
cQB+ADxwcHBwdAAGbGl0dGxleH5xAH4AJ3QABlNUUklOR3NxAH4AH3QAH0luZGljYXRlcyB0aGUg
aGVpZ2h0IGluIHBpeGVscy5wcHQADGltYWdlX2hlaWdodHBxAH4ANXNxAH4AH3QAKkludGVycHJl
dCB0aGUgc2FtcGxlIGFzIHNpZ25lZCBvciB1bnNpZ25lZHBwdAANc2FtcGxlX3NpZ25lZHBxAH4A
KXNxAH4AH3QAGkluZGljYXRlcyB0aGUgY29sb3Igc3BhY2UucHB0AAtjb2xvcl9zcGFjZXNxAH4A
OgAAAAN3BAAAAANzcQB+ADxwdAADWVVWcHQAA3l1dnQAAHNxAH4APHB0AANSR0JwdAADcmdicQB+
AFBzcQB+ADxwdAAJR3JheXNjYWxlcHQACWdyYXlzY2FsZXEAfgBQeHEAfgBBc3EAfgAfdAAjVG90
YWwgbGVuZ3RoIG9mIHRoZSBzdHJlYW0gaWYga25vd25wcHQADmxlbmd0aE9mU3RyZWFtcH5xAH4A
J3QABExPTkdzcQB+AB90AElUaGUgdGltZSBwZXJ0YWluaW5nIHRvIHRoZSBlbmQgb2YgdGhlIGRh
dGEgKHRpbWUgKyBkdXJhdGlvbiBvZiB0aGlzIGRhdGEpcHB0AAd0aW1lRW5kcH5xAH4AJ3QABFRJ
TUVzcQB+AB9wcHB0ABB0cmFuc3BhcmVuY3lUeXBlc3EAfgA6AAAAA3cEAAAAA3NxAH4APHB0AA1B
bHBoYSBjaGFubmVscHQADWFscGhhX2NoYW5uZWxxAH4AUHNxAH4APHB0ABJUcmFuc3BhcmVuY3kg
Y29sb3JwdAASdHJhbnNwYXJlbmN5X2NvbG9ycQB+AFBzcQB+ADxwdAAGT3BhcXVlcHQABm9wYXF1
ZXEAfgBQeHEAfgBBc3EAfgAfdABGTGV2ZWwgb2YgcHJlY2lzaW9uIC0gY2FuIGJlIGxvd2VyIHRo
YW4gdGhlIGFjdHVhbCBudW1iZXIgb2YgdmFsaWQgYml0c3BwdAAYYWNjdXJhY3lfYml0c19wZXJf
c2FtcGxlcHEAfgA1c3EAfgAfdAAjVGhlIHNpemUgb2YgdGhlIGlucHV0IGZpbGUgaW4gYnl0ZXNw
cHQAEWlucHV0X2ZpbGVfbGVuZ3RocHEAfgBac3EAfgAfdAArSW5kaWNhdGVzIHRoZSBzYW1wbGlu
ZyBvZiB0aGUgcGl4ZWwgc2FtcGxlLnBwdAAOcGl4ZWxfc2FtcGxpbmdzcQB+ADoAAAAIdwQAAAAI
c3EAfgA8cHQAATRwcQB+AHhxAH4AUHNxAH4APHB0AAI0NHBxAH4AenEAfgBQc3EAfgA8cHQAAzQ0
NHBxAH4AfHEAfgBQc3EAfgA8cHQAAzQyMnBxAH4AfnEAfgBQc3EAfgA8cHQAAzQyMHBxAH4AgHEA
fgBQc3EAfgA8cHQAAzQxMXBxAH4AgnEAfgBQc3EAfgA8cHQABDQ0NDRwcQB+AIRxAH4AUHNxAH4A
PHB0AAQ0MjI0cHEAfgCGcQB+AFB4cQB+AEFzcQB+AB90ACpJbmRpY2F0ZXMgdGhlIHN0YW5kYXJk
IG9mIHRoZSBjb2xvciBzcGFjZS5wcHQAFGNvbG9yX3NwYWNlX3N0YW5kYXJkc3EAfgA6AAAACHcE
AAAACHNxAH4APHB0AAdSZWMgNjAxcHQABnJlYzYwMXEAfgBQc3EAfgA8cHQAB1JlYyA3MDlwdAAG
cmVjNzA5cQB+AFBzcQB+ADxwdAASUmVjIDYwMSBGdWxsIFJhbmdlcHQAEXJlYzYwMV9mdWxsX3Jh
bmdlcQB+AFBzcQB+ADxwdAASUmVjIDcwOSBGdWxsIFJhbmdlcHQAEXJlYzcwOV9mdWxsX3Jhbmdl
cQB+AFBzcQB+ADxwdAAKU3R1ZGlvIFJHQnB0AAlzdHVkaW9SR0JxAH4AUHNxAH4APHB0AAxDb21w
dXRlciBSR0JwdAALY29tcHV0ZXJSR0JxAH4AUHNxAH4APHB0ABRHcmF5c2NhbGUgSGVhZCBSYW5n
ZXB0ABRncmF5c2NhbGVfaGVhZF9yYW5nZXEAfgBQc3EAfgA8cHQAFEdyYXlzY2FsZSBGdWxsIFJh
bmdlcHQAFGdyYXlzY2FsZV9mdWxsX3JhbmdlcQB+AFB4cQB+AEFzcQB+AB90ADRUaGUgcGF0aCB0
byB0aGUgaW5wdXQgZmlsZSAtIGluY2x1ZGluZyB0aGUgZmlsZSBuYW1lcHB0AA9pbnB1dF9maWxl
X3BhdGhwcQB+AEFzcQB+AB90AERJbmRpY2F0ZXMgdGhlIG51bWJlciBvZiBieXRlcyBmb3IgZWFj
aCBwbGFuZSAoZGVwcmVjYXRlZCBhcyBvZiB2MS4xKXBxAH4AL3QAD2J5dGVzX3Blcl9wbGFuZXBx
AH4ANXNxAH4AH3QAMVRvdGFsIG51bWJlciBvZiB2YWxpZCBhbmQgaW52YWxpZCBiaXRzIHBlciBz
YW1wbGVwcHQAF3N0b3JhZ2VfYml0c19wZXJfc2FtcGxlcHEAfgA1c3EAfgAfdAAeSW5kaWNhdGVz
IHRoZSB3aWR0aCBpbiBwaXhlbHMucHB0AAtpbWFnZV93aWR0aHBxAH4ANXNxAH4AH3QAMUluZGlj
YXRlcyB0aGUgcGFja2luZyBtZXRob2Qgb2YgdGhlIHBpeGVsIHNhbXBsZS5wcHQAFnNhbXBsZV9s
YXlvdXRfc3RyYXRlZ3lzcQB+ADoAAAADdwQAAAADc3EAfgA8cHQABlBhY2tlZHB0AAZwYWNrZWRx
AH4AUHNxAH4APHB0AAZQbGFuYXJwdAAGcGxhbmFycQB+AFBzcQB+ADxwdAAHUGFsZXR0ZXB0AAdw
YWxldHRlcQB+AFB4cQB+AEFzcQB+AB90ADNEaXJlY3Rpb24gb2YgdGhlIGltYWdlIHJhc3RlciAo
dG9wX2Rvd24sIGJvdHRvbV91cClwcHQAEnJhc3Rlcl9vcmllbnRhdGlvbnNxAH4AOgAAAAJ3BAAA
AAJzcQB+ADxwdAAIVG9wIGRvd25wdAAIdG9wX2Rvd25xAH4AUHNxAH4APHB0AAlCb3R0b20gdXBw
dAAJYm90dG9tX3VwcQB+AFB4cQB+AEFzcQB+AB90ADNQb3NpdGlvbiBvciBvZmZzZXQgZnJvbSB0
aGUgYmVnaW5uaW5nIG9mIHRoZSBzdHJlYW1wcHQAEHBvc2l0aW9uSW5TdHJlYW1wcQB+AFpzcQB+
AB90AF1JbmRpY2F0ZXMgdGhhdCB0aGUgYWxwaGEgY2hhbm5lbCBoYXMgYmVlbiBwcmVtdWx0aXBs
aWVkIGludG8gdGhlIG90aGVyIGNoYW5uZWxzIChzaW5jZSB2MS4xMClwcHQAHmFscGhhX2NoYW5u
ZWxfaXNfcHJlbXVsdGlwbGllZHBxAH4AKXNxAH4AH3QAR0luZGljYXRlcyB0aGUgbnVtYmVyIG9m
IGJ5dGVzIGZvciBlYWNoIHNjYW5saW5lIChkZXByZWNhdGVkIGFzIG9mIHYxLjEpcHEAfgAvdAAS
Ynl0ZXNfcGVyX3NjYW5saW5lcHEAfgA1c3EAfgAfdAAiSW5kaWNhdGVzIHRoZSBkaXNwbGF5IGFz
cGVjdCByYXRpb3BwdAAUZGlzcGxheV9hc3BlY3RfcmF0aW9wfnEAfgAndAAIUkFUSU9OQUxzcQB+
AB90AB9OdW1iZXIgb2YgdmFsaWQgYml0cyBwZXIgc2FtcGxlcHB0AA9iaXRzX3Blcl9zYW1wbGVw
cQB+ADVzcQB+AB90AC1UaGUgbW9zdCByZWxldmFudCB0aW1lIHBlcnRhaW5pbmcgdG8gdGhlIGRh
dGFwcHQABHRpbWVwcQB+AF9zcQB+AB90ADtJbmRpY2F0ZXMgdGhlIGxheW91dCBhbmQgZm9ybWF0
IG9mIHBpeGVsIHNhbXBsZXMgaW4gbWVtb3J5LnBwdAAVc2FtcGxlX2xheW91dF9kZXRhaWxzc3EA
fgA6AAAAE3cEAAAAE3NxAH4APHB0AANCR1JwdAADYmdycQB+AFBzcQB+ADxwdAAEQkdSQXB0AARi
Z3JhcQB+AFBzcQB+ADxwcQB+AFJwcQB+AFNxAH4AUHNxAH4APHB0AARSR0JBcHQABHJnYmFxAH4A
UHNxAH4APHB0AARBUkdCcHQABGFyZ2JxAH4AUHNxAH4APHB0AARVWVZZcHQABHV5dnlxAH4AUHNx
AH4APHB0AARZVVlWcHQABHl1eXZxAH4AUHNxAH4APHB0AAZVWVZZMTBwdAAGdXl2eTEwcQB+AFBz
cQB+ADxwdAAGWVVZVjEwcHQABnl1eXYxMHEAfgBQc3EAfgA8cHQABllVVjIxMHB0AAZ5dXYyMTBx
AH4AUHNxAH4APHB0AAZZVVY0MTBwdAAGeXV2NDEwcQB+AFBzcQB+ADxwcQB+AE5wcQB+AE9xAH4A
UHNxAH4APHB0AARZVVZBcHQABHl1dmFxAH4AUHNxAH4APHB0AANZVlVwdAADeXZ1cQB+AFBzcQB+
ADxwcQB+AFVwcQB+AFZxAH4AUHNxAH4APHB0AA9HcmF5c2NhbGUgYWxwaGFwdAAPZ3JheXNjYWxl
X2FscGhhcQB+AFBzcQB+ADxwdAAQTmV4aW8gMTAtYml0IFlVVnB0AAxuZXhpb195dXl2MTBxAH4A
UHNxAH4APHB0ABBOZXhpbyAxMi1iaXQgWVVWcHQADG5leGlvX3l1eXYxMnEAfgBQc3EAfgA8cHQA
BFIxMGtwdAAEcjEwa3EAfgBQeHEAfgBBeHBzcgARamF2YS51dGlsLkhhc2hNYXAFB9rBwxZg0QMA
AkYACmxvYWRGYWN0b3JJAAl0aHJlc2hvbGR4cD8AAAAAAAAgdwgAAABAAAAAFHEAfgAwc3IAJmph
dmEudXRpbC5Db2xsZWN0aW9ucyRVbm1vZGlmaWFibGVMaXN0/A8lMbXsjhACAAFMAARsaXN0cQB+
ACB4cgAsamF2YS51dGlsLkNvbGxlY3Rpb25zJFVubW9kaWZpYWJsZUNvbGxlY3Rpb24ZQgCAy173
HgIAAUwAAWN0ABZMamF2YS91dGlsL0NvbGxlY3Rpb247eHBzcQB+ADoAAAAAdwQAAAAAeHEAfgEX
cQB+ACZxAH4AL3EAfgA5cQB+AEBxAH4ARXNyABFqYXZhLmxhbmcuSW50ZWdlchLioKT3gYc4AgAB
SQAFdmFsdWV4cgAQamF2YS5sYW5nLk51bWJlcoaslR0LlOCLAgAAeHAAAABdcQB+AEhzcQB+AC4A
cQB+AEtxAH4AU3EAfgBicQB+AGZxAH4Ab3NxAH4BGAAAAAhxAH4AcnNyAA5qYXZhLmxhbmcuTG9u
ZzuL5JDMjyPfAgABSgAFdmFsdWV4cQB+ARkAAAAAAAAPh3EAfgB1cQB+AIRxAH4AiXEAfgCccQB+
AKhzcQB+ARNzcQB+ADoAAAAAdwQAAAAAeHEAfgEgcQB+AKV0ACBDOlxVc2Vyc1x4cG91eWF0XFZp
ZGVvc1xsb2dvLnBuZ3EAfgCucQB+ARpxAH4Aq3NxAH4BGAAAAABxAH4AsXEAfgC1cQB+AL5xAH4A
wnEAfgDOc3EAfgETc3EAfgA6AAAAAHcEAAAAAHhxAH4BJHEAfgDWcQB+ARxxAH4A3HEAfgDneA==</property>
                        <pinDefinition name="SampleInformation" displayName="Uncompressed Image" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                    <pin name="PNGImage" type="OUTPUT_IO">
                        <property name="pinProtoDataType" marshallerKey="Serializable">rO0ABXNyAC1jYS5kaWdpdGFscmFwaWRzLmtheWFrLmRhdGF0eXBlcy5CYXNlRGF0YVR5cGUAAAAA
AAAAAQIAA1oAB211dGFibGVMABJkYXRhVHlwZURlZmluaXRpb250AEZMY2EvZGlnaXRhbHJhcGlk
cy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9EYXRhVHlwZURlZmluaXRpb247TAAD
bWFwdAAPTGphdmEvdXRpbC9NYXA7eHAAc3IARGNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5
cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uAAAAAAAAAAECAARMAAdjb21t
ZW50dAASTGphdmEvbGFuZy9TdHJpbmc7TAAOaW5oZXJpdGFuY2VTZXR0AA9MamF2YS91dGlsL1Nl
dDtMAA5rZXlEZWZpbml0aW9uc3EAfgAGTAAEbmFtZXEAfgAFeHBwc3IAEWphdmEudXRpbC5IYXNo
U2V0ukSFlZa4tzQDAAB4cHcMAAAAID9AAAAAAAAWdAAOUE5HSW1hZ2VTdHJlYW10AAZFbmRpYW50
ABFJbWFnZVRyYW5zcGFyZW5jeXQABlJhc3RlcnQADkltYWdlRGltZW5zaW9udAAIRmlsZW5hbWV0
ABFTYW1wbGVJbmZvcm1hdGlvbnQADlNjYW5saW5lU3RyaWRldAAFSW1hZ2V0AAtSYXN0ZXJJbWFn
ZXQADFNhbXBsZUZvcm1hdHQACUlucHV0RmlsZXQACFBOR0ltYWdldAALQXNwZWN0UmF0aW90AAlD
b250YWluZXJ0AAtLYXlha0J1ZmZlcnQACFRlbXBvcmFsdAATSW5wdXRGaWxlQnl0ZVN0cmVhbXQA
CkJ5dGVTdHJlYW10AA5QTkdJbWFnZVNhbXBsZXQABlN0cmVhbXQAC1BpeGVsRm9ybWF0eHNxAH4A
CHcMAAAAQD9AAAAAAAAbc3IAT2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmlu
aXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9uJFNpbXBsZVR5cGUAAAAAAAAAAQIAAkwAEWVu
dW1lcmF0aW9uVmFsdWVzdAAQTGphdmEvdXRpbC9MaXN0O0wABHR5cGV0AENMY2EvZGlnaXRhbHJh
cGlkcy9rYXlhay9kYXRhdHlwZXMvZGVmaW5pdGlvbi9tb2RlbC9TaW1wbGVUeXBlc0VudW07eHIA
UmNhLmRpZ2l0YWxyYXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5
cGVEZWZpbml0aW9uJEtleURlZmluaXRpb24AAAAAAAAAAQIABEwAB2NvbW1lbnRxAH4ABUwAC2Rp
c3BsYXlOYW1lcQB+AAVMAAttdWx0aVZhbHVlZHQAE0xqYXZhL2xhbmcvQm9vbGVhbjtMAARuYW1l
cQB+AAV4cHQAQVRydWUgb24gdGhlIGxhc3QgZGF0YSBwYWNrZXQgb2YgdGhlIFN0cmVhbSAod2l0
aCBvciB3aXRob3V0IGRhdGEpcHB0AAtlbmRPZlN0cmVhbXB+cgBBY2EuZGlnaXRhbHJhcGlkcy5r
YXlhay5kYXRhdHlwZXMuZGVmaW5pdGlvbi5tb2RlbC5TaW1wbGVUeXBlc0VudW0AAAAAAAAAABIA
AHhyAA5qYXZhLmxhbmcuRW51bQAAAAAAAAAAEgAAeHB0AAdCT09MRUFOc3IAUGNhLmRpZ2l0YWxy
YXBpZHMua2F5YWsuZGF0YXR5cGVzLmRlZmluaXRpb24ubW9kZWwuRGF0YVR5cGVEZWZpbml0aW9u
JENvbXBsZXhUeXBlAAAAAAAAAAECAAJMAAhvcHRpb25hbHEAfgAlTAAEdHlwZXEAfgAFeHEAfgAk
dAAtRGV0YWlsIGluZm9ybWF0aW9uIG9uIHRoZSBwbGFuZXMgKHNpbmNlIHYxLjEpcHNyABFqYXZh
LmxhbmcuQm9vbGVhbs0gcoDVnPruAgABWgAFdmFsdWV4cAF0ABNwbGFuYXJfaW1hZ2VfcGxhbmVz
cHQAF1BpeGVsRm9ybWF0UGxhbmVEZXRhaWxzc3EAfgAhdABNVGhlIG51bWJlciBvZiBieXRlcyBm
cm9tIHRoZSBzdGFydCBvZiBvbmUgc2NhbmxpbmUsIHRvIHRoZSBzdGFydCBvZiB0aGUgbmV4dC5w
cHQAD3NjYW5saW5lX3N0cmlkZXB+cQB+ACl0AAdJTlRFR0VSc3EAfgAhdAAXSW5kaWNhdGVzIGJ5
dGUgb3JkZXJpbmdwcHQABmVuZGlhbnNyABNqYXZhLnV0aWwuQXJyYXlMaXN0eIHSHZnHYZ0DAAFJ
AARzaXpleHAAAAACdwQAAAACc3IAN2NhLmRpZ2l0YWxyYXBpZHMua2F5YWsucGx1Z2luLnhtbC5L
YXlha0VudW1lcmF0aW9uVmFsdWUAAAAAAAAAAQIABUwAC2Rlc2NyaXB0aW9ucQB+AAVMAAtkaXNw
bGF5TmFtZXEAfgAFTAAGaGlkZGVucQB+ACVMAA52YWx1ZUF0dHJpYnV0ZXEAfgAFTAANdmFsdWVF
bWJlZGRlZHEAfgAFeHBwcHBwdAADYmlnc3EAfgA+cHBwcHQABmxpdHRsZXh+cQB+ACl0AAZTVFJJ
TkdzcQB+ACF0AB9JbmRpY2F0ZXMgdGhlIGhlaWdodCBpbiBwaXhlbHMucHB0AAxpbWFnZV9oZWln
aHRwcQB+ADdzcQB+ACF0ACpJbnRlcnByZXQgdGhlIHNhbXBsZSBhcyBzaWduZWQgb3IgdW5zaWdu
ZWRwcHQADXNhbXBsZV9zaWduZWRwcQB+ACtzcQB+ACF0ABpJbmRpY2F0ZXMgdGhlIGNvbG9yIHNw
YWNlLnBwdAALY29sb3Jfc3BhY2VzcQB+ADwAAAADdwQAAAADc3EAfgA+cHQAA1lVVnB0AAN5dXZ0
AABzcQB+AD5wdAADUkdCcHQAA3JnYnEAfgBSc3EAfgA+cHQACUdyYXlzY2FsZXB0AAlncmF5c2Nh
bGVxAH4AUnhxAH4AQ3NxAH4AIXQAI1RvdGFsIGxlbmd0aCBvZiB0aGUgc3RyZWFtIGlmIGtub3du
cHB0AA5sZW5ndGhPZlN0cmVhbXB+cQB+ACl0AARMT05Hc3EAfgAhdABJVGhlIHRpbWUgcGVydGFp
bmluZyB0byB0aGUgZW5kIG9mIHRoZSBkYXRhICh0aW1lICsgZHVyYXRpb24gb2YgdGhpcyBkYXRh
KXBwdAAHdGltZUVuZHB+cQB+ACl0AARUSU1Fc3EAfgAhcHBwdAAQdHJhbnNwYXJlbmN5VHlwZXNx
AH4APAAAAAN3BAAAAANzcQB+AD5wdAANQWxwaGEgY2hhbm5lbHB0AA1hbHBoYV9jaGFubmVscQB+
AFJzcQB+AD5wdAASVHJhbnNwYXJlbmN5IGNvbG9ycHQAEnRyYW5zcGFyZW5jeV9jb2xvcnEAfgBS
c3EAfgA+cHQABk9wYXF1ZXB0AAZvcGFxdWVxAH4AUnhxAH4AQ3NxAH4AIXQARkxldmVsIG9mIHBy
ZWNpc2lvbiAtIGNhbiBiZSBsb3dlciB0aGFuIHRoZSBhY3R1YWwgbnVtYmVyIG9mIHZhbGlkIGJp
dHNwcHQAGGFjY3VyYWN5X2JpdHNfcGVyX3NhbXBsZXBxAH4AN3NxAH4AIXQAI1RoZSBzaXplIG9m
IHRoZSBpbnB1dCBmaWxlIGluIGJ5dGVzcHB0ABFpbnB1dF9maWxlX2xlbmd0aHBxAH4AXHNxAH4A
IXQAK0luZGljYXRlcyB0aGUgc2FtcGxpbmcgb2YgdGhlIHBpeGVsIHNhbXBsZS5wcHQADnBpeGVs
X3NhbXBsaW5nc3EAfgA8AAAACHcEAAAACHNxAH4APnB0AAE0cHEAfgB6cQB+AFJzcQB+AD5wdAAC
NDRwcQB+AHxxAH4AUnNxAH4APnB0AAM0NDRwcQB+AH5xAH4AUnNxAH4APnB0AAM0MjJwcQB+AIBx
AH4AUnNxAH4APnB0AAM0MjBwcQB+AIJxAH4AUnNxAH4APnB0AAM0MTFwcQB+AIRxAH4AUnNxAH4A
PnB0AAQ0NDQ0cHEAfgCGcQB+AFJzcQB+AD5wdAAENDIyNHBxAH4AiHEAfgBSeHEAfgBDc3EAfgAh
dAAqSW5kaWNhdGVzIHRoZSBzdGFuZGFyZCBvZiB0aGUgY29sb3Igc3BhY2UucHB0ABRjb2xvcl9z
cGFjZV9zdGFuZGFyZHNxAH4APAAAAAh3BAAAAAhzcQB+AD5wdAAHUmVjIDYwMXB0AAZyZWM2MDFx
AH4AUnNxAH4APnB0AAdSZWMgNzA5cHQABnJlYzcwOXEAfgBSc3EAfgA+cHQAElJlYyA2MDEgRnVs
bCBSYW5nZXB0ABFyZWM2MDFfZnVsbF9yYW5nZXEAfgBSc3EAfgA+cHQAElJlYyA3MDkgRnVsbCBS
YW5nZXB0ABFyZWM3MDlfZnVsbF9yYW5nZXEAfgBSc3EAfgA+cHQAClN0dWRpbyBSR0JwdAAJc3R1
ZGlvUkdCcQB+AFJzcQB+AD5wdAAMQ29tcHV0ZXIgUkdCcHQAC2NvbXB1dGVyUkdCcQB+AFJzcQB+
AD5wdAAUR3JheXNjYWxlIEhlYWQgUmFuZ2VwdAAUZ3JheXNjYWxlX2hlYWRfcmFuZ2VxAH4AUnNx
AH4APnB0ABRHcmF5c2NhbGUgRnVsbCBSYW5nZXB0ABRncmF5c2NhbGVfZnVsbF9yYW5nZXEAfgBS
eHEAfgBDc3EAfgAhdAA0VGhlIHBhdGggdG8gdGhlIGlucHV0IGZpbGUgLSBpbmNsdWRpbmcgdGhl
IGZpbGUgbmFtZXBwdAAPaW5wdXRfZmlsZV9wYXRocHEAfgBDc3EAfgAhdABESW5kaWNhdGVzIHRo
ZSBudW1iZXIgb2YgYnl0ZXMgZm9yIGVhY2ggcGxhbmUgKGRlcHJlY2F0ZWQgYXMgb2YgdjEuMSlw
cQB+ADF0AA9ieXRlc19wZXJfcGxhbmVwcQB+ADdzcQB+ACF0ADFUb3RhbCBudW1iZXIgb2YgdmFs
aWQgYW5kIGludmFsaWQgYml0cyBwZXIgc2FtcGxlcHB0ABdzdG9yYWdlX2JpdHNfcGVyX3NhbXBs
ZXBxAH4AN3NxAH4AIXQAHkluZGljYXRlcyB0aGUgd2lkdGggaW4gcGl4ZWxzLnBwdAALaW1hZ2Vf
d2lkdGhwcQB+ADdzcQB+ACF0ADFJbmRpY2F0ZXMgdGhlIHBhY2tpbmcgbWV0aG9kIG9mIHRoZSBw
aXhlbCBzYW1wbGUucHB0ABZzYW1wbGVfbGF5b3V0X3N0cmF0ZWd5c3EAfgA8AAAAA3cEAAAAA3Nx
AH4APnB0AAZQYWNrZWRwdAAGcGFja2VkcQB+AFJzcQB+AD5wdAAGUGxhbmFycHQABnBsYW5hcnEA
fgBSc3EAfgA+cHQAB1BhbGV0dGVwdAAHcGFsZXR0ZXEAfgBSeHEAfgBDc3EAfgAhdAAzRGlyZWN0
aW9uIG9mIHRoZSBpbWFnZSByYXN0ZXIgKHRvcF9kb3duLCBib3R0b21fdXApcHB0ABJyYXN0ZXJf
b3JpZW50YXRpb25zcQB+ADwAAAACdwQAAAACc3EAfgA+cHQACFRvcCBkb3ducHQACHRvcF9kb3du
cQB+AFJzcQB+AD5wdAAJQm90dG9tIHVwcHQACWJvdHRvbV91cHEAfgBSeHEAfgBDc3EAfgAhdAAz
UG9zaXRpb24gb3Igb2Zmc2V0IGZyb20gdGhlIGJlZ2lubmluZyBvZiB0aGUgc3RyZWFtcHB0ABBw
b3NpdGlvbkluU3RyZWFtcHEAfgBcc3EAfgAhdABdSW5kaWNhdGVzIHRoYXQgdGhlIGFscGhhIGNo
YW5uZWwgaGFzIGJlZW4gcHJlbXVsdGlwbGllZCBpbnRvIHRoZSBvdGhlciBjaGFubmVscyAoc2lu
Y2UgdjEuMTApcHB0AB5hbHBoYV9jaGFubmVsX2lzX3ByZW11bHRpcGxpZWRwcQB+ACtzcQB+ACF0
AEdJbmRpY2F0ZXMgdGhlIG51bWJlciBvZiBieXRlcyBmb3IgZWFjaCBzY2FubGluZSAoZGVwcmVj
YXRlZCBhcyBvZiB2MS4xKXBxAH4AMXQAEmJ5dGVzX3Blcl9zY2FubGluZXBxAH4AN3NxAH4AIXQA
IkluZGljYXRlcyB0aGUgZGlzcGxheSBhc3BlY3QgcmF0aW9wcHQAFGRpc3BsYXlfYXNwZWN0X3Jh
dGlvcH5xAH4AKXQACFJBVElPTkFMc3EAfgAhdAAfTnVtYmVyIG9mIHZhbGlkIGJpdHMgcGVyIHNh
bXBsZXBwdAAPYml0c19wZXJfc2FtcGxlcHEAfgA3c3EAfgAhdAAtVGhlIG1vc3QgcmVsZXZhbnQg
dGltZSBwZXJ0YWluaW5nIHRvIHRoZSBkYXRhcHB0AAR0aW1lcHEAfgBhc3EAfgAhdAA7SW5kaWNh
dGVzIHRoZSBsYXlvdXQgYW5kIGZvcm1hdCBvZiBwaXhlbCBzYW1wbGVzIGluIG1lbW9yeS5wcHQA
FXNhbXBsZV9sYXlvdXRfZGV0YWlsc3NxAH4APAAAABN3BAAAABNzcQB+AD5wdAADQkdScHQAA2Jn
cnEAfgBSc3EAfgA+cHQABEJHUkFwdAAEYmdyYXEAfgBSc3EAfgA+cHEAfgBUcHEAfgBVcQB+AFJz
cQB+AD5wdAAEUkdCQXB0AARyZ2JhcQB+AFJzcQB+AD5wdAAEQVJHQnB0AARhcmdicQB+AFJzcQB+
AD5wdAAEVVlWWXB0AAR1eXZ5cQB+AFJzcQB+AD5wdAAEWVVZVnB0AAR5dXl2cQB+AFJzcQB+AD5w
dAAGVVlWWTEwcHQABnV5dnkxMHEAfgBSc3EAfgA+cHQABllVWVYxMHB0AAZ5dXl2MTBxAH4AUnNx
AH4APnB0AAZZVVYyMTBwdAAGeXV2MjEwcQB+AFJzcQB+AD5wdAAGWVVWNDEwcHQABnl1djQxMHEA
fgBSc3EAfgA+cHEAfgBQcHEAfgBRcQB+AFJzcQB+AD5wdAAEWVVWQXB0AAR5dXZhcQB+AFJzcQB+
AD5wdAADWVZVcHQAA3l2dXEAfgBSc3EAfgA+cHEAfgBXcHEAfgBYcQB+AFJzcQB+AD5wdAAPR3Jh
eXNjYWxlIGFscGhhcHQAD2dyYXlzY2FsZV9hbHBoYXEAfgBSc3EAfgA+cHQAEE5leGlvIDEwLWJp
dCBZVVZwdAAMbmV4aW9feXV5djEwcQB+AFJzcQB+AD5wdAAQTmV4aW8gMTItYml0IFlVVnB0AAxu
ZXhpb195dXl2MTJxAH4AUnNxAH4APnB0AARSMTBrcHQABHIxMGtxAH4AUnhxAH4AQ3hwc3IAEWph
dmEudXRpbC5IYXNoTWFwBQfawcMWYNEDAAJGAApsb2FkRmFjdG9ySQAJdGhyZXNob2xkeHA/AAAA
AAAAIHcIAAAAQAAAABRxAH4AMnNyACZqYXZhLnV0aWwuQ29sbGVjdGlvbnMkVW5tb2RpZmlhYmxl
TGlzdPwPJTG17I4QAgABTAAEbGlzdHEAfgAieHIALGphdmEudXRpbC5Db2xsZWN0aW9ucyRVbm1v
ZGlmaWFibGVDb2xsZWN0aW9uGUIAgMte9x4CAAFMAAFjdAAWTGphdmEvdXRpbC9Db2xsZWN0aW9u
O3hwc3EAfgA8AAAAAHcEAAAAAHhxAH4BGXEAfgAocQB+ADFxAH4AO3EAfgBAcQB+AEdzcgARamF2
YS5sYW5nLkludGVnZXIS4qCk94GHOAIAAUkABXZhbHVleHIAEGphdmEubGFuZy5OdW1iZXKGrJUd
C5TgiwIAAHhwAAAAXXEAfgBNcQB+AFVxAH4AW3NyAA5qYXZhLmxhbmcuTG9uZzuL5JDMjyPfAgAB
SgAFdmFsdWV4cQB+ARsAAAAAAAAPh3EAfgBkcQB+AGhxAH4AcXNxAH4BGgAAAAhxAH4AdHNxAH4B
HQAAAAAAAA+HcQB+AHdxAH4AhnEAfgCLcQB+AJ5xAH4AqnNxAH4BFXNxAH4APAAAAAB3BAAAAAB4
cQB+ASJxAH4Ap3QAIEM6XFVzZXJzXHhwb3V5YXRcVmlkZW9zXGxvZ28ucG5ncQB+ALBxAH4BHHEA
fgCzcQB+ALdxAH4AwHEAfgDEcQB+AMpzcQB+AR0AAAAAAAAAAHEAfgDQc3EAfgEVc3EAfgA8AAAA
AHcEAAAAAHhxAH4BJnEAfgDYcQB+AR9xAH4A3nEAfgDpeA==</property>
                        <pinDefinition name="PNGImage" displayName="Image (PNG)" type="OUTPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <property name="5_1_to_stereo" isNull="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1600.0,322.00001525878906</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
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
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">2371.0,241.0</property>
                    <property name="_graphMinDisplaySize">500.0,400.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
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
    &lt;scriptString&gt;"${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_withoverlay.mp4"&lt;/scriptString&gt;
    &lt;string&gt;
        &lt;constant value="${ROOT_outputWriteDirectory}\\${ROOT_sourceFileBaseName}_withoverlay.mp4"/&gt;
    &lt;/string&gt;
&lt;/expressionInfo&gt;
</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_input">true</property>
                    <componentName>File Output</componentName>
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
                    <property name="_graphDisplayLocation">832.0,31.000011444091797</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="output.data_layout.layout_type">configurable_planar</property>
                    <property name="output.data_layout.package_type">YUYV</property>
                    <property name="output.data_layout.packaged_bit_depth">8</property>
                    <property name="output.data_layout.packaged_scanline_alignment">1</property>
                    <property name="output.data_layout.planar_alpha_channel" isNull="true"/>
                    <property name="output.data_layout.planar_bit_depth">8</property>
                    <property name="output.data_layout.planar_chroma_sampling">422</property>
                    <property name="output.data_layout.planar_scanline_alignment">32</property>
                    <property name="output.raster_orientation" isNull="true"/>
                    <property name="output.video_format.color_space">yuv709</property>
                    <property name="output.video_format.dar" isNull="true"/>
                    <property name="output.video_format.frame_rate" isNull="true"/>
                    <property name="output.video_format.height" isNull="true"/>
                    <property name="output.video_format.scan_type">Progressive</property>
                    <property name="output.video_format.width" isNull="true"/>
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
                    <property name="processing.de_interlacing.latency">40</property>
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
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">550.0,29.000011444091797</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="accuracy_bits_per_sample" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="average_bit_rate" isNull="true"/>
                    <property name="bits_per_sample" isNull="true"/>
                    <property name="color_space_standard">rec709</property>
                    <property name="custom_aspect_ratio">16/9</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="display_aspect_ratio" isNull="true"/>
                    <property name="display_aspect_ratio_shadow" isNull="true"/>
                    <property name="field_dominance" isNull="true"/>
                    <property name="frame_layout" isNull="true"/>
                    <property name="frame_rate" isNull="true"/>
                    <property name="frame_rate_shadow" isNull="true"/>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="language_code" isNull="true"/>
                    <property name="language_code_shadow" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="maximum_bit_rate" isNull="true"/>
                    <property name="out_endian" isNull="true"/>
                    <property name="override_input">FALSE</property>
                    <property name="pulldown_mode" isNull="true"/>
                    <property name="rate_control" isNull="true"/>
                    <property name="sample_signed" isNull="true"/>
                    <property name="sd_no_overscan" isNull="true"/>
                    <property name="sd_no_overscan_shadow" isNull="true"/>
                    <property name="segmented_frame" isNull="true"/>
                    <property name="storage_bits_per_sample" isNull="true"/>
                    <componentName>Video Data Type Updater</componentName>
                    <componentDefinitionName>Video Data Type Updater</componentDefinitionName>
                    <componentDefinitionGuid>D7576695-6BCB-410F-BB86-734E5F526924</componentDefinitionGuid>
                    <componentOwningPluginName>VideoFormatUtilities</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.VideoFormatUtilities</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_IO"/>
                    <pin name="out" type="OUTPUT_IO"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <property name="AAC" isNull="true"/>
                    <property name="Custom" isNull="true"/>
                    <property name="DTS" isNull="true"/>
                    <property name="Dolby_Digital" isNull="true"/>
                    <property name="Dolby_E" isNull="true"/>
                    <property name="WAVE" isNull="true"/>
                    <property name="_graphDisplayContents" isNull="true"/>
                    <property name="_graphDisplayLocation">770.9500122070312,1282.790026664734</property>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="channel1_group"></property>
                    <property name="channel1_speaker">L_LEFT</property>
                    <property name="channel2_group"></property>
                    <property name="channel2_speaker">R_RIGHT</property>
                    <property name="channel3_group"></property>
                    <property name="channel3_speaker"></property>
                    <property name="channel4_group"></property>
                    <property name="channel4_speaker"></property>
                    <property name="channel5_group"></property>
                    <property name="channel5_speaker"></property>
                    <property name="channel6_group"></property>
                    <property name="channel6_speaker"></property>
                    <property name="channel7_group"></property>
                    <property name="channel7_speaker"></property>
                    <property name="channel8_group"></property>
                    <property name="channel8_speaker"></property>
                    <property name="channel_position_preset">L_R</property>
                    <property name="defaultInputPin">in</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="encoder_preset_filter"></property>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="manual_preset_mode">false</property>
                    <property name="override_mode">true</property>
                    <componentName>Speaker Position Assigner</componentName>
                    <componentDefinitionName>Speaker Position Assigner</componentDefinitionName>
                    <componentDefinitionGuid>AB851938-A3DA-4062-9F4A-FB8AF260D887</componentDefinitionGuid>
                    <componentOwningPluginName>AudioFormatUtilities</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.AudioFormatUtilities</componentOwningPluginId>
                    <childComponents/>
                    <pin name="in" type="INPUT_PUSH"/>
                    <pin name="out" type="OUTPUT_PUSH"/>
                </component>
                <component>
                    <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphDisplayContents" group="System" dynamic="true"/>
                    <propertyDefinition hidden="true" name="_graphMinDisplaySize" group="System" dynamic="true"/>
                    <property name="AlternateAudioTracks">true</property>
                    <property name="AlternateSubtitleTracks">true</property>
                    <property name="ChunkDuration">1000</property>
                    <property name="ChunkMode">GOP count or duration</property>
                    <property name="FragmentDuration">3000</property>
                    <property name="Fragmentation">false</property>
                    <property name="NbGopsPerChunk">1</property>
                    <property name="ProgressiveDownload">false</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">2075.8687744140625,226.59271240234375</property>
                    <property name="_graphMinDisplaySize" isNull="true"/>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="defaultInputPin" isNull="true"/>
                    <property name="defaultOutputPin">mp4</property>
                    <property name="drc_iso_file_format">MPEG4</property>
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <componentName>ISO MPEG-4 Multiplexer</componentName>
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
                    <pin name="Track 3" type="INPUT_IO">
                        <pinDefinition name="Track 3" type="INPUT_IO" dynamic="true"/>
                    </pin>
                </component>
                <component>
                    <propertyDefinition displayName="Target Video Format" name="video_format" group="General" dynamic="true">
                        <initialValue>Same as Input(1tc/frame)</initialValue>
                        <valueType type="STRING">
                            <valueRestriction strictEnum="true">
                                <enumerationValue val="23.98i(1tc/frame)" displayName="23.98 fps (inserted per frame)"></enumerationValue>
                                <enumerationValue val="23.98i(1tc/field)" displayName="23.98 fps (inserted per field)"></enumerationValue>
                                <enumerationValue val="24i(1tc/frame)" displayName="24 fps (inserted per frame)"></enumerationValue>
                                <enumerationValue val="24i(1tc/field)" displayName="24 fps (inserted per field)"></enumerationValue>
                                <enumerationValue val="25i(1tc/frame)" displayName="25 fps (inserted per frame)"></enumerationValue>
                                <enumerationValue val="25i(1tc/field)" displayName="25 fps (inserted per field)"></enumerationValue>
                                <enumerationValue val="29.97i(1tc/frame)" displayName="29.97 fps (inserted per frame)"></enumerationValue>
                                <enumerationValue val="29.97i(1tc/field)" displayName="29.97 fps (inserted per field)"></enumerationValue>
                                <enumerationValue val="30i(1tc/frame)" displayName="30 fps (inserted per frame)"></enumerationValue>
                                <enumerationValue val="30i(1tc/field)" displayName="30 fps (inserted per field)"></enumerationValue>
                                <enumerationValue val="50p" displayName="50 fps (inserted per frame)"></enumerationValue>
                                <enumerationValue val="59.94p" displayName="59.94 fps (inserted per frame)"></enumerationValue>
                                <enumerationValue val="60p" displayName="60 fps (inserted per frame)"></enumerationValue>
                                <enumerationValue val="Same as Input(1tc/frame)" displayName="Input (inserted per frame)"></enumerationValue>
                                <enumerationValue val="Same as Input(1tc/field)" displayName="Input (inserted per field)"></enumerationValue>
                            </valueRestriction>
                        </valueType>
                    </propertyDefinition>
                    <propertyDefinition displayName="Timecode Setting" name="start_timecode" group="General" dynamic="true">
                        <initialValue>00:00:00:00/30</initialValue>
                        <valueType type="TIMECODE"/>
                    </propertyDefinition>
                    <propertyDefinition displayName="videobitrate" name="videobitrate" group="Rate Control" dynamic="true">
                        <propertyRedirect propertyPath="AVC Encoder/rc.kbps"/>
                    </propertyDefinition>
                    <propertyDefinition displayName="Insert CGMS-A" name="insert-cgms-a" group="Copy Generation Management System (Analog)" dynamic="true">
                        <initialValue>FALSE</initialValue>
                        <valueType type="BOOLEAN"/>
                    </propertyDefinition>
                    <propertyDefinition displayName="CGMS-A Bit:" name="cgms-a-bit" group="Copy Generation Management System (Analog)" dynamic="true">
                        <initialValue>0</initialValue>
                        <valueType type="STRING">
                            <valueRestriction strictEnum="true">
                                <enumerationValue val="0" displayName="(0,0) Copying is permitted without restriction"></enumerationValue>
                                <enumerationValue val="1" displayName="(0,1) Condition not to be used"></enumerationValue>
                                <enumerationValue val="2" displayName="(1,0) One generation of copies may be made"></enumerationValue>
                                <enumerationValue val="3" displayName="(1,1) No copying is permitted"></enumerationValue>
                            </valueRestriction>
                        </valueType>
                    </propertyDefinition>
                    <propertyDefinition displayName="APS Bit:" name="aps-bit" group="Copy Generation Management System (Analog)" dynamic="true">
                        <initialValue>0</initialValue>
                        <valueType type="STRING">
                            <valueRestriction strictEnum="true">
                                <enumerationValue val="0" displayName="(0,0) No APS"></enumerationValue>
                                <enumerationValue val="1" displayName="(0,1) PSP On; Split Burst Off"></enumerationValue>
                                <enumerationValue val="2" displayName="(1,0) PSP On; 2 line Split Burst On"></enumerationValue>
                                <enumerationValue val="3" displayName="(1,1) PSP On; 4 line Split Burst On"></enumerationValue>
                            </valueRestriction>
                        </valueType>
                    </propertyDefinition>
                    <propertyDefinition displayName="Analog Source Bit:" name="analog-source-bit" group="Copy Generation Management System (Analog)" dynamic="true">
                        <initialValue>0</initialValue>
                        <valueType type="STRING">
                            <valueRestriction strictEnum="true">
                                <enumerationValue val="0"></enumerationValue>
                                <enumerationValue val="1"></enumerationValue>
                            </valueRestriction>
                        </valueType>
                    </propertyDefinition>
                    <propertyDefinition displayName="Insert Content Advisory (V-Chip Information)" name="insert-v-chip-info" group="Content Advisory (V-Chip Information)" dynamic="true">
                        <initialValue>FALSE</initialValue>
                        <valueType type="BOOLEAN"/>
                    </propertyDefinition>
                    <propertyDefinition displayName="System:" name="system" group="Content Advisory (V-Chip Information)" dynamic="true">
                        <initialValue>0</initialValue>
                        <valueType type="STRING">
                            <valueRestriction strictEnum="true">
                                <enumerationValue val="0" displayName="MPAA"></enumerationValue>
                                <enumerationValue val="1" displayName="U.S. TV Parental Guidelines"></enumerationValue>
                                <enumerationValue val="2" displayName="Canadian English Language Rating"></enumerationValue>
                                <enumerationValue val="3" displayName="Canadian French Language Rating"></enumerationValue>
                            </valueRestriction>
                        </valueType>
                    </propertyDefinition>
                    <propertyDefinition displayName="Rating:" name="rating" group="Content Advisory (V-Chip Information)" dynamic="true">
                        <valueType type="STRING">
                            <valueRestriction strictEnum="true"/>
                        </valueType>
                    </propertyDefinition>
                    <propertyDefinition displayName="Fantasy Violence" name="fantasy-violence" group="Content Advisory (V-Chip Information)" dynamic="true">
                        <initialValue>FALSE</initialValue>
                        <valueType type="BOOLEAN"/>
                    </propertyDefinition>
                    <propertyDefinition displayName="Violence" name="violence" group="Content Advisory (V-Chip Information)" dynamic="true">
                        <initialValue>FALSE</initialValue>
                        <valueType type="BOOLEAN"/>
                    </propertyDefinition>
                    <propertyDefinition displayName="Sexual Situations" name="sexual-situations" group="Content Advisory (V-Chip Information)" dynamic="true">
                        <initialValue>FALSE</initialValue>
                        <valueType type="BOOLEAN"/>
                    </propertyDefinition>
                    <propertyDefinition displayName="Adult Language" name="adult-language" group="Content Advisory (V-Chip Information)" dynamic="true">
                        <initialValue>FALSE</initialValue>
                        <valueType type="BOOLEAN"/>
                    </propertyDefinition>
                    <propertyDefinition displayName="Sexually Suggestive Dialog" name="sexually-suggestive-dialog" group="Content Advisory (V-Chip Information)" dynamic="true">
                        <initialValue>FALSE</initialValue>
                        <valueType type="BOOLEAN"/>
                    </propertyDefinition>
                    <property name="Aspect Ratio" isNull="true"/>
                    <property name="Frame Rate" isNull="true"/>
                    <property name="General.extended_sar" isNull="true"/>
                    <property name="General.sar" isNull="true"/>
                    <property name="Timecode">none</property>
                    <property name="_graphDisplayContents">false</property>
                    <property name="_graphDisplayLocation">1597.0,144.59274291992188</property>
                    <property name="_graphMinDisplaySize">1052.0,214.0</property>
                    <property name="_logDebugInfoOnError" isNull="true"/>
                    <property name="_timeBase_local" isNull="true"/>
                    <property name="_useSerializedDataTypeDefs" isNull="true"/>
                    <property name="acquireChildLicenses" isNull="true"/>
                    <property name="adult-language">FALSE</property>
                    <property name="adv.custom_frame_rate_timescale">240000</property>
                    <property name="adv.direct_mode">0</property>
                    <property name="adv.enable_custom_frame_rate_timescale">false</property>
                    <property name="adv.intra_precision">2</property>
                    <property name="adv.mbaff_scan_type">1</property>
                    <property name="adv.ps3_mode">0</property>
                    <property name="adv.qt_mode">0</property>
                    <property name="adv.transform_8x8">0</property>
                    <property name="adv.weighted_pred_flag">1</property>
                    <property name="alternateJava" isNull="true"/>
                    <property name="analog-source-bit">0</property>
                    <property name="aps-bit">0</property>
                    <property name="cadenceReEntryMode">0</property>
                    <property name="cgms-a-bit">0</property>
                    <property name="defaultInputPin">Video</property>
                    <property name="defaultOutputPin">out</property>
                    <property name="detect2224Cadence">true</property>
                    <property name="detect22Cadence">true</property>
                    <property name="detect2332Cadence">true</property>
                    <property name="detect32322Cadence">true</property>
                    <property name="detect32Cadence">true</property>
                    <property name="detect55Cadence">true</property>
                    <property name="detect64Cadence">true</property>
                    <property name="detect87Cadence">true</property>
                    <property name="dominance">pass_through</property>
                    <property name="edge_smoothing" isNull="true"/>
                    <property name="fantasy-violence">FALSE</property>
                    <property name="filterControl">0</property>
                    <property name="filterType">0</property>
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
                    <property name="ignoreChildComponentErrors" isNull="true"/>
                    <property name="ignoreParentGraphState" isNull="true"/>
                    <property name="insert-cgms-a">FALSE</property>
                    <property name="insert-v-chip-info">FALSE</property>
                    <property name="interpolation">CUBIC2P_BSPLINE</property>
                    <property name="javaSelector" isNull="true"/>
                    <property name="latency">30</property>
                    <property name="legacyDataTypeSerialization" isNull="true"/>
                    <property name="logsMaxEntries" isNull="true"/>
                    <property name="me.max_refs">1</property>
                    <property name="me.search_range">-1</property>
                    <property name="me.subdiv">7</property>
                    <property name="mt.max_pict_tasks">-1</property>
                    <property name="mt.mode">2</property>
                    <property name="mt.num_threads">8</property>
                    <property name="noiseTolerance">0</property>
                    <property name="oopLaunchTimeoutSeconds" isNull="true"/>
                    <property name="outputFrameRate">matchInputFieldRate</property>
                    <property name="output_height">720</property>
                    <property name="output_width">1280</property>
                    <property name="pluginOptions" isNull="true"/>
                    <property name="preset">yuyv</property>
                    <property name="pulldown_mode">2:3TFF</property>
                    <property name="rating">0</property>
                    <property name="rc.auto_qp">true</property>
                    <property name="rc.initial_cpb_removal_delay">-1</property>
                    <property name="rc.kbps">2000</property>
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
                    <property name="runOutOfProcess">if32bit</property>
                    <property name="sei.pic_timing_flag">1</property>
                    <property name="sei.split_sei_payload">0</property>
                    <property name="sexual-situations">FALSE</property>
                    <property name="sexually-suggestive-dialog">FALSE</property>
                    <property name="shift">shift_up</property>
                    <property name="showOutOfProcessGUI" isNull="true"/>
                    <property name="slice.deblock">1</property>
                    <property name="slice.mode">0</property>
                    <property name="slice.param">1</property>
                    <property name="start_timecode">00:00:00:00/30</property>
                    <property name="system">0</property>
                    <property name="threads">1</property>
                    <property name="verticalBandwidthControl">1</property>
                    <property name="video_format">Same as Input(1tc/field)</property>
                    <property name="videobitrate">2000</property>
                    <property name="violence">FALSE</property>
                    <property name="vui.bitstream_restriction_flag">false</property>
                    <property name="vui.colour_description_present_flag">false</property>
                    <property name="vui.colour_primaries">2</property>
                    <property name="vui.matrix_coeffients">2</property>
                    <property name="vui.max_bytes_per_pic_denom">false</property>
                    <property name="vui.nal_hrd_parameters_present_flag">false</property>
                    <property name="vui.pic_struct_present_flag">true</property>
                    <property name="vui.timing_info_present_flag">false</property>
                    <property name="vui.transfer_characteristics">2</property>
                    <property name="vui.vcl_hrd_parameters_present_flag">false</property>
                    <property name="vui.video_format">1</property>
                    <property name="vui.video_signal_type_present_flag">true</property>
                    <componentName>AVC Video Encoder</componentName>
                    <componentDefinitionName>Advanced AVC Encoder</componentDefinitionName>
                    <componentDefinitionGuid>A3597472-D51E-44d9-9F0A-395744A83FA3</componentDefinitionGuid>
                    <componentOwningPluginName>DRAVCEncoder</componentOwningPluginName>
                    <componentOwningPluginId>ca.digitalrapids.DRAVCEncoder</componentOwningPluginId>
                    <childComponents/>
                    <pin name="Video" type="INPUT_IO"/>
                    <pin name="out" type="OUTPUT_IO"/>
                    <pin name="Timecode (SEI)" type="INPUT_IO"/>
                    <pin name="EIA-608 Captions" type="INPUT_IO"/>
                    <pin name="EIA-708 Captions" type="INPUT_IO">
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" dynamic="true"/>
                        <property name="_graphDisplayLocation">-61,102</property>
                    </pin>
                    <pin name="User Data" type="INPUT_IO">
                        <propertyDefinition hidden="true" name="_graphDisplayLocation" dynamic="true"/>
                        <property name="_graphDisplayLocation">-57,133</property>
                    </pin>
                </component>
            </childComponents>
            <pin name="primarySourceFile" type="PROPERTY">
                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                <property name="_graphDisplayLocation">8.0,10.0</property>
                <property name="_pinProperty">primarySourceFile</property>
            </pin>
            <pin name="clipListXml" type="PROPERTY">
                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                <property name="_graphDisplayLocation">8.0,40.0</property>
                <property name="_pinProperty">clipListXml</property>
            </pin>
            <pin name="outputAssetsCommand" type="COMMAND">
                <propertyDefinition hidden="true" name="_graphDisplayLocation" group="System" dynamic="true"/>
                <property name="_graphDisplayLocation">2654.3095703125,273.0</property>
            </pin>
        </component>
    </components>
    <pinConnections>
        <connection>
            <sourcePath>File Output/writeComplete</sourcePath>
            <destinationPath>outputAssetsCommand</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/UncompressedAudio</sourcePath>
            <destinationPath>AAC Encoder (Dolby)/audio</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input/UncompressedVideo</sourcePath>
            <destinationPath>Video Data Type Updater/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video Format Converter/out</sourcePath>
            <destinationPath>Graphic Overlay/background</destinationPath>
        </connection>
        <connection>
            <sourcePath>Media File Input Logo/SampleInformation</sourcePath>
            <destinationPath>Graphic Overlay/overlay</destinationPath>
        </connection>
        <connection>
            <sourcePath>Graphic Overlay/out</sourcePath>
            <destinationPath>AVC Video Encoder/Video</destinationPath>
        </connection>
        <connection>
            <sourcePath>AAC Encoder (Dolby)/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer/Track 2</destinationPath>
        </connection>
        <connection>
            <sourcePath>ISO MPEG-4 Multiplexer/mp4</sourcePath>
            <destinationPath>File Output/write</destinationPath>
        </connection>
        <connection>
            <sourcePath>Video Data Type Updater/out</sourcePath>
            <destinationPath>Video Format Converter/in</destinationPath>
        </connection>
        <connection>
            <sourcePath>AVC Video Encoder/out</sourcePath>
            <destinationPath>ISO MPEG-4 Multiplexer/Track 1</destinationPath>
        </connection>
    </pinConnections>
    <authoringInfo>
        <kayakFrameworkVersion>1.4.8.1</kayakFrameworkVersion>
        <userName>xpouyat</userName>
        <userLanguage>en</userLanguage>
        <platform>Windows</platform>
        <osName>Windows 8.1</osName>
        <osArch>amd64</osArch>
        <osVersion>6.3</osVersion>
        <authoredDate>2016-05-13T17:40:06.298+02:00</authoredDate>
        <plugins>
            <plugin pluginId="ca.digitalrapids.AACDecoder" name="AAC Decoder">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-06-17 11:43:45-0400</buildDate>
                    <checksum>08c21e53d0bc5305694e664a7e60c4a3</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AACSourceController" name="AAC Source Controller">
                <pluginVersion>1.2.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-10-07 11:48:00-0400</buildDate>
                    <checksum>0c685b5c9530b99267b47a3c42aef5d4</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AC3SourceController" name="AC3 Source Controller">
                <pluginVersion>1.3.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-08-18 11:04:22-0400</buildDate>
                    <checksum>ae86e965ae3856dec8c8413d8d4f1dca</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AES3AudioProcessor" name="AES3AudioProcessor">
                <pluginVersion>1.3.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-08-18 11:04:59-0400</buildDate>
                    <checksum>97a8f2d49c2be04302f38f8c3ce12245</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AVCDecoder" name="AVCDecoder">
                <pluginVersion>1.3.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-08-17 17:24:55-0400</buildDate>
                    <checksum>591e07bb6cf6ebf861bc1474697531a6</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AVCSourceController" name="AVC Source Controller">
                <pluginVersion>1.3.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-08-17 17:25:29-0400</buildDate>
                    <checksum>95226dc9aaa2fb5ccdf44f9bde1d18f4</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.ActiveImageCrop" name="ActiveImageCrop">
                <pluginVersion>1.0.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-10-29 12:33:30-0400</buildDate>
                    <checksum>17f8c8d074f0b95058f6f92fa2be7196</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AlphaChannelUtilities" name="AlphaChannelUtilities">
                <pluginVersion>1.0.6.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-09-17 10:40:36-0400</buildDate>
                    <checksum>b0ddaf325278ac42026c3f2d9e5a8c65</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.Assets" name="Assets">
                <pluginVersion>1.0.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-18 16:43:49-0400</buildDate>
                    <checksum>255263cf15e1f4f2eedd6b3818774c5e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AudioFormatConverter" name="AudioFormatConverter">
                <pluginVersion>1.1.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-06-09 15:02:21-0400</buildDate>
                    <checksum>9bf6a67476a96a89266c637a70f4eb4f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.AudioFormatUtilities" name="AudioFormatUtilities">
                <pluginVersion>1.5.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-08-18 11:03:15-0400</buildDate>
                    <checksum>4965c839940a4867abf6232c861b082d</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.ChromaResampler" name="ChromaResampler">
                <pluginVersion>1.0.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-09-17 10:40:57-0400</buildDate>
                    <checksum>ae96956d27f9bfaabedbabc16aa3dc75</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.ClosedCaptionsUtilities" name="ClosedCaptionsUtilities">
                <pluginVersion>1.3.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-09-25 13:08:29-0400</buildDate>
                    <checksum>e3b23cf84633cbefde980b9b1a120409</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonAAC" name="CommonAAC">
                <pluginVersion>1.1.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-01 14:19:19-0400</buildDate>
                    <checksum>e59e318f578c22b83b388f9e10280b88</checksum>
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
                <pluginVersion>1.2.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-20 16:19:50-0400</buildDate>
                    <checksum>24fafe6e413585ef3baf1a441c9737f6</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonComponents" name="CommonComponents">
                <pluginVersion>1.12.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-09-14 11:52:13-0400</buildDate>
                    <checksum>6e707be5245fd6fcd7974524aac51758</checksum>
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
                <pluginVersion>1.0.23.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-09-09 10:46:03-0400</buildDate>
                    <checksum>8ed8446b7e9e440d7fbb8fc1d8ab47b0</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonIntelIPP" name="CommonIntelIPP">
                <pluginVersion>1.0.11.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-18 15:39:11-0400</buildDate>
                    <checksum>e6f3740379a0221f52a423b40710dc08</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonIntelUIC" name="CommonIntelUIC">
                <pluginVersion>1.0.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-18 15:57:47-0400</buildDate>
                    <checksum>69e0825448a3d67ba6b00f58d2a221f9</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonLanguage" name="CommonLanguage">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-20 16:18:43-0400</buildDate>
                    <checksum>852cff613f6f650b4728c5b7f24975b9</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMPEG" name="CommonMPEG">
                <pluginVersion>1.0.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 13:13:48-0400</buildDate>
                    <checksum>a934a85eb4d0c86a757d80a792ae032c</checksum>
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
                <pluginVersion>1.4.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-06-17 17:16:26-0400</buildDate>
                    <checksum>090a76986e934d4d00daf6f5c06bb3dd</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMPEG4" name="CommonMPEG4">
                <pluginVersion>1.0.11.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 13:26:02-0400</buildDate>
                    <checksum>ece896a89240b90c772bf89fb8179658</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonMedia" name="CommonMedia">
                <pluginVersion>1.16.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-09-16 10:05:28-0400</buildDate>
                    <checksum>83f554236414ca74560ceba3241ce077</checksum>
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
                <pluginVersion>1.0.5.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 12:54:21-0400</buildDate>
                    <checksum>69fc09d4a2979ccfd562a4ef693a60d7</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonPlayReadyEncryption" name="CommonPlayReadyEncryption">
                <pluginVersion>1.0.5.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 13:59:20-0400</buildDate>
                    <checksum>f612be0829038b2d63b80d948dff3ee8</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.CommonQuickTime" name="CommonQuickTime">
                <pluginVersion>1.0.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-21 15:35:15-0400</buildDate>
                    <checksum>2f8c5ead8bfb9e7f614f6c9ae9684166</checksum>
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
                <pluginVersion>1.0.12.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-11 19:16:58-0500</buildDate>
                    <checksum>3dfdd49f8e071fe886e76060bde6d997</checksum>
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
                <pluginVersion>1.6.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-07-22 17:05:19-0400</buildDate>
                    <checksum>b3a98a4699df4526a32b7c73b7dbc5f3</checksum>
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
                <pluginVersion>1.0.84.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-27 10:54:12-0500</buildDate>
                    <checksum>916b5475ca9abbb5a9912712c61f0bc5</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRColorspaceConverter" name="DRColorspaceConverter">
                <pluginVersion>1.2.7.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-09-16 16:25:31-0400</buildDate>
                    <checksum>e79b443db6ab20c50018a70085b3210b</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRDeinterlacer" name="DRDeinterlacer">
                <pluginVersion>1.4.14.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-09-16 16:25:50-0400</buildDate>
                    <checksum>aaa6c12ddc9c2a89651a7589b27a6e1f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRInterlacer" name="DRInterlacer">
                <pluginVersion>1.0.9.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-04-16 21:42:39-0400</buildDate>
                    <checksum>b61716a094642a365269197fce82db31</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRMediaProcessing" name="DRMediaProcessing">
                <pluginVersion>2.9.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-14 15:33:38-0400</buildDate>
                    <checksum>cc6f0ce4bbbe2e71b048d2f8f789f0be</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRProgressiveToInterlaced" name="DRProgressiveToInterlaced">
                <pluginVersion>1.1.13.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-07 10:38:43-0400</buildDate>
                    <checksum>4b2417a689ce175618a276d867198572</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRScaler" name="DRScaler">
                <pluginVersion>1.2.13.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-07 10:39:07-0400</buildDate>
                    <checksum>ac668ccec6a3abcb013e10b5c1783d9a</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DRTemporalNoiseReduction" name="DRTemporalNoiseReduction">
                <pluginVersion>1.2.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-08-28 18:24:14-0400</buildDate>
                    <checksum>b68a003311890a7576cca4695564d82d</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DTSSourceController" name="DTS Source Controller">
                <pluginVersion>1.0.12.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-11-01 15:22:20-0400</buildDate>
                    <checksum>374aaf0ece6f5fecefc8c73de24a6f3e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DirectShowFileSource" name="DirectShowFileSource">
                <pluginVersion>1.0.19.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-11-20 21:17:46-0500</buildDate>
                    <checksum>e35fae50cdcf706cb794dd1bd14b35cf</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DolbyESourceController" name="Dolby E Source Controller">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-06-05 17:14:51-0400</buildDate>
                    <checksum>5ee1e319515d0f517a25b6661a53f28e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.DolbyPulseEncoder" name="DolbyPulseEncoder">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-17 19:11:58-0400</buildDate>
                    <checksum>1ebe4d7dd3886622d11c93de75601ea7</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.EAC3SourceController" name="EAC3 Source Controller">
                <pluginVersion>1.2.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-08-06 12:33:32-0400</buildDate>
                    <checksum>934035cd7e7f7bfe8f3785541596b871</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.EIACaptionsRetimer" name="EIACaptionsRetimer">
                <pluginVersion>1.2.6.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-08-31 16:55:33-0400</buildDate>
                    <checksum>6bae71ce30027f0b8362a72cd0d50aa0</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.JavaImage" name="JavaImage">
                <pluginVersion>1.0.18.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-12-02 15:52:36-0500</buildDate>
                    <checksum>44e17b088c6a4ff46ec7e383df45637d</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.KayakCore" name="KayakCore">
                <pluginVersion>1.4.8.1</pluginVersion>
                <buildInfo>
                    <buildDate>2015-08-13 15:13:37-0400</buildDate>
                    <checksum>18b5805b558dd8819bea087a77a35cdf</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.KayakDesigner" name="KayakDesigner">
                <pluginVersion>1.3.15.1</pluginVersion>
                <buildInfo>
                    <buildDate>2015-07-20 16:03:45-0400</buildDate>
                    <checksum>ae550dae427f1785d87af37df9d09c76</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2AudioSourceController" name="MPEG2AudioSourceController">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-06-17 11:46:59-0400</buildDate>
                    <checksum>b9412d42882da2f072b1c100f756fbbd</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2UDDemuxer" name="MPEG2UDDemuxer">
                <pluginVersion>1.3.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-07-31 15:36:52-0400</buildDate>
                    <checksum>78b72d3de8dfe422d9904f25b3787056</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG2UDMuxer" name="MPEG2UDMuxer">
                <pluginVersion>1.4.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-09-16 10:24:41-0400</buildDate>
                    <checksum>6a9ff02df1b72ed7f09a39bd0ba9ae91</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG4Demuxer" name="MPEG4Demuxer">
                <pluginVersion>1.1.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-22 11:21:40-0400</buildDate>
                    <checksum>ea535099a20e96c521633d9f846bcd41</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MPEG4Muxer" name="MPEG4Muxer">
                <pluginVersion>1.9.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-09-09 11:50:36-0400</buildDate>
                    <checksum>18e194149199b0b2e05d0e97c6011125</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MediaInspection" name="MediaInspection">
                <pluginVersion>1.4.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-09-25 13:07:48-0400</buildDate>
                    <checksum>991235103100df96b26174f62dde0309</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MediaManager" name="Media Manager">
                <pluginVersion>1.0.57.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-06-09 11:35:15-0400</buildDate>
                    <checksum>bc1d99a531bc4120bb59f2d61b2a3314</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.MediaManagerWSClient" name="Media Manager WS Client">
                <pluginVersion>1.0.10.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-06-02 13:58:30-0400</buildDate>
                    <checksum>8d95df55931735bcd0069c19b36732f2</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.Overlay" name="Overlay">
                <pluginVersion>1.0.13.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-06 15:23:14-0500</buildDate>
                    <checksum>fe74a94f97a02854e6c0bb19bad39402</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.OverlayCore" name="OverlayCore">
                <pluginVersion>1.2.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-17 19:22:53-0400</buildDate>
                    <checksum>49412ddcc61eedd0c9240066aa7dc06e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.PNGDecoder" name="PNGDecoder">
                <pluginVersion>1.0.5.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-23 16:28:21-0400</buildDate>
                    <checksum>be1da82fd91ae23a6afa6a268e18a831</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.PNGSourceController" name="PNGSourceController">
                <pluginVersion>1.0.11.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-07-10 10:47:50-0400</buildDate>
                    <checksum>a4d650e610591b683e0b54c56e682f44</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.RasterFlip" name="RasterFlip">
                <pluginVersion>1.0.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-09-17 10:45:02-0400</buildDate>
                    <checksum>a6c919f26eabd6cab091959c15aa64bf</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.SCCSourceController" name="SCCSourceController">
                <pluginVersion>1.4.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-09-25 13:08:02-0400</buildDate>
                    <checksum>40c4d0c6227d0c8098e845845313de9b</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.StreamSynchronizers" name="StreamSynchronizers">
                <pluginVersion>1.3.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-09-23 16:16:46-0400</buildDate>
                    <checksum>242147de7778ad4b7e2cb8cb8f8cfa9f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.TimecodeEncoder" name="TimecodeEncoder">
                <pluginVersion>1.2.4.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-07-13 17:12:03-0400</buildDate>
                    <checksum>83120ba40f827889585b682d81cb2bc6</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoBitDepthConverters" name="VideoBitDepthConverters">
                <pluginVersion>1.0.2.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-09-17 10:45:26-0400</buildDate>
                    <checksum>63619f11038b6b413b3a923335dde1f3</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoBorder" name="VideoBorder">
                <pluginVersion>1.1.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-29 14:33:12-0400</buildDate>
                    <checksum>d32a84e6cbff8be45918b3648dd98d49</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoDataLayoutConverter" name="VideoDataLayoutConverter">
                <pluginVersion>1.2.0.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-09-16 10:06:39-0400</buildDate>
                    <checksum>672c6f105e28d1051bc6b38c3b2296ea</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoDeinterlacers" name="VideoDeinterlacers">
                <pluginVersion>1.0.11.0</pluginVersion>
                <buildInfo>
                    <buildDate>2013-10-23 14:22:16-0400</buildDate>
                    <checksum>b81fd23b4334dbdba1b981c9577f82e5</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoFormatConverter" name="VideoFormatConverter">
                <pluginVersion>1.0.53.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-07 10:43:13-0400</buildDate>
                    <checksum>db738eb54a3afa6b6226767f44c74c60</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoFormatConverter2" name="VideoFormatConverter2">
                <pluginVersion>1.4.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-09-16 10:08:26-0400</buildDate>
                    <checksum>bfd2219f8bf14ccb9f947275e4eb19df</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoFormatUtilities" name="VideoFormatUtilities">
                <pluginVersion>1.2.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-17 19:20:10-0400</buildDate>
                    <checksum>51d52ff49c3aa5d2b8a16e7e73e8492e</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoGenerators" name="VideoGenerators">
                <pluginVersion>1.0.17.0</pluginVersion>
                <buildInfo>
                    <buildDate>2014-11-19 12:17:42-0500</buildDate>
                    <checksum>dcb71e7cb5f728813412eb56e2febe08</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoProcessor" name="VideoProcessor">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-17 19:20:48-0400</buildDate>
                    <checksum>aebec85e4a723c6cbadb73308e3b384f</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="ca.digitalrapids.VideoPulldownProcessor" name="VideoPulldownProcessor">
                <pluginVersion>1.2.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-04-17 19:27:14-0400</buildDate>
                    <checksum>915fb9dc3f324fecb5041fcd437d6cad</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.AFDUtilities" name="AFDUtilities">
                <pluginVersion>1.2.3.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-07-20 18:09:12-0400</buildDate>
                    <checksum>da3dd432a56aa239de49fd80491a5fd3</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.CommonMediaOrigin" name="CommonMediaOrigin">
                <pluginVersion>1.1.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-03-16 17:20:55-0400</buildDate>
                    <checksum>b850aa9a2d360cf67e995dd215f07965</checksum>
                </buildInfo>
            </plugin>
            <plugin pluginId="com.imaginecommunications.CommonProRes" name="CommonProRes">
                <pluginVersion>1.0.1.0</pluginVersion>
                <buildInfo>
                    <buildDate>2015-08-06 15:30:13-0400</buildDate>
                    <checksum>f2185b7a64aae3f9610c9282331f70ec</checksum>
                </buildInfo>
            </plugin>
        </plugins>
        <pluginIdentifiers>
            <plugin name="AAC Decoder" buildDate="2015-06-17 11:43:45-0400" pluginId="ca.digitalrapids.AACDecoder" pluginVersion="1.1.1.0" platform="Windows" checksum="08c21e53d0bc5305694e664a7e60c4a3"/>
            <plugin name="AAC Source Controller" buildDate="2015-10-07 11:48:00-0400" pluginId="ca.digitalrapids.AACSourceController" pluginVersion="1.2.4.0" platform="Windows" checksum="0c685b5c9530b99267b47a3c42aef5d4"/>
            <plugin name="AC3 Source Controller" buildDate="2015-08-18 11:04:22-0400" pluginId="ca.digitalrapids.AC3SourceController" pluginVersion="1.3.1.0" platform="Windows" checksum="ae86e965ae3856dec8c8413d8d4f1dca"/>
            <plugin name="AES3AudioProcessor" buildDate="2015-08-18 11:04:59-0400" pluginId="ca.digitalrapids.AES3AudioProcessor" pluginVersion="1.3.2.0" platform="Windows" checksum="97a8f2d49c2be04302f38f8c3ce12245"/>
            <plugin name="AVCDecoder" buildDate="2015-08-17 17:24:55-0400" pluginId="ca.digitalrapids.AVCDecoder" pluginVersion="1.3.3.0" platform="Windows" checksum="591e07bb6cf6ebf861bc1474697531a6"/>
            <plugin name="AVC Source Controller" buildDate="2015-08-17 17:25:29-0400" pluginId="ca.digitalrapids.AVCSourceController" pluginVersion="1.3.1.0" platform="Windows" checksum="95226dc9aaa2fb5ccdf44f9bde1d18f4"/>
            <plugin name="ActiveImageCrop" buildDate="2014-10-29 12:33:30-0400" pluginId="ca.digitalrapids.ActiveImageCrop" pluginVersion="1.0.2.0" platform="Windows" checksum="17f8c8d074f0b95058f6f92fa2be7196"/>
            <plugin name="AlphaChannelUtilities" buildDate="2014-09-17 10:40:36-0400" pluginId="ca.digitalrapids.AlphaChannelUtilities" pluginVersion="1.0.6.0" platform="Windows" checksum="b0ddaf325278ac42026c3f2d9e5a8c65"/>
            <plugin name="Assets" buildDate="2013-10-18 16:43:49-0400" pluginId="ca.digitalrapids.Assets" pluginVersion="1.0.4.0" platform="Generic" checksum="255263cf15e1f4f2eedd6b3818774c5e"/>
            <plugin name="AudioFormatConverter" buildDate="2015-06-09 15:02:21-0400" pluginId="ca.digitalrapids.AudioFormatConverter" pluginVersion="1.1.2.0" platform="Generic" checksum="9bf6a67476a96a89266c637a70f4eb4f"/>
            <plugin name="AudioFormatUtilities" buildDate="2015-08-18 11:03:15-0400" pluginId="ca.digitalrapids.AudioFormatUtilities" pluginVersion="1.5.1.0" platform="Windows" checksum="4965c839940a4867abf6232c861b082d"/>
            <plugin name="ChromaResampler" buildDate="2014-09-17 10:40:57-0400" pluginId="ca.digitalrapids.ChromaResampler" pluginVersion="1.0.2.0" platform="Windows" checksum="ae96956d27f9bfaabedbabc16aa3dc75"/>
            <plugin name="ClosedCaptionsUtilities" buildDate="2015-09-25 13:08:29-0400" pluginId="ca.digitalrapids.ClosedCaptionsUtilities" pluginVersion="1.3.1.0" platform="Windows" checksum="e3b23cf84633cbefde980b9b1a120409"/>
            <plugin name="CommonAAC" buildDate="2015-04-01 14:19:19-0400" pluginId="ca.digitalrapids.CommonAAC" pluginVersion="1.1.2.0" platform="Generic" checksum="e59e318f578c22b83b388f9e10280b88"/>
            <plugin name="CommonAC3" buildDate="2015-08-06 12:30:52-0400" pluginId="ca.digitalrapids.CommonAC3" pluginVersion="1.3.1.0" platform="Generic" checksum="5ce148e5f5783ec69336b72d12d9b4a8"/>
            <plugin name="CommonAES3" buildDate="2015-02-05 12:16:28-0500" pluginId="ca.digitalrapids.CommonAES3" pluginVersion="1.2.1.0" platform="Generic" checksum="450ea9b38fded4b37deb9cbc4f0f28f6"/>
            <plugin name="CommonAVC" buildDate="2015-04-20 16:19:50-0400" pluginId="ca.digitalrapids.CommonAVC" pluginVersion="1.2.1.0" platform="Windows" checksum="24fafe6e413585ef3baf1a441c9737f6"/>
            <plugin name="CommonComponents" buildDate="2015-09-14 11:52:13-0400" pluginId="ca.digitalrapids.CommonComponents" pluginVersion="1.12.2.0" platform="Generic" checksum="6e707be5245fd6fcd7974524aac51758"/>
            <plugin name="CommonDTS" buildDate="2015-02-05 16:29:30-0500" pluginId="ca.digitalrapids.CommonDTS" pluginVersion="1.1.1.0" platform="Generic" checksum="b47ecd82ba53dcca736784efd7d2d6a1"/>
            <plugin name="CommonDV" buildDate="2015-02-05 16:29:44-0500" pluginId="ca.digitalrapids.CommonDV" pluginVersion="1.1.1.0" platform="Generic" checksum="581bda38d92daf60a96174a1760a8c76"/>
            <plugin name="CommonDolbyE" buildDate="2015-06-05 17:12:45-0400" pluginId="ca.digitalrapids.CommonDolbyE" pluginVersion="1.2.1.0" platform="Generic" checksum="8bb859ada3dd0b3c84da638f503dedc5"/>
            <plugin name="CommonEAC3" buildDate="2015-08-06 12:31:10-0400" pluginId="ca.digitalrapids.CommonEAC3" pluginVersion="1.2.1.0" platform="Generic" checksum="fe3828204f7ee515fe56d8f8736f80db"/>
            <plugin name="CommonFont" buildDate="2014-02-21 19:27:37-0500" pluginId="ca.digitalrapids.CommonFont" pluginVersion="1.0.1.0" platform="Generic" checksum="385df375fa90cde4af26294936172be7"/>
            <plugin name="CommonImageFormats" buildDate="2014-09-09 10:46:03-0400" pluginId="ca.digitalrapids.CommonImageFormats" pluginVersion="1.0.23.0" platform="Generic" checksum="8ed8446b7e9e440d7fbb8fc1d8ab47b0"/>
            <plugin name="CommonIntelIPP" buildDate="2013-10-18 15:39:11-0400" pluginId="ca.digitalrapids.CommonIntelIPP" pluginVersion="1.0.11.0" platform="Windows" checksum="e6f3740379a0221f52a423b40710dc08"/>
            <plugin name="CommonIntelUIC" buildDate="2013-10-18 15:57:47-0400" pluginId="ca.digitalrapids.CommonIntelUIC" pluginVersion="1.0.2.0" platform="Windows" checksum="69e0825448a3d67ba6b00f58d2a221f9"/>
            <plugin name="CommonLanguage" buildDate="2015-04-20 16:18:43-0400" pluginId="ca.digitalrapids.CommonLanguage" pluginVersion="1.1.1.0" platform="Windows" checksum="852cff613f6f650b4728c5b7f24975b9"/>
            <plugin name="CommonMPEG" buildDate="2013-10-21 13:13:48-0400" pluginId="ca.digitalrapids.CommonMPEG" pluginVersion="1.0.4.0" platform="Generic" checksum="a934a85eb4d0c86a757d80a792ae032c"/>
            <plugin name="CommonMPEG1" buildDate="2015-02-05 12:17:22-0500" pluginId="ca.digitalrapids.CommonMPEG1" pluginVersion="1.1.1.0" platform="Generic" checksum="27565032c30a0de4541b77262ca34a95"/>
            <plugin name="CommonMPEG2" buildDate="2015-06-17 17:16:26-0400" pluginId="ca.digitalrapids.CommonMPEG2" pluginVersion="1.4.3.0" platform="Generic" checksum="090a76986e934d4d00daf6f5c06bb3dd"/>
            <plugin name="CommonMPEG4" buildDate="2013-10-21 13:26:02-0400" pluginId="ca.digitalrapids.CommonMPEG4" pluginVersion="1.0.11.0" platform="Generic" checksum="ece896a89240b90c772bf89fb8179658"/>
            <plugin name="CommonMedia" buildDate="2015-09-16 10:05:28-0400" pluginId="ca.digitalrapids.CommonMedia" pluginVersion="1.16.1.0" platform="Windows" checksum="83f554236414ca74560ceba3241ce077"/>
            <plugin name="CommonMediaEncryption" buildDate="2014-06-27 17:24:40-0400" pluginId="ca.digitalrapids.CommonMediaEncryption" pluginVersion="1.0.7.0" platform="Generic" checksum="e8137cd8314ceb3134bd2569645d0cd2"/>
            <plugin name="CommonMetadata" buildDate="2013-10-21 12:54:21-0400" pluginId="ca.digitalrapids.CommonMetadata" pluginVersion="1.0.5.0" platform="Generic" checksum="69fc09d4a2979ccfd562a4ef693a60d7"/>
            <plugin name="CommonPlayReadyEncryption" buildDate="2013-10-21 13:59:20-0400" pluginId="ca.digitalrapids.CommonPlayReadyEncryption" pluginVersion="1.0.5.0" platform="Generic" checksum="f612be0829038b2d63b80d948dff3ee8"/>
            <plugin name="CommonQuickTime" buildDate="2013-10-21 15:35:15-0400" pluginId="ca.digitalrapids.CommonQuickTime" pluginVersion="1.0.3.0" platform="Generic" checksum="2f8c5ead8bfb9e7f614f6c9ae9684166"/>
            <plugin name="CommonSCC" buildDate="2015-07-14 12:49:10-0400" pluginId="ca.digitalrapids.CommonSCC" pluginVersion="1.1.1.0" platform="Generic" checksum="6a2f083148a9336012fa6ed2543a09ee"/>
            <plugin name="CommonStereoVideo" buildDate="2015-02-05 16:30:34-0500" pluginId="ca.digitalrapids.CommonStereoVideo" pluginVersion="1.1.1.0" platform="Generic" checksum="f8764aaec77fe5604228de00578cff0b"/>
            <plugin name="CommonSubtitles" buildDate="2014-11-11 19:16:58-0500" pluginId="ca.digitalrapids.CommonSubtitles" pluginVersion="1.0.12.0" platform="Generic" checksum="3dfdd49f8e071fe886e76060bde6d997"/>
            <plugin name="CommonTimecode" buildDate="2015-05-04 16:57:22-0400" pluginId="ca.digitalrapids.CommonTimecode" pluginVersion="1.2.3.0" platform="Generic" checksum="998e14fd976cbb310fdb42cf6bf0b0d6"/>
            <plugin name="CommonTimedText" buildDate="2014-11-11 19:16:43-0500" pluginId="ca.digitalrapids.CommonTimedText" pluginVersion="1.0.2.0" platform="Generic" checksum="bd7301c97546b6cd9e8a91df982358dd"/>
            <plugin name="CommonUltraviolet" buildDate="2014-03-25 11:15:24-0400" pluginId="ca.digitalrapids.CommonUltraviolet" pluginVersion="1.0.3.0" platform="Generic" checksum="0fbaf01cd371c81f4fcb7cf66f41f6d9"/>
            <plugin name="CommonVC3" buildDate="2015-02-05 16:30:50-0500" pluginId="ca.digitalrapids.CommonVC3" pluginVersion="1.1.1.0" platform="Generic" checksum="1fe7814f916437e8d32e470bc8c8ee7e"/>
            <plugin name="CommonVideoSystem" buildDate="2015-07-22 17:05:19-0400" pluginId="ca.digitalrapids.CommonVideoSystem" pluginVersion="1.6.1.0" platform="Windows" checksum="b3a98a4699df4526a32b7c73b7dbc5f3"/>
            <plugin name="CommonWAVE" buildDate="2015-02-05 12:17:51-0500" pluginId="ca.digitalrapids.CommonWAVE" pluginVersion="1.1.1.0" platform="Generic" checksum="0355d46d1c92231032fed50b5947275b"/>
            <plugin name="DRAVCEncoder" buildDate="2014-11-27 10:54:12-0500" pluginId="ca.digitalrapids.DRAVCEncoder" pluginVersion="1.0.84.0" platform="Windows" checksum="916b5475ca9abbb5a9912712c61f0bc5"/>
            <plugin name="DRColorspaceConverter" buildDate="2014-09-16 16:25:31-0400" pluginId="ca.digitalrapids.DRColorspaceConverter" pluginVersion="1.2.7.0" platform="Windows" checksum="e79b443db6ab20c50018a70085b3210b"/>
            <plugin name="DRDeinterlacer" buildDate="2014-09-16 16:25:50-0400" pluginId="ca.digitalrapids.DRDeinterlacer" pluginVersion="1.4.14.0" platform="Windows" checksum="aaa6c12ddc9c2a89651a7589b27a6e1f"/>
            <plugin name="DRInterlacer" buildDate="2014-04-16 21:42:39-0400" pluginId="ca.digitalrapids.DRInterlacer" pluginVersion="1.0.9.0" platform="Windows" checksum="b61716a094642a365269197fce82db31"/>
            <plugin name="DRMediaProcessing" buildDate="2015-04-14 15:33:38-0400" pluginId="ca.digitalrapids.DRMediaProcessing" pluginVersion="2.9.1.0" platform="Windows" checksum="cc6f0ce4bbbe2e71b048d2f8f789f0be"/>
            <plugin name="DRProgressiveToInterlaced" buildDate="2015-04-07 10:38:43-0400" pluginId="ca.digitalrapids.DRProgressiveToInterlaced" pluginVersion="1.1.13.0" platform="Windows" checksum="4b2417a689ce175618a276d867198572"/>
            <plugin name="DRScaler" buildDate="2015-04-07 10:39:07-0400" pluginId="ca.digitalrapids.DRScaler" pluginVersion="1.2.13.0" platform="Windows" checksum="ac668ccec6a3abcb013e10b5c1783d9a"/>
            <plugin name="DRTemporalNoiseReduction" buildDate="2014-08-28 18:24:14-0400" pluginId="ca.digitalrapids.DRTemporalNoiseReduction" pluginVersion="1.2.3.0" platform="Windows" checksum="b68a003311890a7576cca4695564d82d"/>
            <plugin name="DTS Source Controller" buildDate="2013-11-01 15:22:20-0400" pluginId="ca.digitalrapids.DTSSourceController" pluginVersion="1.0.12.0" platform="Windows" checksum="374aaf0ece6f5fecefc8c73de24a6f3e"/>
            <plugin name="DirectShowFileSource" buildDate="2013-11-20 21:17:46-0500" pluginId="ca.digitalrapids.DirectShowFileSource" pluginVersion="1.0.19.0" platform="Windows" checksum="e35fae50cdcf706cb794dd1bd14b35cf"/>
            <plugin name="Dolby E Source Controller" buildDate="2015-06-05 17:14:51-0400" pluginId="ca.digitalrapids.DolbyESourceController" pluginVersion="1.1.1.0" platform="Windows" checksum="5ee1e319515d0f517a25b6661a53f28e"/>
            <plugin name="DolbyPulseEncoder" buildDate="2015-04-17 19:11:58-0400" pluginId="ca.digitalrapids.DolbyPulseEncoder" pluginVersion="1.1.1.0" platform="Windows" checksum="1ebe4d7dd3886622d11c93de75601ea7"/>
            <plugin name="EAC3 Source Controller" buildDate="2015-08-06 12:33:32-0400" pluginId="ca.digitalrapids.EAC3SourceController" pluginVersion="1.2.0.0" platform="Windows" checksum="934035cd7e7f7bfe8f3785541596b871"/>
            <plugin name="EIACaptionsRetimer" buildDate="2015-08-31 16:55:33-0400" pluginId="ca.digitalrapids.EIACaptionsRetimer" pluginVersion="1.2.6.0" platform="Windows" checksum="6bae71ce30027f0b8362a72cd0d50aa0"/>
            <plugin name="JavaImage" buildDate="2013-12-02 15:52:36-0500" pluginId="ca.digitalrapids.JavaImage" pluginVersion="1.0.18.0" platform="Generic" checksum="44e17b088c6a4ff46ec7e383df45637d"/>
            <plugin name="KayakCore" buildDate="2015-08-13 15:13:37-0400" pluginId="ca.digitalrapids.KayakCore" pluginVersion="1.4.8.1" platform="Windows" checksum="18b5805b558dd8819bea087a77a35cdf"/>
            <plugin name="KayakDesigner" buildDate="2015-07-20 16:03:45-0400" pluginId="ca.digitalrapids.KayakDesigner" pluginVersion="1.3.15.1" platform="Generic" checksum="ae550dae427f1785d87af37df9d09c76"/>
            <plugin name="MPEG2AudioSourceController" buildDate="2015-06-17 11:46:59-0400" pluginId="ca.digitalrapids.MPEG2AudioSourceController" pluginVersion="1.1.1.0" platform="Windows" checksum="b9412d42882da2f072b1c100f756fbbd"/>
            <plugin name="MPEG2UDDemuxer" buildDate="2015-07-31 15:36:52-0400" pluginId="ca.digitalrapids.MPEG2UDDemuxer" pluginVersion="1.3.4.0" platform="Windows" checksum="78b72d3de8dfe422d9904f25b3787056"/>
            <plugin name="MPEG2UDMuxer" buildDate="2015-09-16 10:24:41-0400" pluginId="ca.digitalrapids.MPEG2UDMuxer" pluginVersion="1.4.4.0" platform="Windows" checksum="6a9ff02df1b72ed7f09a39bd0ba9ae91"/>
            <plugin name="MPEG4Demuxer" buildDate="2015-04-22 11:21:40-0400" pluginId="ca.digitalrapids.MPEG4Demuxer" pluginVersion="1.1.2.0" platform="Windows" checksum="ea535099a20e96c521633d9f846bcd41"/>
            <plugin name="MPEG4Muxer" buildDate="2015-09-09 11:50:36-0400" pluginId="ca.digitalrapids.MPEG4Muxer" pluginVersion="1.9.1.0" platform="Windows" checksum="18e194149199b0b2e05d0e97c6011125"/>
            <plugin name="MediaInspection" buildDate="2015-09-25 13:07:48-0400" pluginId="ca.digitalrapids.MediaInspection" pluginVersion="1.4.1.0" platform="Generic" checksum="991235103100df96b26174f62dde0309"/>
            <plugin name="Media Manager" buildDate="2015-06-09 11:35:15-0400" pluginId="ca.digitalrapids.MediaManager" pluginVersion="1.0.57.0" platform="Generic" checksum="bc1d99a531bc4120bb59f2d61b2a3314"/>
            <plugin name="Media Manager WS Client" buildDate="2015-06-02 13:58:30-0400" pluginId="ca.digitalrapids.MediaManagerWSClient" pluginVersion="1.0.10.0" platform="Generic" checksum="8d95df55931735bcd0069c19b36732f2"/>
            <plugin name="Overlay" buildDate="2014-11-06 15:23:14-0500" pluginId="ca.digitalrapids.Overlay" pluginVersion="1.0.13.0" platform="Windows" checksum="fe74a94f97a02854e6c0bb19bad39402"/>
            <plugin name="OverlayCore" buildDate="2015-04-17 19:22:53-0400" pluginId="ca.digitalrapids.OverlayCore" pluginVersion="1.2.0.0" platform="Windows" checksum="49412ddcc61eedd0c9240066aa7dc06e"/>
            <plugin name="PNGDecoder" buildDate="2013-10-23 16:28:21-0400" pluginId="ca.digitalrapids.PNGDecoder" pluginVersion="1.0.5.0" platform="Windows" checksum="be1da82fd91ae23a6afa6a268e18a831"/>
            <plugin name="PNGSourceController" buildDate="2014-07-10 10:47:50-0400" pluginId="ca.digitalrapids.PNGSourceController" pluginVersion="1.0.11.0" platform="Generic" checksum="a4d650e610591b683e0b54c56e682f44"/>
            <plugin name="RasterFlip" buildDate="2014-09-17 10:45:02-0400" pluginId="ca.digitalrapids.RasterFlip" pluginVersion="1.0.2.0" platform="Windows" checksum="a6c919f26eabd6cab091959c15aa64bf"/>
            <plugin name="SCCSourceController" buildDate="2015-09-25 13:08:02-0400" pluginId="ca.digitalrapids.SCCSourceController" pluginVersion="1.4.1.0" platform="Windows" checksum="40c4d0c6227d0c8098e845845313de9b"/>
            <plugin name="StreamSynchronizers" buildDate="2015-09-23 16:16:46-0400" pluginId="ca.digitalrapids.StreamSynchronizers" pluginVersion="1.3.2.0" platform="Windows" checksum="242147de7778ad4b7e2cb8cb8f8cfa9f"/>
            <plugin name="TimecodeEncoder" buildDate="2015-07-13 17:12:03-0400" pluginId="ca.digitalrapids.TimecodeEncoder" pluginVersion="1.2.4.0" platform="Windows" checksum="83120ba40f827889585b682d81cb2bc6"/>
            <plugin name="VideoBitDepthConverters" buildDate="2014-09-17 10:45:26-0400" pluginId="ca.digitalrapids.VideoBitDepthConverters" pluginVersion="1.0.2.0" platform="Windows" checksum="63619f11038b6b413b3a923335dde1f3"/>
            <plugin name="VideoBorder" buildDate="2015-04-29 14:33:12-0400" pluginId="ca.digitalrapids.VideoBorder" pluginVersion="1.1.3.0" platform="Windows" checksum="d32a84e6cbff8be45918b3648dd98d49"/>
            <plugin name="VideoDataLayoutConverter" buildDate="2015-09-16 10:06:39-0400" pluginId="ca.digitalrapids.VideoDataLayoutConverter" pluginVersion="1.2.0.0" platform="Windows" checksum="672c6f105e28d1051bc6b38c3b2296ea"/>
            <plugin name="VideoDeinterlacers" buildDate="2013-10-23 14:22:16-0400" pluginId="ca.digitalrapids.VideoDeinterlacers" pluginVersion="1.0.11.0" platform="Windows" checksum="b81fd23b4334dbdba1b981c9577f82e5"/>
            <plugin name="VideoFormatConverter" buildDate="2015-04-07 10:43:13-0400" pluginId="ca.digitalrapids.VideoFormatConverter" pluginVersion="1.0.53.0" platform="Generic" checksum="db738eb54a3afa6b6226767f44c74c60"/>
            <plugin name="VideoFormatConverter2" buildDate="2015-09-16 10:08:26-0400" pluginId="ca.digitalrapids.VideoFormatConverter2" pluginVersion="1.4.1.0" platform="Windows" checksum="bfd2219f8bf14ccb9f947275e4eb19df"/>
            <plugin name="VideoFormatUtilities" buildDate="2015-04-17 19:20:10-0400" pluginId="ca.digitalrapids.VideoFormatUtilities" pluginVersion="1.2.1.0" platform="Windows" checksum="51d52ff49c3aa5d2b8a16e7e73e8492e"/>
            <plugin name="VideoGenerators" buildDate="2014-11-19 12:17:42-0500" pluginId="ca.digitalrapids.VideoGenerators" pluginVersion="1.0.17.0" platform="Generic" checksum="dcb71e7cb5f728813412eb56e2febe08"/>
            <plugin name="VideoProcessor" buildDate="2015-04-17 19:20:48-0400" pluginId="ca.digitalrapids.VideoProcessor" pluginVersion="1.1.1.0" platform="Windows" checksum="aebec85e4a723c6cbadb73308e3b384f"/>
            <plugin name="VideoPulldownProcessor" buildDate="2015-04-17 19:27:14-0400" pluginId="ca.digitalrapids.VideoPulldownProcessor" pluginVersion="1.2.1.0" platform="Windows" checksum="915fb9dc3f324fecb5041fcd437d6cad"/>
            <plugin name="AFDUtilities" buildDate="2015-07-20 18:09:12-0400" pluginId="com.imaginecommunications.AFDUtilities" pluginVersion="1.2.3.0" platform="Windows" checksum="da3dd432a56aa239de49fd80491a5fd3"/>
            <plugin name="CommonMediaOrigin" buildDate="2015-03-16 17:20:55-0400" pluginId="com.imaginecommunications.CommonMediaOrigin" pluginVersion="1.1.1.0" platform="Windows" checksum="b850aa9a2d360cf67e995dd215f07965"/>
            <plugin name="CommonProRes" buildDate="2015-08-06 15:30:13-0400" pluginId="com.imaginecommunications.CommonProRes" pluginVersion="1.0.1.0" platform="Generic" checksum="f2185b7a64aae3f9610c9282331f70ec"/>
        </pluginIdentifiers>
    </authoringInfo>
    <dependencyInfo>
        <plugins>
            <plugin pluginId="ca.digitalrapids.AACDecoder" name="AAC Decoder"/>
            <plugin pluginId="ca.digitalrapids.AACSourceController" name="AAC Source Controller"/>
            <plugin pluginId="ca.digitalrapids.AC3SourceController" name="AC3 Source Controller"/>
            <plugin pluginId="ca.digitalrapids.AES3AudioProcessor" name="AES3AudioProcessor"/>
            <plugin pluginId="ca.digitalrapids.AVCDecoder" name="AVCDecoder"/>
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
            <plugin pluginId="ca.digitalrapids.CommonComponents" name="CommonComponents"/>
            <plugin pluginId="ca.digitalrapids.CommonDTS" name="CommonDTS"/>
            <plugin pluginId="ca.digitalrapids.CommonDV" name="CommonDV"/>
            <plugin pluginId="ca.digitalrapids.CommonDolbyE" name="CommonDolbyE"/>
            <plugin pluginId="ca.digitalrapids.CommonEAC3" name="CommonEAC3"/>
            <plugin pluginId="ca.digitalrapids.CommonFont" name="CommonFont"/>
            <plugin pluginId="ca.digitalrapids.CommonImageFormats" name="CommonImageFormats"/>
            <plugin pluginId="ca.digitalrapids.CommonIntelIPP" name="CommonIntelIPP"/>
            <plugin pluginId="ca.digitalrapids.CommonIntelUIC" name="CommonIntelUIC"/>
            <plugin pluginId="ca.digitalrapids.CommonLanguage" name="CommonLanguage"/>
            <plugin pluginId="ca.digitalrapids.CommonMPEG" name="CommonMPEG"/>
            <plugin pluginId="ca.digitalrapids.CommonMPEG1" name="CommonMPEG1"/>
            <plugin pluginId="ca.digitalrapids.CommonMPEG2" name="CommonMPEG2"/>
            <plugin pluginId="ca.digitalrapids.CommonMPEG4" name="CommonMPEG4"/>
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
            <plugin pluginId="ca.digitalrapids.DRInterlacer" name="DRInterlacer"/>
            <plugin pluginId="ca.digitalrapids.DRMediaProcessing" name="DRMediaProcessing"/>
            <plugin pluginId="ca.digitalrapids.DRProgressiveToInterlaced" name="DRProgressiveToInterlaced"/>
            <plugin pluginId="ca.digitalrapids.DRScaler" name="DRScaler"/>
            <plugin pluginId="ca.digitalrapids.DRTemporalNoiseReduction" name="DRTemporalNoiseReduction"/>
            <plugin pluginId="ca.digitalrapids.DTSSourceController" name="DTS Source Controller"/>
            <plugin pluginId="ca.digitalrapids.DirectShowFileSource" name="DirectShowFileSource"/>
            <plugin pluginId="ca.digitalrapids.DolbyESourceController" name="Dolby E Source Controller"/>
            <plugin pluginId="ca.digitalrapids.DolbyPulseEncoder" name="DolbyPulseEncoder"/>
            <plugin pluginId="ca.digitalrapids.EAC3SourceController" name="EAC3 Source Controller"/>
            <plugin pluginId="ca.digitalrapids.EIACaptionsRetimer" name="EIACaptionsRetimer"/>
            <plugin pluginId="ca.digitalrapids.JavaImage" name="JavaImage"/>
            <plugin pluginId="ca.digitalrapids.KayakCore" name="KayakCore"/>
            <plugin pluginId="ca.digitalrapids.KayakDesigner" name="KayakDesigner"/>
            <plugin pluginId="ca.digitalrapids.MPEG2AudioSourceController" name="MPEG2AudioSourceController"/>
            <plugin pluginId="ca.digitalrapids.MPEG2UDDemuxer" name="MPEG2UDDemuxer"/>
            <plugin pluginId="ca.digitalrapids.MPEG2UDMuxer" name="MPEG2UDMuxer"/>
            <plugin pluginId="ca.digitalrapids.MPEG4Demuxer" name="MPEG4Demuxer"/>
            <plugin pluginId="ca.digitalrapids.MPEG4Muxer" name="MPEG4Muxer"/>
            <plugin pluginId="ca.digitalrapids.MediaInspection" name="MediaInspection"/>
            <plugin pluginId="ca.digitalrapids.MediaManager" name="Media Manager"/>
            <plugin pluginId="ca.digitalrapids.MediaManagerWSClient" name="Media Manager WS Client"/>
            <plugin pluginId="ca.digitalrapids.Overlay" name="Overlay"/>
            <plugin pluginId="ca.digitalrapids.OverlayCore" name="OverlayCore"/>
            <plugin pluginId="ca.digitalrapids.PNGDecoder" name="PNGDecoder"/>
            <plugin pluginId="ca.digitalrapids.PNGSourceController" name="PNGSourceController"/>
            <plugin pluginId="ca.digitalrapids.RasterFlip" name="RasterFlip"/>
            <plugin pluginId="ca.digitalrapids.SCCSourceController" name="SCCSourceController"/>
            <plugin pluginId="ca.digitalrapids.StreamSynchronizers" name="StreamSynchronizers"/>
            <plugin pluginId="ca.digitalrapids.TimecodeEncoder" name="TimecodeEncoder"/>
            <plugin pluginId="ca.digitalrapids.VideoBitDepthConverters" name="VideoBitDepthConverters"/>
            <plugin pluginId="ca.digitalrapids.VideoBorder" name="VideoBorder"/>
            <plugin pluginId="ca.digitalrapids.VideoDataLayoutConverter" name="VideoDataLayoutConverter"/>
            <plugin pluginId="ca.digitalrapids.VideoDeinterlacers" name="VideoDeinterlacers"/>
            <plugin pluginId="ca.digitalrapids.VideoFormatConverter" name="VideoFormatConverter"/>
            <plugin pluginId="ca.digitalrapids.VideoFormatConverter2" name="VideoFormatConverter2"/>
            <plugin pluginId="ca.digitalrapids.VideoFormatUtilities" name="VideoFormatUtilities"/>
            <plugin pluginId="ca.digitalrapids.VideoGenerators" name="VideoGenerators"/>
            <plugin pluginId="ca.digitalrapids.VideoProcessor" name="VideoProcessor"/>
            <plugin pluginId="ca.digitalrapids.VideoPulldownProcessor" name="VideoPulldownProcessor"/>
            <plugin pluginId="com.imaginecommunications.AFDUtilities" name="AFDUtilities"/>
            <plugin pluginId="com.imaginecommunications.CommonMediaOrigin" name="CommonMediaOrigin"/>
            <plugin pluginId="com.imaginecommunications.CommonProRes" name="CommonProRes"/>
        </plugins>
        <components>
            <component pluginId="ca.digitalrapids.AACSourceController" name="AAC ADTS to Raw Converter" guid="eed0ba59-346f-47b0-ba9a-2ea14be6fa53"/>
            <component pluginId="ca.digitalrapids.AACSourceController" name="AAC Controller" guid="784ee2cc-8a15-41c9-b84b-1a79ced4a646"/>
            <component pluginId="ca.digitalrapids.AACDecoder" name="AAC Decoder" displayName="AAC Decoder" guid="ce7e9384-2d1c-4cc6-9953-928a8eab53e9"/>
            <component pluginId="ca.digitalrapids.DolbyPulseEncoder" name="AAC Encoder - Dolby Pulse" displayName="AAC Encoder (Dolby)" guid="D0933A55-4818-4ADC-9301-8BE7687AC9E3"/>
            <component pluginId="ca.digitalrapids.DolbyPulseEncoder" name="Atomic AAC Encoder - Dolby Pulse" displayName="AAC Encoder Core (Dolby)" guid="8916cfea-3397-4310-b5bb-402e27fb0baf"/>
            <component pluginId="ca.digitalrapids.AACSourceController" name="AAC Media Inspector" guid="d62fab72-405c-471d-bcb0-5e231dd44ab2"/>
            <component pluginId="ca.digitalrapids.ActiveImageCrop" name="Active Image Crop" guid="695BC53B-922F-4af1-88D1-6B33A3D05599"/>
            <component pluginId="ca.digitalrapids.AlphaChannelUtilities" name="Alpha Channel Adder" guid="22444100-318E-4379-9CC1-6DD8A74135A2"/>
            <component pluginId="ca.digitalrapids.AlphaChannelUtilities" name="Alpha Channel Remover" guid="92E5AC2E-9CC7-422F-B947-DC39E077D9CB"/>
            <component pluginId="ca.digitalrapids.VideoFormatUtilities" name="Pixel Aspect Ratio Updater" displayName="Aspect Ratio Updater" guid="d75a869d-be42-43e9-b74d-b7d04adf1ae5"/>
            <component pluginId="ca.digitalrapids.AudioFormatConverter" name="Audio Format Converter" displayName="Audio Format Converter (Deprecated)" guid="F2A4515C-ABD5-49f9-B0D5-DB462E4BB674"/>
            <component pluginId="ca.digitalrapids.AVCSourceController" name="AVC Controller" guid="6ae5cf5f-3a25-4f61-8e3c-16c33b474d4c"/>
            <component pluginId="ca.digitalrapids.AVCSourceController" name="AVC Media Inspector" guid="6702fa3b-5f23-4c82-a471-c830337f83f1"/>
            <component pluginId="ca.digitalrapids.AVCSourceController" name="AVC Part10 to Part15 Converter" guid="b2eC0208-f841-4272-8a16-4b88e80d86a5"/>
            <component pluginId="ca.digitalrapids.DRAVCEncoder" name="Advanced AVC Encoder" displayName="AVC Video Encoder" guid="A3597472-D51E-44d9-9F0A-395744A83FA3"/>
            <component pluginId="ca.digitalrapids.DRAVCEncoder" name="AVC Encoder" displayName="AVC Video Encoder Core" guid="16c55dc4-7cd8-4d25-bfec-1cc4aebad739"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Bit Depth Converter" guid="7DF81BC0-6DFD-44fd-BDAA-2E568F65CFF6"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Channel Mapper" displayName="Channel Remapper" guid="771ACEB1-E611-4803-A356-21F221E3753D"/>
            <component pluginId="ca.digitalrapids.ChromaResampler" name="Chroma Resampler" guid="96051CC1-65A8-4574-944E-8427D0598427"/>
            <component pluginId="ca.digitalrapids.VideoFormatUtilities" name="Color Space Converter - Intel" displayName="Color Space Converter - Intel" guid="2FDE07E0-7DBF-47e2-BC73-91F3B82D4392"/>
            <component pluginId="ca.digitalrapids.DRColorspaceConverter" name="DRColorspaceConverter" displayName="Colorspace Converter" guid="A1F022EA-A1BF-446b-B1E2-C1DFEA43F29E"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Container Data Type Merger" guid="b6eac4c1-3c04-4f8d-9654-96da605b9e90"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="Data Type Merger" guid="4971c1a4-07ab-4c9a-93a6-947526a1005d"/>
            <component pluginId="ca.digitalrapids.DRDeinterlacer" name="DRDeinterlacer" displayName="Deinterlacer" guid="750D51F3-FC19-410f-89AB-B7F3E8CAFEDC"/>
            <component pluginId="ca.digitalrapids.ClosedCaptionsUtilities" name="EIA-708 to 608 Converter" displayName="EIA-708 to 608 De-Embedder" guid="57CA8716-84CF-4C7F-B59F-DF34AFE2E73E"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="EndOfStreamNotification" displayName="End of Stream Notification" guid="285BF6A1-3FEA-4c2a-9D2D-4DB4B965C3EA"/>
            <component pluginId="ca.digitalrapids.CommonMedia" name="Endian Converter" guid="D076A34F-6E7D-46BD-875A-4C590B5538BF"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="File Output" guid="9b376163-de8d-4e09-8bed-353725b6b6d6"/>
            <component pluginId="ca.digitalrapids.VideoFormatUtilities" name="Frame Rate Converter" guid="3a36bec1-8b92-442b-92c4-8a8f908a6cd5"/>
            <component pluginId="ca.digitalrapids.Overlay" name="Graphic Overlay" displayName="Graphic Overlay" guid="FD517552-805D-4168-8102-B06C9048915A"/>
            <component pluginId="ca.digitalrapids.AVCDecoder" name="H.264 (AVC)Decoder" displayName="H.264 (AVC) Decoder" guid="5BB7F271-F83A-4180-ADFB-FFD42634052D"/>
            <component pluginId="ca.digitalrapids.VideoBorder" name="Image Border Core 2" displayName="Image Border Core" guid="165205D3-C849-4651-82BF-97EF31CA0827"/>
            <component pluginId="ca.digitalrapids.VideoBorder" name="Image Crop Core 2" displayName="Image Crop Core" guid="32DA642E-168F-4db7-A33C-CB4D395ED2B8"/>
            <component pluginId="ca.digitalrapids.MPEG4Muxer" name="Advanced ISO MPEG4 Multiplexer" displayName="ISO MPEG-4 Multiplexer" guid="E25468C3-A65C-4f1a-8172-E72CE4B63A70"/>
            <component pluginId="ca.digitalrapids.MPEG4Muxer" name="ISO MPEG4 Multiplexer" displayName="ISO MPEG-4 Multiplexer Core" guid="3CC47644-DC6D-4f2b-AB3B-580D305F47CC"/>
            <component pluginId="ca.digitalrapids.CommonLanguage" name="Language Code Updater" guid="563232cc-20ba-453f-8f69-43284cea7abc"/>
            <component pluginId="ca.digitalrapids.CommonMedia" name="Media Data Type Auto Updater" guid="9dc80c38-b4ff-4b3e-8324-2f29abeb461e"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Media File Input" guid="7cec6ecd-a477-4834-bc6f-97e34aa58bb5"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Media Inpection Data Type Merger" guid="A025A4BD-A59D-42e4-B00C-66F67BCB147C"/>
            <component pluginId="ca.digitalrapids.MediaInspection" name="Media Inspector" guid="3ada68f0-f492-4133-87e2-cdb55ae9f7fc"/>
            <component pluginId="ca.digitalrapids.MPEG2UDDemuxer" name="MPEG2 User Data Decoder" displayName="MPEG User Data Decoder" guid="abc91f15-8728-463d-92c3-84a158b24248"/>
            <component pluginId="ca.digitalrapids.MPEG4Demuxer" name="MPEG4 Demultiplexer" displayName="MPEG-4 Demultiplexer" guid="35b5587f-a925-436f-aa96-455e2ffc66dd"/>
            <component pluginId="ca.digitalrapids.MPEG4Demuxer" name="MPEG4 Media Inspector" displayName="MPEG-4 Media Inspector" guid="31f535e6-23ad-48d4-9f77-40eaf1fe2588"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Multi-Program Audio Splitter" displayName="Multi-Program Audio Splitter" guid="6436a63f-1fa4-40e6-ba86-95138130d456"/>
            <component pluginId="ca.digitalrapids.PNGSourceController" name="PNG Controller" guid="2aa4773a-3f45-4e6d-9f50-1ab8c9a2ca9f"/>
            <component pluginId="ca.digitalrapids.PNGDecoder" name="PNG Decoder" displayName="PNG Decoder" guid="451037A2-8DA7-4898-A16F-B567052C23D0"/>
            <component pluginId="ca.digitalrapids.PNGSourceController" name="PNG Media Inspector" guid="b50abed0-e2f1-4a23-9fa2-a4e628665ca1"/>
            <component pluginId="ca.digitalrapids.DRProgressiveToInterlaced" name="DRProgressiveToInterlaced" displayName="Progressive To Interlaced" guid="05EE19E6-624C-4a64-8540-2AB31682B357"/>
            <component pluginId="ca.digitalrapids.CommonComponents" name="Random Access File" guid="ef0bd6fd-7564-4efb-bb78-a184bce33a29"/>
            <component pluginId="ca.digitalrapids.RasterFlip" name="Raster Orientation Inverter" guid="8d5132cd-c826-412e-9d57-51d0cc5d8166"/>
            <component pluginId="ca.digitalrapids.AudioFormatUtilities" name="Sample Rate Converter" guid="0DAC861A-FDD8-4e0c-97BB-3341C4E46999"/>
            <component pluginId="ca.digitalrapids.DRScaler" name="DRScaler" displayName="Scaler" guid="2EA57BB6-D100-4eaf-8DE0-1739BD64B833"/>
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
            <component pluginId="ca.digitalrapids.VideoFormatUtilities" name="Video Data Type Updater" guid="D7576695-6BCB-410F-BB86-734E5F526924"/>
            <component pluginId="ca.digitalrapids.VideoFormatConverter2" name="VideoFormatConverter2" displayName="Video Format Converter" guid="044A97C7-A980-433e-83FF-FC15067F627F"/>
            <component pluginId="ca.digitalrapids.VideoFormatConverter" name="Video Format Converter" displayName="Video Format Converter (Deprecated)" guid="AC185E0C-6839-4dae-A547-5E18DF5EA058"/>
            <component pluginId="ca.digitalrapids.VideoFormatUtilities" name="Video Frame Rate Updater" guid="3EA4F9F3-9DBA-4E6A-A55E-53A8D0A2BAC2"/>
            <component pluginId="ca.digitalrapids.VideoPulldownProcessor" name="Video Pulldown Processor" guid="B0BBD4A8-2E48-42F7-BF58-AB2660B27D01"/>
            <component pluginId="ca.digitalrapids.KayakCore" name="Kayak Graph" displayName="Zenium Graph" guid="abc785f2-427e-4522-ba00-f3cb6acd1220"/>
            <component pluginId="ca.digitalrapids.KayakCore" name="Kayak OOP Graph" displayName="Zenium OOP Graph" guid="967a0d59-a62e-4c75-962c-4f65c180d45c"/>
        </components>
    </dependencyInfo>
</kayakDocument>
