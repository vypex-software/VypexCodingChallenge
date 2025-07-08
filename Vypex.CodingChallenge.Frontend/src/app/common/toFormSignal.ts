import { Signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { AbstractControl, FormArray, FormControl, FormGroup, ɵFormArrayRawValue, ɵFormGroupRawValue } from '@angular/forms';

export function toFormSignal<T>(formControl: FormControl<T>): Signal<T>;
export function toFormSignal<T extends { [K in keyof T]: AbstractControl<any>; }>(formGroup: FormGroup<T>): Signal<ɵFormGroupRawValue<T>>;
export function toFormSignal<T extends AbstractControl<any>>(formArray: FormArray<T>): Signal<ɵFormArrayRawValue<T>>;
export function toFormSignal(formControl: AbstractControl): Signal<any> {
  return toSignal(formControl.valueChanges, { initialValue: formControl.value });
}
