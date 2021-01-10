import { Component, OnInit, AfterContentInit, TemplateRef} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICandidatePhoto } from '../../shared/models/CandidatePhoto';
import { AgencyService } from '../agency.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AccountService } from 'src/app/account/account.service';
import { IAgencyPhoto } from 'src/app/shared/models/AgencyPhoto';
import swal from 'sweetalert2';

@Component({
  selector: 'app-manage-profile-photo',
  templateUrl: './manage-profile-photo.component.html',
  styleUrls: ['./manage-profile-photo.component.css'],
})
export class ManageProfilePhotoComponent implements OnInit, AfterContentInit {
  files: File[] = [];
  agencyPhotos$: Observable<IAgencyPhoto[]>;
  modalRef: BsModalRef;
  message: string;
  deletePhoto: IAgencyPhoto;
  mainPhoto: IAgencyPhoto;
  constructor(
    private http: HttpClient,
    private modalService: BsModalService,
    private accountService: AccountService,
    private agencyService: AgencyService
  ) {}

  onSelect(event) {
    console.log('seleted file', event);
    this.files.push(...event.addedFiles);
    this.onFileUpload();
  }
  ngAfterContentInit(): void {
     this.agencyService.getAgencyPhotos().subscribe();
  }
  refreshPhotos(item: IAgencyPhoto) {
     this.agencyService.updateAfterInsert(item);
  }
  onRemove(event) {
    console.log('remove', event);
    console.log(this.files.indexOf(event));
    this.files.splice(this.files.indexOf(event), 1);
  }
  onRemove1() {
    this.files.splice(0, 1);
  }
  ngOnInit(): void {
    this.agencyPhotos$ = this.agencyService.agencyPhotos$;
    //  this.basket$ = this.basketService.basket$;
  }
  onFileUpload() {
    const fd = new FormData();
    fd.append('File', this.files[0], this.files[0].name);
    this.http
      .post('https://localhost:44327/api/AgencyPhoto', fd)
      .subscribe((res: IAgencyPhoto) => {
        console.log(res);
        this.onRemove1();
        this.refreshPhotos(res);
      });
  }
 async setToMainPicture(item: IAgencyPhoto) {
     this.deletePhoto = item;
     const result = await swal.fire({
       title: 'Are you sure you?',
       text: 'This Photo will set as main !',
       icon: 'info',
       showCancelButton: true,
       confirmButtonText: 'Yes, set as default!',
       cancelButtonText: 'No'
     });
     if (result.value)
     {

      this.agencyService.setAgencyPhotoMain(this.deletePhoto.id).subscribe(() => {
        this.accountService.changeMemberPhoto(item.url);
        swal.fire(
          'Changed!',
          'image has been set as default picture.',
          'success'
          );
      });
     } else if (result.dismiss === swal.DismissReason.cancel)
     {
       swal.fire(
           'Cancelled',
           'everything remains the same :)',
           'info'
         );
     }
  }
  removePhoto(item: ICandidatePhoto) {
    console.log(item);
    // debugger;

    // this.candidateService.deleteCandidate(item.id);
    this.modalRef = this.modalService.show('template', { class: 'modal-sm' });
    // this.modalRef = this.modalService.show('template');
    // this.openModal('template')
    // this.candidatePhotos$ = this.candidateService.getCandidatePhotos();
  }
  openModalMain(template: TemplateRef<any>, item: IAgencyPhoto) {
    this.mainPhoto = item;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  openModal(template: TemplateRef<any>, item: IAgencyPhoto) {

    this.deletePhoto = item;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirmMain(): void {
    this.modalRef.hide();
    this.setToMainPicture(this.mainPhoto);
  }
  confirmDelete(): void {
    this.modalRef.hide();
    this.agencyService.deleteAgencyPhoto(this.deletePhoto.id).subscribe();
  }
  decline(): void {
    this.message = 'Declined!';
    this.modalRef.hide();
  }
  // tslint:disable-next-line:adjacent-overload-signatures
  updatePhoto(item: ICandidatePhoto) {
    this.agencyService.UpdateAgencyPhoto(item.id, 'today');
  }
  async showModateswal(item: IAgencyPhoto) {
    this.deletePhoto = item;
    const result = await swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this imaginary file!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    });
    if (result.value)
    {
      console.log(result.value);
      this.agencyService.deleteAgencyPhoto(this.deletePhoto.id).subscribe(() => {
        swal.fire(
        'Deleted!',
        'Your imaginary file has been deleted.',
        'success'
        );
      });

    } else if (result.dismiss === swal.DismissReason.cancel)
    {
      swal.fire(
          'Cancelled',
          'Your imaginary file is safe :)',
          'error'
        );
    }
  }
}
