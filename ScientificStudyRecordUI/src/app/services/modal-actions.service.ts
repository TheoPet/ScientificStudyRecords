import { Injectable } from '@angular/core';
import { StudyService } from './study.service';

@Injectable({
  providedIn: 'root'
})
export class ModalActionsService {

  constructor(private studyService: StudyService) { }

  modalAction(modalData: any) {
    switch (modalData.name) {
      case 'deleteStudy':
        this.deleteStudy(modalData);
        break;

      default:
        break;
    }
  }

  private deleteStudy(modalData: any) {
    this.studyService.deleteStudy(modalData.studyId);
  }
}
