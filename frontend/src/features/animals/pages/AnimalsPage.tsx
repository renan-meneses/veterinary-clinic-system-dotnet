import { useQuery } from '@tanstack/react-query'
import api from '@/shared/services/api'
import { formatDate } from '@/shared/utils/cn'

interface Animal {
  id: string
  name: string
  species: number
  breed: string
  sex: number
  birthDate: string
  weight: number
  color: string
  photoUrl: string
  status: number
}

interface AnimalsResponse {
  success: boolean
  data: Animal[]
  page: number
  pageSize: number
  totalItems: number
}

const speciesMap: Record<number, string> = {
  1: 'Cão',
  2: 'Gato',
  3: 'Pássaro',
  4: 'Coelho',
  5: 'Hamster',
  6: 'Peixe',
  7: 'Réptil',
  99: 'Outro',
}

export function AnimalsPage() {
  const { data, isLoading } = useQuery({
    queryKey: ['animals'],
    queryFn: async () => {
      const response = await api.get('/bff/v1/animals')
      return response.data as AnimalsResponse
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
      <div className="flex items-center justify-between">
        <h1 className="text-2xl font-bold text-gray-900">Animais</h1>
        <button className="px-4 py-2 bg-primary-600 text-white rounded-lg hover:bg-primary-700">
          Novo Animal
        </button>
      </div>

      <div className="bg-white rounded-xl shadow-sm border border-gray-100 overflow-hidden">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Nome
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Espécie
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Raça
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Data Nascimento
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Peso
              </th>
              <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                Status
              </th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
            {data?.data?.map((animal) => (
              <tr key={animal.id} className="hover:bg-gray-50">
                <td className="px-6 py-4 whitespace-nowrap">
                  <div className="flex items-center">
                    <div className="flex-shrink-0 h-10 w-10">
                      {animal.photoUrl ? (
                        <img
                          className="h-10 w-10 rounded-full object-cover"
                          src={animal.photoUrl}
                          alt={animal.name}
                        />
                      ) : (
                        <div className="h-10 w-10 rounded-full bg-primary-100 flex items-center justify-center">
                          <span className="text-primary-600 font-medium">
                            {animal.name.charAt(0).toUpperCase()}
                          </span>
                        </div>
                      )}
                    </div>
                    <div className="ml-4">
                      <div className="text-sm font-medium text-gray-900">{animal.name}</div>
                    </div>
                  </div>
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {speciesMap[animal.species] || 'Desconhecido'}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {animal.breed || '-'}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {animal.birthDate ? formatDate(animal.birthDate) : '-'}
                </td>
                <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                  {animal.weight ? `${animal.weight} kg` : '-'}
                </td>
                <td className="px-6 py-4 whitespace-nowrap">
                  <span className="px-2 py-1 text-xs font-medium rounded-full bg-green-100 text-green-800">
                    {animal.status === 1 ? 'Ativo' : 'Inativo'}
                  </span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>

        {(!data?.data || data.data.length === 0) && (
          <div className="text-center py-12 text-gray-500">
            Nenhum animal encontrado.
          </div>
        )}
      </div>
    </div>
  )
}