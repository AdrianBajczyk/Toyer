import { ComponentPropsWithoutRef, type ReactNode } from 'react';

type ListProps<T> = {
  items: T[]; 
  renderItem: (item: T) => ReactNode;
} & ComponentPropsWithoutRef<'ul'>;

export default function List<T>({ items,  renderItem, ...otherProps }: ListProps<T>) {
  return <ul {...otherProps}>{items.map(renderItem)}</ul>;
}
