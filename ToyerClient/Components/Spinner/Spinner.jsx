import { Rings } from "react-loader-spinner";

const Spinner = ({ height, width }) => {
  return (
    <Rings
      visible={true}
      height={height}
      width={width}
      color="#FFFFFF"
      ariaLabel="rings-loading"
      wrapperStyle={{}}
      wrapperClass="spinner"
    />
  );
};

export default Spinner;
