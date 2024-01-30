import {Link} from "react-router-dom";


export default function Button(props){

    const linkStyle = {
        textDecoration: 'none', 
        color: 'inherit',
        cursor: 'auto',
      };

if(props.element === 'link'){
    return <button {...props}><Link to={props.to} style={linkStyle}>{props.children}</Link></button>
} 
return <button {...props}>{props.children}</button>
}