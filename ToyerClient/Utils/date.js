import { parse, isBefore, subYears } from "date-fns";

export const isEarlierThan100YearsAgo = (inputDate) => {

    const parsedDate = parse(inputDate, "yyyy-MM-dd", new Date());
  
    const hundredYearsAgo = subYears(new Date(), 100);
  
    return isBefore(parsedDate, hundredYearsAgo);
  };