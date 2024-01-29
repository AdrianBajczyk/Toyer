import Form, { FormHandle } from "../UI/Form.tsx";
import Input from "../UI/Input/Input.tsx";
import Button from "../UI/Button.tsx"
import { useRef } from "react";

interface RegisterFormProps{
    UserNameInput: string;
    PasswordInput: string;
    ConfirmPasswordInput: string;
    NameInput: string;
    SurnameInput: string;
     EmailInput: string;
     BirthDateInput: string;
     StreetInput: string;
     StreetNumberInput: number;
     UnitNumberInput: number;
     CityInput: string;
     StateInput: string;
     PostalCodeInput: string;
     Country: string;
     PhoneInput: string
}

export function RegisterForm(){

    const formRef = useRef<FormHandle>(null)

    function handleSave(data: unknown){
        const extractedData = data as RegisterFormProps
        console.log(extractedData)
        formRef.current?.submit("dupa.com", extractedData);
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