import { useQuery } from '@tanstack/react-query'
import api from '@/shared/services/api'
import { formatDateTime } from '@/shared/utils/cn'

interface QueueItem {
  id: string
  animalName: string
  tutorName: string
  veterinarianName: string
  officeName: string
  arrivalTime: string
  scheduledTime: string
  status: number
  statusName: string
  type: number
  typeName: string
  priority: number
}

interface QueueResponse {
  success: boolean
  data: {
    items: QueueItem[]
    totalCount: number
  }
}

const statusColors: Record<number, string> = {
  1: 'bg-yellow-100 text-yellow-800',
  2: 'bg-blue-100 text-blue-800',
  3: 'bg-green-100 text-green-800',
  4: 'bg-gray-100 text-gray-800',
  5: 'bg-red-100 text-red-800',
}

const typeColors: Record<number, string> = {
  1: 'border-blue-500',
  2: 'border-green-500',
  3: 'border-purple-500',
  4: 'border-red-500',
  5: 'border-orange-500',
  6: 'border-teal-500',
}

export function MonitorQueuePage() {
  const { data, isLoading, refetch } = useQuery({
    queryKey: ['attendance-queue-monitor'],
    queryFn: async () => {
      const response = await api.get('/bff/v1/attendance-queue/monitor')
      return response.data as QueueResponse
    },
    refetchInterval: 30000,
  })

  if (isLoading) {
    return (
      <div className="min-h-screen bg-gray-900 flex items-center justify-center">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-white"></div>
      </div>
    )
  }

  const queueItems = data?.data?.items || []

  return (
    <div className="min-h-screen bg-gray-900 text-white p-6">
      <div className="max-w-7xl mx-auto">
        <div className="flex items-center justify-between mb-8">
          <h1 className="text-3xl font-bold">Fila de Atendimento</h1>
          <button
            onClick={() => refetch()}
            className="px-4 py-2 bg-gray-800 hover:bg-gray-700 rounded-lg"
          >
            Atualizar
          </button>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          {queueItems.map((item) => (
            <div
              key={item.id}
              className={`bg-gray-800 rounded-xl p-6 border-l-4 ${typeColors[item.type] || 'border-gray-500'}`}
            >
              <div className="flex items-center justify-between mb-4">
                <span className="text-sm text-gray-400">
                  {formatDateTime(item.arrivalTime)}
                </span>
                <span className={`px-2 py-1 text-xs font-medium rounded ${statusColors[item.status]}`}>
                  {item.statusName}
                </span>
              </div>

              <h2 className="text-xl font-bold mb-2">{item.animalName}</h2>
              <p className="text-gray-400 mb-4">Tutor: {item.tutorName}</p>

              <div className="space-y-2 text-sm">
                {item.veterinarianName && (
                  <div className="flex items-center gap-2">
                    <span className="text-gray-500">Veterinário:</span>
                    <span>{item.veterinarianName}</span>
                  </div>
                )}
                {item.officeName && (
                  <div className="flex items-center gap-2">
                    <span className="text-gray-500">Consultório:</span>
                    <span>{item.officeName}</span>
                  </div>
                )}
                <div className="flex items-center gap-2">
                  <span className="text-gray-500">Tipo:</span>
                  <span>{item.typeName}</span>
                </div>
                {item.scheduledTime && (
                  <div className="flex items-center gap-2">
                    <span className="text-gray-500">Agendado:</span>
                    <span>{formatDateTime(item.scheduledTime)}</span>
                  </div>
                )}
              </div>

              <div className="mt-4 flex gap-2">
                <button className="flex-1 px-3 py-2 bg-primary-600 hover:bg-primary-700 rounded-lg text-sm">
                  Chamar
                </button>
                <button className="flex-1 px-3 py-2 bg-gray-700 hover:bg-gray-600 rounded-lg text-sm">
                  Iniciar
                </button>
              </div>
            </div>
          ))}
        </div>

        {queueItems.length === 0 && (
          <div className="text-center py-12 text-gray-400">
            Nenhum paciente na fila no momento.
          </div>
        )}
      </div>
    </div>
  )
}