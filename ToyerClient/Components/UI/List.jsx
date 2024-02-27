

export default function List({ items,  renderItem,...otherProps }) {
  return <ul {...otherProps}>{items.map(renderItem)}</ul>;
}
