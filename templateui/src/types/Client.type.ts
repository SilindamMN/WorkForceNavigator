export interface IUpdateClientDto {
    clientName: string;
    phone: string;
    email: string;
  }  
  
  export interface IClientDto extends IUpdateClientDto{
    id:number;
  }

  export interface IClientDetails{
    projectName:string[],
  }