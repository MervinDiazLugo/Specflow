Feature: SpecFlow Features Examples
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Add two numbers
	Given the first number is 50
	And the second number is 70
	When the two numbers are added
	Then the result should be 120

@example
Scenario: Inicializar Navegador
	Given I am in main app
	And I put 'spotify.json' as DOM
	
@example
Scenario: Manejar dropdowns
	Given I am in main app
	And I put 'spotify.json' as DOM
	And I select 'Mes de Nacimiento' dropdown by text 'Enero'
	Then I select 'Mes de Nacimiento' dropdown by text 'Marzo'

@example
Scenario: Manejar textbox
	Given I am in main app
	And I put 'spotify.json' as DOM
	And I set 'Email' with 'mervindiazlugo@gmail.com'


@example
Scenario: Label text value
	Given I am in main app
	And I put 'spotify.json' as DOM
	And I set 'Email' with 'mervindiazlugo@gmail.com'
	And I click on 'Email Confirmacion' element
	Given I pause '3' seconds
	Then I check element 'Email Error' contains 'Este correo electrónico ya está conectado a una cuenta. Inicia sesión.'
	Then I save text of label 'Email Error' in scenario context
	And I set 'Email Confirmacion' with '{scenario:Email Error}'
	Given I pause '5' seconds


	

@Home
Scenario: Create New user in User Management Board
	#Given I am in the Homepage RHPRO
	#And Wait for DOM Complete
	#And I am logged in
	#And I set Language with 'Español - Argentina'
	And Wait for DOM Complete
	When I go to Users Management
	And I maximize window
	And I click on 'Agregar_Usuario' button
	And I set 'inputusername' with 'randomText' and save in context
	And I set 'inputname' with 'randomText' and save in context
	And I set 'inputemail' with 'randomEmail' and save in context
	And I set 'inputPassword' with 'suipacha' and save in context
	And I set 'inputPassword2' with 'suipacha' and save in context
	And I set 'list_UserTenants' dropdown to 'AUTOFMK_TEST'
	And I set 'list_RolesTenants' dropdown to 'WebAPI'
	And I set 'list_PoliciesTenants' dropdown to 'Relajada'
	And I click on 'Agregar' button
	And I click on 'Aceptar' button
	And I Wait for element 'Message_Box' is enable
	And I check that 'Message_Box' contains 'Usuario Creado'
	And I click on 'Aceptar' button
	And I Wait for element 'txtSearch' is enable
	And I set 'txtSearch' with '{scenario:inputusername}' and save in context
	And I click on 'btnLoad' button
	And I check that 'SelectionListUserName' contains '{scenario:inputusername}'
	And I close all windows
