import React, { type ReactNode, type ComponentPropsWithoutRef, Children } from "react";

type ButtonProps = {
    element: 'button';
    children: ReactNode;
} & ComponentPropsWithoutRef<'button'>

type AnchorProps = {
    element: 'anchor';
    children: ReactNode;
} & ComponentPropsWithoutRef<'a'>

export default function Button(props: ButtonProps | AnchorProps){
if(props.element === 'anchor'){
    return <a {...props}>{props.children}</a>
} 
return <button {...props}>{props.children}</button>
}