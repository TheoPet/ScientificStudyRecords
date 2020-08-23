import { BasicData } from '../models/basic-data.model';
import { BasicTestSubject } from '../models/basic-test-subject.model';

export class FilterUtils {

    static displayFunction(object: BasicData) {
    if (object) {
      return object.name;
    }
    return '';
  }
  static displaFunctionTestSubject(object: BasicTestSubject) {
    if (object) {
      const fullName = object.name + ' ' + object.surname;
      console.log(fullName)
      return fullName;
    }
    return '';
  }
}
