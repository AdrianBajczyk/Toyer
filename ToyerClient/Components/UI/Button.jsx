import { Link } from "react-router-dom";

export default function Button(props) {
  const linkStyle = {
    textDecoration: "none",
    color: "inherit",
  };

  if (props.element === "link") {
    return (
      <button className="generalButton" {...props}>
        <Link to={props.to} style={linkStyle}>
          {props.children}
        </Link>
      </button>
    );
  }
  return (
    <button className="generalButton" {...props}>
      {props.children}
    </button>
  );
}
