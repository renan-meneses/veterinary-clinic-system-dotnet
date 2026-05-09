import axios from 'axios'
import type { AxiosInstance, AxiosRequestConfig, AxiosResponse, InternalAxiosRequestConfig } from 'axios'

const api: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_BFF_BASE_URL || 'http://localhost:5001',
  timeout: 30000,
  headers: {
    'Content-Type': 'application/json',
  },
})

api.interceptors.request.use(
  (config: InternalAxiosRequestConfig) => {
    const token = localStorage.getItem('@veterinary-clinic:token')
    if (token && config.headers) {
      config.headers.Authorization = `Bearer ${token}`
    }
    return config
  },
  (error) => Promise.reject(error)
)

api.interceptors.response.use(
  (response: AxiosResponse) => response,
  async (error) => {
    const originalRequest = error.config

    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true

      try {
        const refreshToken = localStorage.getItem('@veterinary-clinic:refresh-token')
        if (refreshToken) {
          const response = await axios.post(`${import.meta.env.VITE_BFF_BASE_URL}/bff/v1/auth/refresh-token`, {
            refreshToken,
          })

          const { accessToken, refreshToken: newRefreshToken } = response.data.data
          localStorage.setItem('@veterinary-clinic:token', accessToken)
          localStorage.setItem('@veterinary-clinic:refresh-token', newRefreshToken)

          originalRequest.headers.Authorization = `Bearer ${accessToken}`
          return api(originalRequest)
        }
      } catch (refreshError) {
        localStorage.removeItem('@veterinary-clinic:token')
        localStorage.removeItem('@veterinary-clinic:refresh-token')
        window.location.href = '/login'
        return Promise.reject(refreshError)
      }
    }

    return Promise.reject(error)
  }
)

export interface ApiResponse<T> {
  success: boolean
  data: T
  errors: string[]
  notifications: string[]
}

export interface PaginatedResponse<T> {
  success: boolean
  data: T[]
  page: number
  pageSize: number
  totalItems: number
  totalPages: number
  errors: string[]
}

export default api