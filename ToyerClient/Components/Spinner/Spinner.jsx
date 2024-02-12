import { Rings } from 'react-loader-spinner'

const Spinner = () => {
  return (
    <Rings
    visible={true}
    height="600"
    width="600"
    color="#FFFFFF"
    ariaLabel="rings-loading"
    wrapperStyle={{}}
    wrapperClass=""
/>
  );
};

export default Spinner;
