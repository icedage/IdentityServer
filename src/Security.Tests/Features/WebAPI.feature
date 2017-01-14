Feature: WebAPI

#Scenario: Basic Authorization
#    Given an authorized request
#	When I apply for a loan
#    Then the result should respond with 200

Scenario: Roles Based Authentication
    Given an authorized request for an underwtiter
	When I approve a loan request
    Then the result should respond with 200
