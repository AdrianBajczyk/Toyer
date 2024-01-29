
import { forwardRef, } from "react";


const Input = forwardRef( function Input({id, label, ...props}, ref){
    return <>
        <label htmlFor={id} >{label}</label>
        <input id={id} name={id} ref={ref} {...props}/>
    </>
})

export default Input;