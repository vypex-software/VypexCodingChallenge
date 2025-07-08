import { AbstractControl, FormArray, FormGroup } from '@angular/forms';

export function validateForm(form: FormGroup | FormArray | AbstractControl): boolean {
  if (form instanceof FormArray) {
    return form.controls.every(validateForm);
  } else if (form instanceof FormGroup) {
    return Object.values(form.controls).every(validateForm);
  } else {
    form.markAsDirty();
    form.updateValueAndValidity({ onlySelf: true });
    return form.valid;
  }
}
