import { BasicData } from './basic-data.model';

export class BasicTask extends BasicData {
         constructor(name: string, id?: number) {
           super(name, id);
         }
       }
