import React from "react";



export default function Container({as, children, ...props}){
    const Component = as || React.Fragment;
    return <Component {...props}>{children}</Component>
}