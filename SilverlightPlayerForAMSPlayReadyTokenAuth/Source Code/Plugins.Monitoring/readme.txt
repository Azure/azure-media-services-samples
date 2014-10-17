    This is a Silverlight Media Framework plugin used to wire up the diagnostics component to SMF, monitor the information it generates, and pass it onto the logging component in a uniform format (i.e. Log objects).
	All information is logged via LoggingService.Current (defined in Microsoft.SilverlightMediaFramework.Logging).
	Developers also have the ability to tap into the API directly by creating their own log objects and getting this component to populate these logs with basic video information.
	This component can be configured (optional) via the SMFPlayer.GlobalConfigMetadata property...
		<Core:SMFPlayer>
            <Core:SMFPlayer.GlobalConfigMetadata>
                <Utilities:MetadataItem Key="Microsoft.SilverlightMediaFramework.Logging.ConfigUri" Value="MonitoringConfig.xml" />
            </Core:SMFPlayer.GlobalConfigMetadata>
        </Core:SMFPlayer>