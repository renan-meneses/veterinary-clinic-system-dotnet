import { useQuery } from '@tanstack/react-query'
import api from '@/shared/services/api'

interface TutorDashboard {
  totalAnimals: number
  upcomingVaccines: number
  scheduledConsultations: number
  activeHospitalizations: number
}

export function TutorDashboardPage() {
  const { data, isLoading } = useQuery({
    queryKey: ['tutor-dashboard'],
    queryFn: async () => {
      const response = await api.get('/bff/v1/tutor/dashboard')
      return response.data.data as TutorDashboard
    },
  })

  if (isLoading) {
    return (
      <div className="flex items-center justify-center h-64">
        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-primary-600"></div>
      </div>
    )
  }

  return (
    <div className="space-y-6">
      <h1 className="text-2xl font-bold text-gray-900">Bem-vindo ao Portal do Tutor</h1>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        <div className="bg-white rounded-xl shadow-sm p-6">
          <p className="text-sm text-gray-500">Meus Animais</p>
          <p className="text-3xl font-bold text-gray-900">{data?.totalAnimals ?? 0}</p>
        </div>
        <div className="bg-white rounded-xl shadow-sm p-6">
          <p className="text-sm text-gray-500">Próximas Vacinas</p>
          <p className="text-3xl font-bold text-gray-900">{data?.upcomingVaccines ?? 0}</p>
        </div>
        <div className="bg-white rounded-xl shadow-sm p-6">
          <p className="text-sm text-gray-500">Consultas Agendadas</p>
          <p className="text-3xl font-bold text-gray-900">{data?.scheduledConsultations ?? 0}</p>
        </div>
        <div className="bg-white rounded-xl shadow-sm p-6">
          <p className="text-sm text-gray-500">Internações Ativas</p>
          <p className="text-3xl font-bold text-gray-900">{data?.activeHospitalizations ?? 0}</p>
        </div>
      </div>
    </div>
  )
}