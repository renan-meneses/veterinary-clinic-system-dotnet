import { Link } from 'react-router-dom'
import { useAuthStore } from '@/shared/hooks/useAuthStore'

export function TutorPortalHeader() {
  const { user, logout } = useAuthStore()

  const handleLogout = () => {
    logout()
    window.location.href = '/login'
  }

  return (
    <header className="bg-white border-b border-gray-200">
      <div className="container mx-auto px-4">
        <div className="flex items-center justify-between h-16">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 bg-primary-600 rounded-lg flex items-center justify-center">
              <span className="text-white font-bold">VC</span>
            </div>
            <div>
              <div className="font-semibold text-gray-900">Portal do Tutor</div>
              <div className="text-xs text-gray-500">{user?.name}</div>
            </div>
          </div>

          <nav className="flex items-center gap-4">
            <Link to="/tutor/dashboard" className="text-sm text-gray-600 hover:text-primary-600">
              Início
            </Link>
            <Link to="/tutor/animals" className="text-sm text-gray-600 hover:text-primary-600">
              Meus Animais
            </Link>
            <Link to="/tutor/vaccines" className="text-sm text-gray-600 hover:text-primary-600">
              Vacinas
            </Link>
            <Link to="/tutor/consultations" className="text-sm text-gray-600 hover:text-primary-600">
              Consultas
            </Link>
          </nav>

          <button
            onClick={handleLogout}
            className="px-4 py-2 text-sm text-gray-600 hover:text-gray-900 hover:bg-gray-100 rounded-lg transition-colors"
          >
            Sair
          </button>
        </div>
      </div>
    </header>
  )
}