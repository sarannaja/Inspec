import { AbstractControl, ValidationErrors } from "@angular/forms"

export const PasswordStrengthValidator = function (control: AbstractControl): ValidationErrors | null {

  let value: string = control.value || '';

  if (!value) {
    return null
  }

  let upperCaseCharacters = /((?=.*[a-zA-Z]).{6,})+/g
  if (upperCaseCharacters.test(value) === false) {
    return { passwordStrength: `ต้องการตัวอักษรภาษาอังกฤษ หรือ ตัวเลข` };
  }

  // let lowerCaseCharacters = /[a-z]+/g
  // if (lowerCaseCharacters.test(value) === false) {
  //   // return { passwordStrength: `text has to contine lower case characters,current value ${value}` };
  //   return { passwordStrength: `ต้องการตัวอักษรภาษาอังกฤษเล็ก` };
  // }


  // let numberCharacters = /[0-9]+/g
  // if (numberCharacters.test(value) === false) {
  //   return { passwordStrength: `ต้องมีตัวเลข` };
  // }

  // let specialCharacters = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/
  // if (specialCharacters.test(value) === false) {
  //   return { passwordStrength: `ต้องมีตัวอักษรพิเศษ เช่น !@#$%^&` };
  // }
  return null;
}