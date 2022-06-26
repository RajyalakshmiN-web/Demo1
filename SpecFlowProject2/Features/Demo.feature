Feature: Demo

A short summary of the feature

@tag1
Scenario: Compare both table extracts and generate results
	Given Connect to MS SQL Database and extract the source table records
	And Connect to MS SQL Database and extract the target table records
	When Compare both table extracts and generate results
	


