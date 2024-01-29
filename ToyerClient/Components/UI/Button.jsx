
export default function Button(props){
if(props.element === 'anchor'){
    return <a {...props}>{props.children}</a>
} 
return <button {...props}>{props.children}</button>
}