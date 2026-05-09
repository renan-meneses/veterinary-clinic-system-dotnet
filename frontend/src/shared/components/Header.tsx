import { useAuthStore } from '@/shared/hooks/useAuthStore'
import { Link } from 'react-router-dom'

export function Header() {
  const { user, logout } = useAuthStore()

  const handleLogout = () => {
    logout()
    window.location.href = '/login'
  }

  return (
    <header className="h-16 bg-white border-b border-gray-200 flex items-center justify-between px-6">
      <div className="flex items-center gap-4">
        <h1 className="text-lg font-semibold text-gray-900">
          Veterinary Clinic Admin
        </h1>
      </div>

      <div className="flex items-center gap-4">
        <div className="flex items-center gap-2">
          <div className="w-8 h-8 bg-primary-100 rounded-full flex items-center justify-center">
            <span className="text-primary-600 font-medium text-sm">
              {user?.name?.charAt(0).toUpperCase() || 'U'}
            </span>
          </div>
          <div className="text-sm">
            <div className="font-medium text-gray-900">{user?.name}</div>
            <div className="text-gray-500 text-xs">{user?.roleName}</div>
          </div>
        </div>

        <button
          onClick={handleLogout}
          className="ml-4 px-3 py-1.5 text-sm text-gray-600 hover:text-gray-900 hover:bg-gray-100 rounded-lg transition-colors"
        >
          Sair
        </button>
      </div>
    </header>
  )
}