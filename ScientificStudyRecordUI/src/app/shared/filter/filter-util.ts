import { BasicSearch } from '../models/basic-search.model';

export class FilterUtils {

    static displayFunction(object: BasicSearch) {
    if (object) {
      return object.name;
    }
    return '';
  }
}
