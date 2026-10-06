// Durum yönetimi
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { axiosClient } from '../api/axiosClient';
import type { Musteri } from '../types/customer';

// Backend'den müşterileri getiren asenkron işlem (GET)
export const fetchCustomers = createAsyncThunk(
    'customers/fetchAll',
    async () => {
        const response = await axiosClient.get<Musteri[]>('/Musteri');
        return response.data;
    }
);

// Yeni müşteri ekleyen asenkron işlem (POST)
export const addCustomer = createAsyncThunk(
    'customers/add',
    async (customerData: any, { rejectWithValue }) => {
        try {
            // C# API'deki ekleme metoduna POST isteği atıyoruz
            const response = await axiosClient.post('/Musteri', customerData);
            return response.data;
        } catch (error: any) {
            return rejectWithValue(error.response?.data || 'Kayıt sırasında bir hata oluştu');
        }
    }
);

// Mevcut müşteriyi güncelleyen asenkron işlem (PUT)
export const updateCustomer = createAsyncThunk(
    'customers/update',
    async (customerData: any, { rejectWithValue }) => {
        try {
            // C# API'deki güncelleme metoduna PUT isteği atıyoruz.
            // NOT: Backend URL yapına göre burası '/Musteri', '/Musteri/Guncelle' veya `/Musteri/${customerData.MusteriID}` olabilir. 
            // C# tarafındaki yönlendirmene göre burayı düzenleyebilirsin.
            const response = await axiosClient.put('/Musteri', customerData);
            return response.data;
        } catch (error: any) {
            return rejectWithValue(error.response?.data || 'Güncelleme sırasında bir hata oluştu');
        }
    }
);

// Müşteri silen asenkron işlem (DELETE)
export const deleteCustomer = createAsyncThunk(
    'customers/delete',
    async (id: number, { rejectWithValue }) => {
        try {
            // C# API'ye silme isteği atıyoruz
            await axiosClient.delete(`/Musteri/${id}`);
            return id; // Silinen müşterinin ID'sini reducer'a gönderiyoruz
        } catch (error: any) {
            return rejectWithValue(error.response?.data || 'Silme işlemi başarısız oldu');
        }
    }
);

interface CustomerState {
    customers: Musteri[];
    loading: boolean;
    error: string | null;
}

const initialState: CustomerState = {
    customers: [],
    loading: false,
    error: null,
};

const customerSlice = createSlice({
    name: 'customers',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            // --- Listeleme İşlemleri ---
            .addCase(fetchCustomers.pending, (state) => {
                state.loading = true;
            })
            .addCase(fetchCustomers.fulfilled, (state, action) => {
                state.loading = false;
                state.customers = action.payload;
            })
            .addCase(fetchCustomers.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message || 'Veriler çekilemedi';
            })

            // --- Ekleme İşlemleri ---
            .addCase(addCustomer.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(addCustomer.fulfilled, (state, action) => {
                state.loading = false;
                state.customers.push(action.payload);
            })
            .addCase(addCustomer.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload as string;
            })

            // --- Güncelleme İşlemleri (YENİ EKLENEN KISIM) ---
            .addCase(updateCustomer.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(updateCustomer.fulfilled, (state, action) => {
                state.loading = false;
                // Mevcut listedeki müşteriyi ID'sine göre bulup yeni verilerle güncelliyoruz
                const index = state.customers.findIndex(c => c.MusteriID === action.payload.MusteriID);
                if (index !== -1) {
                    state.customers[index] = action.payload;
                }
            })
            .addCase(updateCustomer.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload as string;
            })

            // --- Silme İşlemleri ---
            .addCase(deleteCustomer.pending, (state) => {
                state.loading = true;
            })
            .addCase(deleteCustomer.fulfilled, (state, action) => {
                state.loading = false;
                // Silinen müşteriyi ID'sine bakarak listeden filtreleyip atıyoruz
                state.customers = state.customers.filter(c => c.MusteriID !== action.payload);
            })
            .addCase(deleteCustomer.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload as string;
            });


    },
});

export default customerSlice.reducer;