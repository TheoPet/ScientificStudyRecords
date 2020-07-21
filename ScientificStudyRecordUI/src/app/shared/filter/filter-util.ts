import { BasicData } from '../models/basic-data.model';

export class FilterUtils {

    static displayFunction(object: BasicData) {
    if (object) {
      return object.name;
    }
    return '';
  }
}
