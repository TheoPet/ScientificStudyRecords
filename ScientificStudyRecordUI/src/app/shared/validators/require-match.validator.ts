import { AbstractControl } from '@angular/forms';
import { isString } from 'util';

export function RequireMatch(control: AbstractControl) {
const selection: any = control.value;
if (isString(selection)) {
  return { requireMatch: true };
}
return null;
}
