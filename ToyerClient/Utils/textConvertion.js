export function addSpaceBeforeUppercase(str) {
    let result = "";
    for (let i = 0; i < str.length; i++) {
      if (
        i > 0 &&
        str[i] === str[i].toUpperCase() &&
        str[i - 1] !== str[i - 1].toUpperCase()
      ) {
        result += " ";
      }
      result += str[i];
    }
    return result;
  }