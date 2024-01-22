import React, { forwardRef, type ComponentPropsWithoutRef } from "react";


type InputProps = {
    id: string;
    label: string;
} & ComponentPropsWithoutRef<'input'>;

const Input = forwardRef<HTMLInputElement, InputProps>( function Input({id, label, ...props}, ref){
    return <>
        <label htmlFor={id}>{label}</label>
        <input id={id} name={id} ref={ref} {...props}/>
    </>
})

export default Input;