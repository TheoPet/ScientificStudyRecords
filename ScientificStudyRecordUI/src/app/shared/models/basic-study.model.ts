import { BasicData } from './basic-data.model';

export class BasicStudy extends BasicData {
         constructor(name: string, id?: number, headers?: string ) {
           super(name, id);
         }
       }
