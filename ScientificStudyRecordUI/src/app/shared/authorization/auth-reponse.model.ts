import { BasicUserResponse } from 'src/app/login/user-response.model';

export interface AuthResponse {
    accessToken: string;
    expiresAt: string;
    user: BasicUserResponse;
  }
