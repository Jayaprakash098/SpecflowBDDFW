Feature: TestFeature

A short summary of the feature

@youtube
Scenario:  Youtube testing - Scenario1
	When Search gavs tech in youtube
	And Navigate to channel
	Then Verify title of the page "GAVS - YouTube"

	@youtube
Scenario:  Youtube testing - Scenario2
	When Search gavs tech in youtube
	And Navigate to channel
	Then Verify title of the page "GAVS - y"

