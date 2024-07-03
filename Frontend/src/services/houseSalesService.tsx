import axios, { AxiosInstance, AxiosResponse } from "axios";
import { fetchAuthSession } from "aws-amplify/auth";
import { ListItems } from "../models/sales-list";

class HouseSalesService {
  private api: AxiosInstance;

  constructor() {
    this.api = axios.create({
      baseURL: "https://api.sales.projects.bbdgrad.com",
      withCredentials: false,
    });

    this.api.interceptors.request.use(
      async (config) => {
        const session = await fetchAuthSession();
        const idToken = session?.tokens?.idToken?.toString();
        console.log(idToken);
        console.log(config);
        if (config.headers && idToken) {
          config.headers.Authorization = `Bearer ${idToken}`;
        }
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );
  }

  // Fetch sales data
  async fetchSales(): Promise<ListItems[]> {
    try {
      const response: AxiosResponse<ListItems[]> = await this.api.get(
        "/admin/get"
      );
      return response.data;
    } catch (error) {
      console.error("Error fetching sales data:", error);
      throw error;
    }
  }
}

const houseSalesService = new HouseSalesService();
export default houseSalesService;
