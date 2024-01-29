import Form from "../UI/Form.jsx";
import Input from "../UI/Input/Input.jsx";
import Button from "../UI/Button.jsx"
import { useRef } from "react";


export function RegisterForm(){

    const formRef = useRef(null)

    function handleSave(data){
    
        formRef.current?.submit("dupa.com", data);
        formRef.current?.clear();
    }


    return<>
    <Form onSave={handleSave} ref={formRef}>
        <Input type="text" label="User name" id="UserNameInput" />
        <Input type="password" label="Password" id="PasswordInput"  />
        <Input type="password" label="Confirm password" id="ConfirmPasswordInput"  />
        <Input type="text" label="Name" id="NameInput" />
        <Input type="text" label="Surname" id="SurnameInput" />
        <Input type="email" label="Email" id="EmailInput" />
        <Input type="date" label="Birth date" id="BirthDateInput" />
        <Input type="text" label="Street" id="StreetInput" />
        <Input type="number" label="Street number" id="StreetNumberInput" />
        <Input type="number" label="Unit number" id="UnitNumberInput" />
        <Input type="text" label="City" id="CityInput" />
        <Input type="text" label="State" id="StateInput" />
        <Input type="text" label="Postal code" id="PostalCodeInput"/>
        <Input type="text" label="Country" id="CountryInput"/>
        <Input type="text" label="Phone" id="PhoneInput"/>
    <Button element='button'>Register</Button>
    </Form>
    </>
}

export default RegisterForm;